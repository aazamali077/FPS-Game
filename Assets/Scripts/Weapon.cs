using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage;
    public float fireRate;

    private float nextfire;
    public Camera cameras;

    [Header("VFX")]
    public GameObject HitVFX;


    // Update is called once per frame
    void Update()
    {
        if (nextfire > 0)
        {
            nextfire-= Time.deltaTime;
        }

        if (Input.GetButton("Fire1")&&nextfire<=0)
        {
            nextfire= 1/fireRate;

            Fire();

        }
    }

    private void Fire()
    {
        Ray ray = new Ray(cameras.transform.position, cameras.transform.forward);

        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, 100f))
        {
            PhotonNetwork.Instantiate(HitVFX.name, hit.point, Quaternion.identity);
            if (hit.transform.gameObject.GetComponent<Health>())
            {
                hit.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, damage);
            }
        }
        
    }
}
