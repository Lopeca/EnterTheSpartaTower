using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLocation : MonoBehaviour
{
    public BoxCollider2D boxCollider2D;
    public Color layerColor;
    void OnDrawGizmosSelected()
    {
        Gizmos.color = layerColor; // 연두 투명
        Gizmos.DrawCube(transform.position+(Vector3)boxCollider2D.offset, boxCollider2D.size);
    }
}
