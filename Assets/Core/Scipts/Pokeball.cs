using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Pokeball : MonoBehaviour
{
    [SerializeField] private PokemonSpawner pokemonSpawner; // Reference to the PokemonSpawner script
    private Vector3 _collisionPosition; // Position where the collision happened

    private void Start()
    {
        // Find the PokemonSpawner in the scene and set the reference
        pokemonSpawner = FindObjectOfType<PokemonSpawner>();
        if (pokemonSpawner == null)
        {
            Debug.LogError("PokemonSpawner not found in the scene!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Handle collision with AR Plane
        if (collision.gameObject.GetComponent<ARPlane>() != null)
        {
            _collisionPosition = collision.contacts[0].point; // Get the collision point
            Invoke(nameof(HandleCollision), 2f); // Call HandleCollision after 2 seconds
        }

        // Handle collision with Pokémon
        Pokemon collidedPokemon = collision.gameObject.GetComponent<Pokemon>();
        if (collidedPokemon != null)
        {
            // Destroy both the Pokeball and the Pokémon immediately
            Destroy(collidedPokemon.gameObject);
            Destroy(gameObject);
        }
    }

    private void HandleCollision()
    {
        // Destroy the Pokeball
        Destroy(gameObject);

        // Spawn a Pokemon at the collision position
        if (pokemonSpawner != null)
        {
            pokemonSpawner.SpawnPokemonAtPosition(_collisionPosition);
        }
    }
}








