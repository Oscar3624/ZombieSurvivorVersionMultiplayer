using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicio : MonoBehaviour
{
    
    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1 );
    }

    public void Salir()
    {
        Debug.Log("Saliendoo...");
        Application.Quit();
    }
}
