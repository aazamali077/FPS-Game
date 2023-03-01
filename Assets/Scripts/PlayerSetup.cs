using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerSetup : MonoBehaviour
{
   // public PlayerMovement movement;
    public FirstPersonController controller;


    public GameObject cameras;

    public TextMeshPro nickname;

    public Renderer changecolor;



    private void Start()
    {
        changecolor= GetComponent<Renderer>();
    }
    public void IsLocalPlayer()
    {
        //movement.enabled= true;
        controller.enabled= true;
        cameras.SetActive(true);
    }

    [PunRPC]
    public void SetNickName(string _name)
    {
        nickname.text = _name;
    }

    [PunRPC]
    public void ChangeRedColor()
    {
          changecolor.material.color= Color.red;

    }


    [PunRPC]
    public void ChangeGreenColor()
    {
        changecolor.material.color= Color.green;

    }

}
