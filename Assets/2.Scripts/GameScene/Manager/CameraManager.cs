using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    public Camera _camera;
    public GameObject target;

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
        _camera.transform.position = Vector3.Lerp(_camera.transform.position, destination, lerpSpeed * Time.fixedDeltaTime);
    }
}
