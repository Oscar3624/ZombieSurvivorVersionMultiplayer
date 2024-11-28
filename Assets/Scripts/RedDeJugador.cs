using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RedDeJugador : MonoBehaviour
{

    public MonoBehaviour[] Ignorar;

    private PhotonView photonview;
    // Start is called before the first frame update
    void Start()
    {
        photonview = GetComponent<PhotonView>();
        if(!photonview.IsMine){
            foreach(var codigo in Ignorar)
            {
                codigo.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
