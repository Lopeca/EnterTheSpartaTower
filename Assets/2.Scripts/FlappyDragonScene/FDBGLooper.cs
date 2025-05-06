using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDBGLooper : MonoBehaviour
{
    private const float floorRepositionDistance = 40;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Floor"))
        {
            Vector3 floorPos = collision.transform.position;
            floorPos.x += floorRepositionDistance;
            collision.transform.position = floorPos;
        }
        else if (collision.CompareTag("Obstacle") || collision.CompareTag("Item"))
        {
            Destroy(collision.gameObject);
        }
    }
}
