using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class TilePrefabPair
{
    public TileBase tile;
    public GameObject prefab;
}
public class TileToPrefab : MonoBehaviour
{
    public Tilemap tileMap;
    public List<TilePrefabPair> tileDB;
    // Start is called before the first frame update
    void Start()
    {
        // 타일맵 영역. 보이지 않는 영역은 Rect Tool 상태로 눌러서 확인하기
        BoundsInt bounds = tileMap.cellBounds;

        for (int x = bounds.xMin; x <= bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y <= bounds.yMax; y++)
            {
                Vector3Int pos = new Vector3Int(x, y);
                TileBase tile = tileMap.GetTile(pos);

                GameObject obj = GetPrefabFromTile(tile);
                if(obj != null)
                {
                    Vector3 worldPos = tileMap.CellToWorld(pos) + tileMap.tileAnchor;
                    Instantiate(obj, worldPos, Quaternion.identity, transform);
                }
            }
        }

        Destroy(tileMap.gameObject);
    }

    private GameObject GetPrefabFromTile(TileBase tile)
    {
        for (int i = 0; i < tileDB.Count; i++)
        {
            if (tileDB[i].tile == tile)
                return tileDB[i].prefab;
        }
        return null;
    }
}
