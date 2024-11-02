using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public void Die()
    {
        Destroy(gameObject);
    }
}
