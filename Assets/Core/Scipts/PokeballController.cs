using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PokeballController : MonoBehaviour
{
    [SerializeField] private GameObject _pokeballPrefab; // Pokeball prefab to instantiate
    [SerializeField] private float _strength; 

    private void Update()
    {
        // Shoot the Pokeball when the screen is touched
        if (Pointer.current.press.wasPressedThisFrame)
        {
            var pokeball = Instantiate(
                _pokeballPrefab,
                Camera.main.transform.position,
                Camera.main.transform.rotation);
            pokeball.GetComponent<Rigidbody>().AddForce(pokeball.transform.forward * _strength, ForceMode.VelocityChange);

        }
    }
}











