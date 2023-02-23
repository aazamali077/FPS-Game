using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    public int damage;
    public float fireRate;

    private float nextfire;
    public Camera cameras;

    [Header("VFX")]
    public GameObject HitVFX;

    [Header("Ammo")]
    public int mag = 5;
    public int ammo = 30;
    public int magAmmo = 30;

    [Header("Ammo UI")]
    public TextMeshProUGUI MagText;
    public TextMeshProUGUI AmmoText;

    [Header("Animation")]
    public Animation ReloadAnimation;
    public AnimationClip ReloadClip;

    [Header("Recoil")]
    //[Range(0, 1)]
    //public float Recoilpercent = 0.3f;
    [Range(0, 2)]
    public float Recoverpercent = 0.7f;
    [Space]
    public float RecoilUp = 1f;
    public float RecoilBack = 0f;

    private Vector3 originalposition;
    private Vector3 recoilVelocity = Vector3.zero;

    private float recoilLength;
    private float recoverLength;

    private bool recoiling;
    private bool recovering;
    



    private void Start()
    {
       
        MagText.text = mag.ToString();
        AmmoText.text = ammo + "/" + magAmmo;

        originalposition = transform.localPosition;

        recoilLength =0;
        recoverLength=1/ fireRate * Recoverpercent;

    }
    void Update()
    {
        if (nextfire > 0)
        {
            nextfire-= Time.deltaTime;
        }

        if (Input.GetButton("Fire1")&&nextfire<=0&&ammo>0&&ReloadAnimation.isPlaying == false)
        {
            nextfire= 1/fireRate;
            ammo--;

            MagText.text = mag.ToString();
            AmmoText.text = ammo + "/" + magAmmo;

            Fire();
        }

        if (Input.GetKeyDown(KeyCode.R)&&mag>0&&ammo==0)
        {
            StartCoroutine(Reload());
        }

        if (recoiling)
        {
            Recoil();
        }

        if (recovering)
        {
            Recovering();
        }
    }

    private void Fire()
    {

        recoiling= true;
        recovering= false;

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

    IEnumerator Reload()
    {
        if (mag > 0 && ammo == 0) ReloadAnimation.Play(ReloadClip.name);
        yield return new WaitForSeconds(3f);
       
        if (mag > 0&&ammo==0)
        {
            
            mag--;
            ammo = magAmmo;
        }
        MagText.text = mag.ToString();
        AmmoText.text = ammo+"/"+magAmmo;
    }


    void Recoil()
    {
        Vector3 finalPosition = new Vector3(originalposition.x, originalposition.y+RecoilUp, originalposition.z-RecoilBack);

        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, finalPosition,ref recoilVelocity, recoilLength);

        if (transform.localPosition == finalPosition)
        {
            recoiling= false;
            recovering= true;
        }
    }

    void Recovering()
    {
        Vector3 finalPosition = originalposition;

        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, finalPosition, ref recoilVelocity, recoverLength);

        if (transform.localPosition == finalPosition)
        {
            recoiling = false;
            recovering = false;
        }
    }
}
