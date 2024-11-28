using System.Collections;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class TimeController2 : MonoBehaviourPunCallbacks
{
    [SerializeField] int min, seg;
    [SerializeField] TextMeshProUGUI tiempo;

    private float restante;
    private bool enMarcha;
    private Jugador jugador; // Referencia al jugador

    private void Awake()
    {
        ReiniciarTemporizador();
    }

    void Update()
    {
        if (PhotonNetwork.IsMasterClient && enMarcha)
        {
            restante -= Time.deltaTime;
            if (restante < 1)
            {
                enMarcha = false;
                photonView.RPC("TerminarTiempo", RpcTarget.All);
            }
            photonView.RPC("ActualizarTiempo", RpcTarget.All, restante);
        }
    }

    [PunRPC]
void ActualizarTiempo(float tiempoRestante)
{
    restante = tiempoRestante;
    int tempMin = Mathf.FloorToInt(restante / 60);
    int tempSeg = Mathf.FloorToInt(restante % 60);
    
    if (tiempo != null)
    {
        tiempo.text = string.Format("{00:00}:{01:00}", tempMin, tempSeg);
    }
    else
    {
        Debug.LogWarning("El componente tiempo no está asignado en el Inspector.");
    }
}

    [PunRPC]
    void TerminarTiempo()
    {
        enMarcha = false;
        if (jugador != null && photonView.IsMine)
        {
            jugador.getDamage(100); // Solo el jugador local recibe el daño
        }
    }

    public void AsignarJugador(Jugador jugadorRef)
    {
        jugador = jugadorRef; // Asigna la referencia del jugador
    }
        // Método para reiniciar el temporizador
    public void ReiniciarTemporizador()
    {
        restante = (min * 60) + seg;
        enMarcha = true;
    }
}