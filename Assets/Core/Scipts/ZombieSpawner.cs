using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Random = UnityEngine.Random;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private ARPlaneManager _planeManager;
    [SerializeField] private GameObject _zombiePrefab;
    [SerializeField] private float _spawnPeriod;
    [SerializeField] private float _minimumSpawnArea;

    private List<ARPlane> _planes = new();
    private float _time;

    private void OnEnable()
    {
        _planeManager.planesChanged += OnPlaneschanged;
    }
    
    private void OnDisable()
    {
        _planeManager.planesChanged -= OnPlaneschanged;
    }

    private void Update()
    {
        _time += Time.deltaTime;
        if (_time > _spawnPeriod)
        {
            if (_planes.Count == 0)
                return;
            _time = 0;
            Instantiate(_zombiePrefab, GetRandomPosition(), Quaternion.identity);
            
        }
    }

    private void OnPlaneschanged(ARPlanesChangedEventArgs context)
    {
        foreach (var plane in context.added)
        {
            if (plane.size.x * plane.size.y > _minimumSpawnArea)
                _planes.Add(plane);
        }

        foreach (var plane in context.removed)
        {
            if(_planes.Contains(plane))
                _planes.Remove(plane);
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 randomPosition;
        ARPlane randomPlane = _planes[Random.Range(0, _planes.Count)];
        float RandomX = Random.Range(randomPlane.center.x - randomPlane.extents.x, randomPlane.center.x + randomPlane.extents.x);
        float RandomY = Random.Range(randomPlane.center.y - randomPlane.extents.y, randomPlane.center.y + randomPlane.extents.y);

        return new Vector3(RandomX, RandomY, randomPlane.center.z);

    }
}
