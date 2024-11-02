using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Random = UnityEngine.Random;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private ARPlaneManager _planeManager;
    [SerializeField] private GameObject _objectPrefab;
    [SerializeField] private float _spawnPeriod;
    [SerializeField] private float _minimunSpawnArea;
    
    private List<ARPlane> _planes = new ();
    private float _time;

    private void OnEnable()
    {
        _planeManager.planesChanged += OnPlanesChanged;
    }

    private void OnDisable()
    {
        _planeManager.planesChanged -= OnPlanesChanged;
    }

    private void Update()
    {
        _time += Time.deltaTime;
        if (_time >= _spawnPeriod)
        {
            if (_planes.Count == 0)
                return;
            _time = 0;
            Instantiate(_objectPrefab, GetRandomPosition(), Quaternion.identity);

        }
    }

    private void OnPlanesChanged(ARPlanesChangedEventArgs context)
    {
        foreach (var plane in context.added)
        {
            if (plane.size.x * plane.size.y >= _minimunSpawnArea)
             _planes.Add(plane);
        }

        foreach (var plane in context.removed)
        {
            if (_planes.Contains(plane))
             _planes.Remove(plane);
        }
    }

    private Vector3 GetRandomPosition()
    {
        ARPlane RandomPlane = _planes[Random.Range(0, _planes.Count)];
        float randomX = Random.Range(RandomPlane.center.x - RandomPlane.extents.x, RandomPlane.center.x + RandomPlane.extents.x);
        float randomY = Random.Range(RandomPlane.center.y - RandomPlane.extents.y, RandomPlane.center.y + RandomPlane.extents.y);

        
        return new Vector3(randomX, randomY, RandomPlane.center.z);
        
    }
}
