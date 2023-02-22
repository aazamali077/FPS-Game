using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Health : MonoBehaviour
{
    public int health;
    public bool islocalplayer;
    public TMPro.TextMeshProUGUI HealthText;

    [PunRPC]
  public void TakeDamage (int damage)
    {
        health -= damage;
        HealthText.text = health.ToString();
        if (health<=0)
        {
            if(islocalplayer)
            RoomManager.instance.RespawnPlayer();


            Destroy(gameObject);
        }
    }

}
