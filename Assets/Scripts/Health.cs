using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Health : MonoBehaviour
{
    public int health;
    public TMPro.TextMeshProUGUI HealthText;

    [PunRPC]
  public void TakeDamage (int damage)
    {
        health -= damage;
        HealthText.text = health.ToString();
        if (health<=0)
        {
            Destroy(gameObject);
        }
    }

}
