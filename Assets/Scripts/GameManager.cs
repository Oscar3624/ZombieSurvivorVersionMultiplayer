using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public float vida = 100;
    private int contadorEnemigos = 0;
    private int limiteEnemigos = 30;
    private int clock = 0;
    private float contador = 0f;    //Valor de inicio de cuenta
    private float quantum = 1f;
    //public TextMeshProUGUI scoreText;
    //public TextMeshProUGUI vidaText;
    //public int scorePasarNivel = 10;
    //public TextMeshProUGUI clockText;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        this.score = 0;
        this.vida = 100;
        //scoreText.text = "Score: " + this.score;
        this.clock = 0;
        //clockText.text = "tiempo: "+ this.clock + " seg";
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;
        if(contador >= quantum)
        {
            StartCoroutine("reloj", quantum);
            contador = 0f;
            //clockText.text = "tiempo: "+ this.clock + " seg";
        }

    }

    public void incScore(int puntos)
    {
        this.score += puntos;
        //scoreText.text = "Score: " + this.score;
    }

    public void decLife(int damage)
    {
        this.vida -= damage;
        //vidaText.text = "Salud: " + this.vida;
    }

    public void enemiesCount(int cantidad)
    {
        this.contadorEnemigos+= cantidad;
    }

    public int getEnemiesCount()
    {
        return this.contadorEnemigos;
    }

    public int getEnemiesMax()
    {
        return this.limiteEnemigos;
    }

    IEnumerator reloj(float t)
    {
        yield return new WaitForSeconds(t) ;
        this.clock++;
    }

    public void SetMaxVida(int vida)
    {
        slider.maxValue = vida;
        slider.value = vida;
    }
    //asd

    public void SetHealth(int vida)
    {
        slider.value = vida;
    }
}
