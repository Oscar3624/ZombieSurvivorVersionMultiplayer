using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Enemigo : MonoBehaviourPun
{
    public float velX = -3f; //Se mueve hacia la izquierda
    public float velY = 0;
    public bool onChase = false;
    public int vida = 50;
    public int danoMordida = 10;
    //public int danoGolpe = 5;
    public int puntos = 1;
    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movimiento del enemigo
        transform.Translate(this.velX * Time.deltaTime, 0, 0);
    }

    //Si el enemigo deja la pantalla, se destruye
    public void OnBecameInvisible()
    {
        Debug.Log("muerte zombi");
        Destroy(this.gameObject);
    }

    public void getDamage(int damage)
    {
        this.vida -= damage;
        if (this.vida <= 0)
        {
            gm.incScore(this.puntos);
            gm.enemiesCount(-1);
            //Destroy(gameObject);
            photonView.RPC("DestruirEnemigo", RpcTarget.All);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Jugador jugador = other.gameObject.GetComponent<Jugador>();
        if (jugador != null)
        {
            jugador.getDamage(this.danoMordida);
        }
        if (other.gameObject.tag == "Obstaculo")
        {
            velX *= -1;
        }
    }

    [PunRPC]
    void DestruirEnemigo()
    {
        // Este método se ejecutará en todos los clientes y destruirá el enemigo
        Destroy(gameObject);
    }
}
