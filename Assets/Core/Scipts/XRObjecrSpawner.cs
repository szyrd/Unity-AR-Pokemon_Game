using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit;

public class XRObjecrSpawner : MonoBehaviour
{
    [SerializeField] private XRRayInteractor _interactor;
    [SerializeField] private XRScreenSpaceController _controller;
    
    [SerializeField] private GameObject _spawnPrefab;

    // Update is called once per frame
    void Update()
    {
        
        if(_controller.pinchStartPositionAction.action.WasPressedThisFrame())
            return;
        
        if (_interactor.TryGetCurrentARRaycastHit(out ARRaycastHit hit))
        {
            if (hit.trackable is not ARPlane)
                return;
            
            if (_controller.tapStartPositionAction.action.WasPressedThisFrame())
            {
                Instantiate(_spawnPrefab, hit.pose.position, hit.pose.rotation);
            }
            
            if (_controller.twistStartPositionAction.action.IsPressed())
            {
                float twistAmount = _controller.twistDeltaRotationAction.action.ReadValue<float>();
                _spawnPrefab.transform.Rotate(Vector3.up, twistAmount);
            }

            // Scaling Handling
            if (_controller.pinchGapAction.action.IsPressed())
            {
                float pinchDelta = _controller.pinchGapDeltaAction.action.ReadValue<float>();
                float scalingFactor = 1 + pinchDelta * 0.01f;
                _spawnPrefab.transform.localScale *= scalingFactor;
            }
            
        }
        
    }
}
