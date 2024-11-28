using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MoverEnemigoPlataforma : MonoBehaviourPun
{
    [SerializeField]
    private float velocidadEnemigo;
    [SerializeField]
    private Transform controladorSuelo;
    [SerializeField]
    private float distancia;
    [SerializeField]
    private bool haciaDerecha;
    
    private Rigidbody2D rb;

    [SerializeField]
    private AudioSource VozZombie1;

    private int cont=0;

    public int vida = 50;
    public int danoMordida = 10;
    public int puntos = 1;
    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();        
    }

    // Update is called once per frame
    void Update()
    {        
    }

    private void FixedUpdate() 
    {
        RaycastHit2D infoSuelo = Physics2D.Raycast(controladorSuelo.position, Vector2.down, distancia);
        rb.velocity = new Vector2(velocidadEnemigo,rb.velocity.y);

        if(infoSuelo == false)
        {
            //Girar
            Girar();
        }        
    }

    private void Girar()
    {
        haciaDerecha = !haciaDerecha;
        transform.eulerAngles = new Vector3(0,transform.eulerAngles.y + 180,0);
        velocidadEnemigo *= -1;
        cont++;
        
        if(cont % 2 == 0)
        
        VozZombie1.Play();
        
        
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controladorSuelo.transform.position, controladorSuelo.transform.position + Vector3.down * distancia);                
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Plataforma")
        {
            transform.parent = collision.transform;
        }
        
        //Si choca con el jugador, hace daño
        Jugador jugador = collision.gameObject.GetComponent<Jugador>();
        if (jugador != null)
        {
            jugador.getDamage(this.danoMordida);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Plataforma")
        {
            transform.parent = null;
        }
    }

    public void getDamage(int damage)
    {
        this.vida -= damage;
        if (this.vida <= 0)
        {
            gm.incScore(this.puntos);
            gm.enemiesCount(-1);
            photonView.RPC("DestruirEnemigo", RpcTarget.All); // Sincroniza la destrucción
            
        }
    }

    [PunRPC]
    void DestruirEnemigo()
    {
        Destroy(gameObject);
    }
}
