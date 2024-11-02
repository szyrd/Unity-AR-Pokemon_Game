using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PokemonSpawner : MonoBehaviour
{
    [SerializeField] private ARPlaneManager planeManager; // AR Plane Manager to manage planes
    [SerializeField] private GameObject[] pokemonPrefabs; // Array of Pokemon prefabs to instantiate randomly
    
    public void SpawnPokemonAtPosition(Vector3 position)
    {
        
        if (pokemonPrefabs.Length == 0)
        {
            Debug.LogError("No Pokémon prefabs assigned to the PokemonSpawner!");
            return;
        }

        // Pick a random Pokémon prefab from the array
        int randomPokemonIndex = Random.Range(0, pokemonPrefabs.Length);
        GameObject pokemonPrefab = pokemonPrefabs[randomPokemonIndex];

        // Instantiate the Pokémon at the specified position
        Instantiate(pokemonPrefab, position, Quaternion.identity);
        Debug.Log("Pokémon spawned at position: " + position);
    }
    
    public void SpawnPokemon()
    {
        Debug.Log("Attempting to spawn a Pokemon...");
        List<ARPlane> planes = new List<ARPlane>();
        
        foreach (ARPlane plane in planeManager.trackables)
        {
            planes.Add(plane);
        }

        if (planes.Count > 0)
        {
            // Pick a random plane
            int randomPlaneIndex = Random.Range(0, planes.Count);
            ARPlane randomPlane = planes[randomPlaneIndex];

            // Pick a random Pokemon prefab from the array
            int randomPokemonIndex = Random.Range(0, pokemonPrefabs.Length);
            GameObject pokemonPrefab = pokemonPrefabs[randomPokemonIndex];

            // Spawn the Pokemon at the center of the plane
            Vector3 spawnPosition = randomPlane.center;
            Instantiate(pokemonPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("Pokemon spawned successfully.");
        }
        else
        {
            Debug.LogWarning("No planes available to spawn Pokemon.");
        }
    }
}












