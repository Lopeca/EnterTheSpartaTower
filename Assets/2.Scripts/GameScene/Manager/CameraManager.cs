using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    public Camera _camera;
    public GameObject target;
    public BoxCollider2D defaultLocation;
    public BoxCollider2D currentLocation;

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
        Vector3 destination = target.transform.position;
        destination.z = _camera.transform.position.z;

        BoxCollider2D targetLocation = currentLocation != null ? currentLocation : defaultLocation;

        destination = KeepCameraInLocation(targetLocation, destination);

        _camera.transform.position = Vector3.Lerp(_camera.transform.position, destination, lerpSpeed * Time.fixedDeltaTime);
    }

    private Vector3 KeepCameraInLocation(BoxCollider2D targetCollider, Vector3 destination)
    {
        float camHalfHeight = _camera.orthographicSize;
        float camHalfWidth = camHalfHeight * _camera.aspect;

        Vector3 colliderPos = targetCollider.transform.position;

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
