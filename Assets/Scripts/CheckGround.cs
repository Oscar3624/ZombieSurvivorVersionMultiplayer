using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{

    public static bool isGrounded;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Terreno") || other.CompareTag("Obstaculo") || other.CompareTag("Trampa") || other.CompareTag("Plataforma"))
        {
            isGrounded= true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Terreno") || other.CompareTag("Obstaculo") || other.CompareTag("Trampa") || other.CompareTag("Plataforma"))
        {
            isGrounded= false;
        }
    }
}
