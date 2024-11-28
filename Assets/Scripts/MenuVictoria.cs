using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class MenuVictoria : MonoBehaviourPun
{
    private GestorParaPhoton gestorPhoton;

    private void Start()
    {
        gestorPhoton = FindObjectOfType<GestorParaPhoton>();
    }

    public void VolverAJugar()
    {
        if (gestorPhoton != null)
        {
            gestorPhoton.SalirAlMenu();
        }
        // Si ya está conectado, reinicia el juego y vuelve a conectar
        StartCoroutine(EsperarDesconexion());
        SceneManager.LoadScene("EscenaPrincipal");
    }

    public void Salir()
    {
        Debug.Log("Salir");
        Application.Quit();
    }

    private IEnumerator EsperarDesconexion()
    {
        PhotonNetwork.Disconnect();
        // Espera hasta que Photon esté completamente desconectado
        while (PhotonNetwork.IsConnected)
        {
            yield return null;
        }

        // Luego recarga la escena principal
        
    }

}
