using System;
using System.Collections.Generic;
using UnityEngine;

public class GridProducer : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    
    [SerializeField] private GameObject _tilePrefab;
    
    [SerializeField] private GameObject _tileParent;

    private float prefabSize;

    private Vector2 worldPos;

    //private Dictionary<Vector2, Tile> _tiles;

    private void Awake()
    {
        prefabSize = _tilePrefab.transform.localScale.x;
        
        GenerateGrid();
    }

    void GenerateGrid()
    {
        //_tiles = new Dictionary<Vector2, Tile>();
        
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector2(x * prefabSize, y * prefabSize), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.transform.parent = _tileParent.transform;
                
                Tile tile = spawnedTile.GetComponent<Tile>();
                
                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                tile.Init(isOffset);
                
                //_tiles[new Vector2(x, y)] = spawnedTile;
            }
        }
    }
}
