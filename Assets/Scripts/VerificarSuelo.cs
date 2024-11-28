using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerificarSuelo : MonoBehaviour
{
   public static bool estaEnSuelo;

   private void OnTriggerEnter2D(Collider2D other) 
   {
    estaEnSuelo = true;    
   }
   private void OnTriggerExit2D(Collider2D other) 
   {
    estaEnSuelo = false;    
   }
   
}
