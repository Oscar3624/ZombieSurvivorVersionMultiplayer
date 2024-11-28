using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class MenuDerrota : MonoBehaviourPun
{
    private GestorParaPhoton gestorPhoton;

    private void Start()
    {
        gestorPhoton = FindObjectOfType<GestorParaPhoton>();
    }

    public void Jugar()
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
        if (gestorPhoton != null)
        {
            gestorPhoton.SalirAlMenu();
        }
        // Si ya está conectado, reinicia el juego y vuelve a conectar
        StartCoroutine(EsperarDesconexion());
        Application.Quit();
        Debug.Log("Saliendo...");
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
