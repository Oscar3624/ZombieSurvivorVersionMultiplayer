using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaDanio : MonoBehaviour
{
    private GameManager gm;
    private int danio= 10;
    
    
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Jugador jugador = other.gameObject.GetComponent<Jugador>();
        if (jugador != null)
        {
            jugador.getDamage(this.danio);
        }
    }

    private void OnCollisionEnter2D(Collision2D other){
        Jugador jugador = other.gameObject.GetComponent<Jugador>();
        if (jugador != null)
        {
            jugador.getDamage(this.danio);
        }
    }
}
