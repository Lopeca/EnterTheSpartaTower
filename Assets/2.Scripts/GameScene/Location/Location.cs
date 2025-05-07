using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LocationType
{
    Default,
    AreaLocked,
    CameraPosFixed
}
public class Location : MonoBehaviour
{
    public BoxCollider2D boxCollider2D;
    public Color layerColor;

    public LocationType locationType;

    [SerializeField] private float zoomSize;
    public float ZoomSize => zoomSize;  



    void OnDrawGizmosSelected()
    {
        Gizmos.color = layerColor; // 연두 투명
        Gizmos.DrawCube(transform.position+(Vector3)boxCollider2D.offset, boxCollider2D.size);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CameraManager.Instance.touchingLocations.Add(this);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CameraManager.Instance.touchingLocations.Remove(this);
        }
    }
}
