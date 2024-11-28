using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class MoverPlataforma : MonoBehaviourPun
{
    public GameObject plataformaMover;

    public Transform puntoInicio;
    public Transform puntoFin;

    public float velocidadPlataforma = 1f;

    private Vector3 moverHacia;

    // Start is called before the first frame update
    void Start()
    {
        moverHacia = puntoFin.position;        
    }

    // Update is called once per frame
    void Update()
    {
        
        plataformaMover.transform.position = Vector3.MoveTowards(plataformaMover.transform.position,moverHacia, velocidadPlataforma * Time.deltaTime);
        
        if(plataformaMover.transform.position == puntoFin.position)
        {
            moverHacia = puntoInicio.position;                        
        }

        if(plataformaMover.transform.position == puntoInicio.position)
        {
            moverHacia = puntoFin.position;                        
        }

    }


}
