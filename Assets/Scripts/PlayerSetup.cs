using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerSetup : MonoBehaviour
{
    //public PlayerMovement movement;

    public GameObject cameras;

    public TextMeshPro nickname;

    public void IsLocalPlayer()
    {
        //movement.enabled= true;
        GetComponent<FirstPersonController>().enabled = true;
        cameras.SetActive(true);
    }

    [PunRPC]
    public void SetNickName(string _name)
    {
        nickname.text = _name;
    }

}
