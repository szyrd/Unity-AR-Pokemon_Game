using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARObjectSpawner : MonoBehaviour
{
    [SerializeField] private ARRaycastManager _raycastManager;
    [SerializeField] private GameObject _spawnedObject;
    private List<ARRaycastHit> _hits = new();
    private void Update()
    {
        if (!Pointer.current.press.wasPressedThisFrame) return;
        {
            _raycastManager.Raycast(Pointer.current.position.value, _hits, TrackableType.PlaneWithinPolygon);
            foreach (var hit in _hits)
            {
                Debug.Log(hit.trackable, hit.trackable.gameObject);
                Instantiate(_spawnedObject).transform.SetWorldPose(hit.pose);
                break;
            }
        }

        {
            
        }
    }
}
