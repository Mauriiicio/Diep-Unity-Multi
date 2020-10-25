using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ControllerNetwork : MonoBehaviourPunCallbacks
{
    public GameObject Player;
    public GameObject Panel_login;
    
    public InputField InputField_PlayerName;

    public Text txt_ping;


    string nameNull;
    

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        nameNull = "Monomyto" + Random.Range(1, 100);
        InputField_PlayerName.text = nameNull;
    }

    public void PlayGame()
    {
        PhotonNetwork.NickName = InputField_PlayerName.text;

        if (PhotonNetwork.IsConnected)
        {
            string RoomName = "Sala";
            RoomOptions room = new RoomOptions() { MaxPlayers = 10 };
            PhotonNetwork.JoinOrCreateRoom(RoomName, room, TypedLobby.Default);
            Panel_login.gameObject.SetActive(false);
            txt_ping.text = PhotonNetwork.GetPing().ToString();
        }
    }
    






    //Conexão Photon.
    public override void OnConnected()
    {
        Debug.Log("Conectador");
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado Master");
        //PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("Entrou Lobby");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Entrou Sala");
        Debug.Log("Numero de jogadores sala: " + PhotonNetwork.CurrentRoom.PlayerCount);

        
        PhotonNetwork.Instantiate(Player.name, Player.transform.position, Player.transform.rotation, 0);

    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Desconectado: " + cause);
    }

}
