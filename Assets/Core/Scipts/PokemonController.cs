using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Assuming the collision is with a Pokeball
        if (collision.gameObject.GetComponent<Pokeball>() != null)
        {
            Debug.Log("Pokémon hit by Pokeball, destroying Pokémon.");
            Destroy(gameObject);
            Destroy(collision.gameObject); // Destroy the Pokeball as well
        }
    }
}













