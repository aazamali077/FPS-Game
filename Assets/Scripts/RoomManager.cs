using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

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
    public GameObject TeamJoinUI;
    public GameObject WaitingUI;



    [Space]
    public Button RedTeambtn, GreenTeambtn;
    private string nickname = "No Name";

   
    private bool isred;
    private bool isgreen;
    private bool isbothplayerconnected;

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
      
        PhotonNetwork.ConnectUsingSettings();

        nameUI.SetActive(false);
        WaitingUI.SetActive(true);
    }

    void Start()
    {
        isgreen= false;
        isred= false;
        isbothplayerconnected = false;


        RedTeambtn.onClick.AddListener(() =>
        {
            isred= true;
            TeamJoinUI.SetActive(false);
            nameUI.SetActive(true);
        });
        GreenTeambtn.onClick.AddListener(() =>
        {
            isgreen = true;
            TeamJoinUI.SetActive(false);
            nameUI.SetActive(true);
        });
     
    }

    private void Update()
    {
        if (isbothplayerconnected)
        {
            RoomCam.SetActive(false);
            RespawnPlayer();
            isbothplayerconnected = false;
        }
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
        

        if (PhotonNetwork.PlayerList.Length==2)
        {
            isbothplayerconnected=true;
            WaitingUI.SetActive(false);
            connectingUI.SetActive(true);
            RoomCam.SetActive(false);
            //RespawnPlayer();
        }
        else 
        {
            isbothplayerconnected=false;
            connectingUI.SetActive(false);
            WaitingUI.SetActive(true);
        }
        
        base.OnJoinedRoom();
        
    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            isbothplayerconnected = true;
           
            
            
        }
    }







    public void RespawnPlayer()
    {
        Transform spawnpoint =transform;
        if (isred)
        {
            spawnpoint = spawnpoints[0];
        }
        else if(isgreen)
        {
            spawnpoint = spawnpoints[1];
        }
        GameObject _player = PhotonNetwork.Instantiate(player.name, spawnpoint.position, Quaternion.identity);
        _player.GetComponent<PlayerSetup>().IsLocalPlayer();
        _player.GetComponent<Health>().islocalplayer = true;

        _player.GetComponent<PhotonView>().RPC("SetNickName", RpcTarget.AllBuffered, nickname);
    }


}
