using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class MenuDePausa : MonoBehaviourPun
{
    public static bool Pausado = false;
    public GameObject PausaUI;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Pausado)
            {
                Continuar();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void Continuar()
    {
        //PausaUI.SetActive(false);
        //Time.timeScale = 1f;
        //Pausado = false;
        photonView.RPC("SincronizarPausa", RpcTarget.All, false);
    }

    public void Pausar()
    {
        photonView.RPC("SincronizarPausa", RpcTarget.All, true);
        //PausaUI.SetActive(true);
        //Time.timeScale = 0f;
        //Pausado = true;
    }

        [PunRPC]
    void SincronizarPausa(bool pausa)
    {
        PausaUI.SetActive(pausa);
        Time.timeScale = pausa ? 0f : 1f;
        Pausado = pausa;
    }

    public void Inicio()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("EscenaPrincipal");
    }

    public void Salir()
    {
        Application.Quit();
        Debug.Log("Saliendo...");
    }

}
