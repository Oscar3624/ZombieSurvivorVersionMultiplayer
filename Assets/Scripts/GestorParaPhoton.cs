using System.Collections;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GestorParaPhoton : MonoBehaviourPunCallbacks
{
    private TimeController2 timeController;

    void Start()
    {
        timeController = FindObjectOfType<TimeController2>();

        // Solo conecta si no está ya conectado
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        RoomOptions roomOptions = new RoomOptions { MaxPlayers = 2 };
        PhotonNetwork.JoinOrCreateRoom("Sala", roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        // Reinicia el temporizador al ingresar en la sala
        if (timeController != null)
        {
            timeController.ReiniciarTemporizador();
        }

        // Instancia el jugador si no existe ya
        if (GameObject.FindObjectOfType<Jugador>() == null)
        {
            GameObject jugadorObj = PhotonNetwork.Instantiate("Jugador", new Vector2(2, 2), Quaternion.identity);
            timeController.AsignarJugador(jugadorObj.GetComponent<Jugador>());
        }
    }

    public void SalirAlMenu()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.LeaveRoom();  // Abandonar la sala antes de desconectar
        }
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Desconectado de Photon. Causa: " + cause);
    }
}