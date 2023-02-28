using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medikit : MonoBehaviour
{
    public int MediKits;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered: "+ other.name);
        other.gameObject.GetComponent<Health>().AddHealth(MediKits);
        Destroy(this.gameObject);
    }
}
