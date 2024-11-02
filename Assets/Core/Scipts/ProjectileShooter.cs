using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Pointer = UnityEngine.InputSystem.Pointer;

public class ProjectileShooter : MonoBehaviour
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _strength;
    
    void Update()
    {
        if (Pointer.current.press.wasPressedThisFrame)
        {
            var projectile = Instantiate(
                _projectile, 
                Camera.main.transform.position, 
                Camera.main.transform.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * _strength, ForceMode.VelocityChange);
        }
        
    }
}
