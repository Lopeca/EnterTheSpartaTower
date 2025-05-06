using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDCameraManager : MonoBehaviour
{
    public GameObject player;

    private Vector2 posDiff;
    private float posDiffX;

    public void Init(GameObject player)
    {
        transform.position = new Vector3(0, 0, -10);
        this.player = player;
        posDiff = transform.position - player.transform.position;
        posDiffX = posDiff.x;
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            transform.position = new Vector3(player.transform.position.x + posDiffX, transform.position.y, transform.position.z);
        }

    }
}
