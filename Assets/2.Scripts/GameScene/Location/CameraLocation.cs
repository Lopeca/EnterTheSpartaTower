using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLocation : MonoBehaviour
{
    public BoxCollider2D boxCollider2D;
    public Color layerColor;
    void OnDrawGizmosSelected()
    {
        Gizmos.color = layerColor; // ���� ����
        Gizmos.DrawCube(transform.position, boxCollider2D.size);
    }
}
