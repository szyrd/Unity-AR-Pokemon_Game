using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;

public class TrackImageVisuals : MonoBehaviour
{
    [SerializeField]
    private ARTrackedImageManager _TrackedImageManager;

    [SerializeField] 
    private List<GameObject> _gameObjectsList;

    private Dictionary<ARTrackedImage, GameObject> _createdObjects = new();

    void OnEnable() => _TrackedImageManager.trackedImagesChanged += OnChanged;

    void OnDisable() => _TrackedImageManager.trackedImagesChanged -= OnChanged;

    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage newImage in eventArgs.added)
        {
            createGmaeObject(newImage);
        }

        foreach (ARTrackedImage updatedImage in eventArgs.updated)
        {
            updateObjects(updatedImage);
        }

        foreach (ARTrackedImage removedImage in eventArgs.removed)
        {
            // Handle removed event
        }
    }

    private void updateObjects(ARTrackedImage image)
    {
        _createdObjects[image].transform.SetPositionAndRotation(image.transform.position, image.transform.rotation);
    }

    private void createGmaeObject(ARTrackedImage image)
    {
        var prefab = _gameObjectsList.First(p => p.name == image.referenceImage.name);
        var newObject = Instantiate(prefab);
        _createdObjects[image] = newObject;
    }
}
