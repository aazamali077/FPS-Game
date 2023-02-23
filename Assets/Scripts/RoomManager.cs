using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager instance;
    public GameObject player;
    [Space] 
    public Transform[] spawnpoints;

    public GameObject RoomCam;

    [Space]
    public GameObject nameUI;
    public GameObject connectingUI;

    private string nickname = "No Name";

    private void Awake()
    {
        instance = this;
    }

    public void ChangeNickname(string _name)
    {
        nickname= _name;    
    }

    public void JoinRoomButtonPressed()
    {
        Debug.Log("Connecting...");
        PhotonNetwork.ConnectUsingSettings();

        nameUI.SetActive(false);
        connectingUI.SetActive(true);
    }

    void Start()
    {
        
     
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected to master");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        PhotonNetwork.JoinOrCreateRoom("test", null, null);

        Debug.Log("We are in a room now");
        
    }

    public override void OnJoinedRoom()
    {
        RoomCam.SetActive(false);

        RespawnPlayer();
        base.OnJoinedRoom();
        
    }

    public void RespawnPlayer()
    {
        Transform spawnpoint = spawnpoints[Random.Range(0, spawnpoints.Length)];
        GameObject _player = PhotonNetwork.Instantiate(player.name, spawnpoint.position, Quaternion.identity);
        _player.GetComponent<PlayerSetup>().IsLocalPlayer();
        _player.GetComponent<Health>().islocalplayer = true;

        _player.GetComponent<PhotonView>().RPC("SetNickName", RpcTarget.AllBuffered, nickname);
    }


}
