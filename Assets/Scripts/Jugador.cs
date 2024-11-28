using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class Jugador : MonoBehaviourPun
{
    public float velX= 5f;
    public float velY= 5f;
    public Rigidbody2D rb2d;
    public GameObject disparo;
    public Transform arma;
    public Transform bala;
    private GameManager gm;
    private SpriteRenderer spriteRenderer;
    public int maxhealth = 100;
    public int vidaenelmomento;

    public Animator animator;
    

    [SerializeField] private AudioSource Salto;
    [SerializeField] private AudioSource Shoot;
    

    // Start is called before the first frame update
    void Start()
    {
            if (photonView.IsMine)
        {
            // Llama a una función que configura la cámara para seguir este jugador
            ConfigurarCamara();
        }
        rb2d = GetComponent<Rigidbody2D>();
        arma= GetComponent<Transform>().GetChild(0);
        spriteRenderer= GetComponent<SpriteRenderer>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        //bala= GameObject.Find("Disparo").GetComponent<Transform>();
        vidaenelmomento = maxhealth;
        gm.SetMaxVida(maxhealth);
    }

    // Update is called once per frame
    void Update()
    {
        //Mover
        if(Input.GetAxis("Horizontal") < 0){
            spriteRenderer.flipX= true;
        }else{
            spriteRenderer.flipX= false;
        }

        transform.Translate(Input.GetAxis("Horizontal") * velX * Time.deltaTime, 0, 0);
        //rb2d.velocity= new Vector2(Input.GetAxis("Horizontal"), rb2d.velocity.y);

        //Saltar si está en el suelo
        if (Input.GetKey("space") && CheckGround.isGrounded)
        {
            //movement= new Vector2(0, Input.GetAxis("Vertical") * velY * Time.deltaTime);
            //this.rb2d.MovePosition(transform.position + new Vector3(rb2d.velocity.x, Input.GetAxis("Vertical") * velY * Time.deltaTime, 0));
            rb2d.velocity= new Vector2(rb2d.velocity.x, velY);
            //transform.Translate(Input.GetAxis("Horizontal") * velX * Time.deltaTime, Input.GetAxis("Vertical") * velY * Time.deltaTime, 0);
            Salto.Play();
            animator.SetBool("Saltar",true);
        }else if(CheckGround.isGrounded) 
        {
            animator.SetBool("Saltar",false);
        }

        //Disparar
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            animator.SetBool("Disparar",true);
            //Ubicar disparo en boca del cañon del arma
            //Obtengo los datos de posicion, tamaño y rotación del arma
            Vector2 posicionCanion= arma.transform.GetChild(0).position;
            Vector2 tamanioCanion= arma.transform.GetChild(0).localScale;
            Quaternion rotacionArma= arma.transform.GetChild(0).rotation;
            
            // Instancia el disparo en red usando PhotonNetwork.Instantiate
    if (photonView.IsMine)
    {
        PhotonNetwork.Instantiate("Disparo", posicionCanion, rotacionArma);
    }

    Shoot.Play();/*
            //Genero el vector posicion para la bala
            Vector2 posicionBala = new Vector2(posicionCanion.x + tamanioCanion.x/2 + bala.localScale.x/2, posicionCanion.y);
            //Instacio el disparo con los datos anteriores
            Instantiate(disparo, posicionBala, rotacionArma);
            Shoot.Play();
            */
        }else{
            animator.SetBool("Disparar",false);
        }

    }

    //Recibir daño
    public void getDamage(int damage)
    {
        gm.decLife(damage);
        vidaenelmomento -= damage;
        gm.SetHealth(vidaenelmomento);

        // Verifica si el jugador ha perdido toda la vida
        if (vidaenelmomento <= 0)
        {
            if (photonView.IsMine)
            {
                // Envía el RPC de derrota a todos los jugadores
                photonView.RPC("GameOver", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    void GameOver()
    {
        PhotonNetwork.LoadLevel("EscenaDerrota"); // Lleva a todos los jugadores a la escena de derrota
        // Destruye el jugador actual y carga la escena de derrota en ambos clientes
        //Destroy(this.gameObject);
        //PhotonNetwork.LoadLevel("EscenaDerrota"); // Carga la escena de derrota para todos los jugadores
    }

    
        private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Plataforma")
        {
            transform.parent = collision.transform;
        }
    } 

        private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Plataforma")
        {
            transform.parent = null;
        }
    } 

    private void ConfigurarCamara()
    {
        // Encuentra la cámara virtual y establece el seguimiento en este jugador
        Cinemachine.CinemachineVirtualCamera camaraVirtual = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
        if (camaraVirtual != null)
        {
            camaraVirtual.Follow = transform;
        }
        else
        {
            Debug.LogWarning("No se encontró la cámara virtual de Cinemachine en la escena.");
        }
    }
}
