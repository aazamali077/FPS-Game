using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    public PlayerMovement movement;

    public GameObject cameras;

    public void IsLocalPlayer()
    {
        movement.enabled= true;
        cameras.SetActive(true);
    }

}
