using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var zombie = other.attachedRigidbody.GetComponent<Zombie>();
        if (zombie != null)
            zombie.Die();
        
    }
}
