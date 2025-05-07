using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;
using static UnityEditor.PlayerSettings;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    public Camera _camera;
    public GameObject player;
    public List<Location> touchingLocations;

    Location CurrentLocation => touchingLocations.Count > 0 ? touchingLocations[0] : null;

    private Vector3 destination;

    private float lerpSpeed = 8f;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CurrentLocation == null)
        {
            SetDestinationToPlayer();
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, destination, lerpSpeed * Time.fixedDeltaTime);
            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, 5, lerpSpeed * Time.fixedDeltaTime);
        }
        else
        {
            switch (CurrentLocation.locationType)
            {
                case LocationType.Default:
                    SetDestinationToPlayer();
                    SetCameraIntention(CurrentLocation, destination);
                    break;
                case LocationType.AreaLocked:
                    SetDestinationToPlayer();
                    BoxCollider2D targetLocationCollider = CurrentLocation.boxCollider2D;
                    destination = KeepCameraInLocation(targetLocationCollider, destination);
                    SetCameraIntention(CurrentLocation, destination);
                    break;
                case LocationType.CameraPosFixed:
                    destination = CurrentLocation.transform.position;
                    //destination.z = _camera.transform.position.z;
                    SetCameraIntention(CurrentLocation, destination);
                    break;
            }
        }
    }

    private void SetDestinationToPlayer()
    {
        destination.x = player.transform.position.x;
        destination.y = player.transform.position.y;
        destination.z = _camera.transform.position.z;
    }

    private void SetCameraIntention(Location location, Vector3 destination)
    {      
        float zoomSize = location.ZoomSize;
        
        _camera.transform.position = Vector3.Lerp(_camera.transform.position, destination, lerpSpeed * Time.fixedDeltaTime);
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, zoomSize, lerpSpeed * Time.fixedDeltaTime);
    }

    private Vector3 KeepCameraInLocation(BoxCollider2D targetCollider, Vector3 destination)
    {
        float camHalfHeight = _camera.orthographicSize;
        float camHalfWidth = camHalfHeight * _camera.aspect;

        Vector3 colliderPos = targetCollider.transform.position + (Vector3)targetCollider.offset;

        float colHalfWidth = targetCollider.size.x / 2;
        float colHalfHeight = targetCollider.size.y / 2;

        float minX = colliderPos.x - colHalfWidth;
        float maxX = colliderPos.x + colHalfWidth;
        float minY = colliderPos.y - colHalfHeight;
        float maxY = colliderPos.y + colHalfHeight;


        if (targetCollider.size.x < camHalfWidth * 2)
        {
            destination.x = colliderPos.x;
        }
        else
        {
            destination.x = Mathf.Clamp(destination.x, minX + camHalfWidth, maxX - camHalfWidth);
        }


        if (targetCollider.size.y < camHalfHeight * 2)
        {
            destination.y = colliderPos.y;
        }
        else
        {
            destination.y = Mathf.Clamp(destination.y, minY + camHalfHeight, maxY - camHalfHeight);
        }


        return destination;
    }
}
