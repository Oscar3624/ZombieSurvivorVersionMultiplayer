using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Disparo : MonoBehaviourPun
{
    public float velx= 10f; //Velocidad del disparo en x
    public float vely= 0;
    public int dano= 10;
    public float lifetime = 5f; // Tiempo de vida de la bala en segundos
    // Start is called before the first frame update
    void Start()
    {
         // Destruye la bala automáticamente después de `lifetime` segundos
        if (photonView.IsMine)
        {
            Destroy(gameObject, lifetime);
        }
    }

    // Update is called once per frame
    void Update()
    {
                // Solo el propietario del disparo lo mueve
        
            transform.Translate(velx * Time.deltaTime, 0, 0);
        
    }
/*
    public void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }*/

    /*
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Obstaculo" || other.gameObject.tag == "Trampa" || other.gameObject.tag == "Terreno")
        {
            Debug.Log("impacto obstaculo");
            Destroy(this.gameObject);
        }
        
        if (other.gameObject.tag == "Enemigo")
        {
            Enemigo enemigo = other.gameObject.GetComponent<Enemigo>();
            if (enemigo != null)
            {
                enemigo.getDamage(this.dano);
                Destroy(this.gameObject);
            }
        }
        Debug.Log(other.gameObject.tag);
    }
    */

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstaculo") || other.CompareTag("Terreno") || other.CompareTag("Trampa") || other.CompareTag("Plataforma"))
        {
            if (photonView.IsMine)
            {
                PhotonNetwork.Destroy(this.gameObject);
            }
        }

        // Intenta obtener el componente Enemigo o MoverEnemigoPlataforma
        Enemigo enemigo1 = other.GetComponent<Enemigo>();
        MoverEnemigoPlataforma enemigo2 = other.GetComponent<MoverEnemigoPlataforma>();

        // Llama a getDamage en el enemigo correspondiente
        if (enemigo1 != null)
        {
            enemigo1.getDamage(this.dano);
            if (photonView.IsMine)
            {
                PhotonNetwork.Destroy(this.gameObject);
            }
        }
        else if (enemigo2 != null)
        {
            enemigo2.getDamage(this.dano);
            if (photonView.IsMine)
            {
                PhotonNetwork.Destroy(this.gameObject);
            }
        }
    }
    
}
