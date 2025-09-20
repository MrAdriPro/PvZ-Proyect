using System;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    
    [SerializeField] private GameObject _tilePrefab;

    private float prefabSize;

    private Vector2 worldPos;

    private void Start()
    {
        prefabSize = _tilePrefab.transform.localScale.x;
        
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector2(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                
                Tile tile = spawnedTile.GetComponent<Tile>();
                
                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                tile.Init(isOffset);
            }
        }
    }
}
