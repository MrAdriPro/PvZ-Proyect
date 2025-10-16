using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SunRainManager : MonoBehaviour
{
    [SerializeField] private GameObject sunPrefab;

    [Header("Values")]
    public Vector2 minDistance;
    public Vector2 maxDistance;
    private float _spawnPosY;
    
    public float rainRate = 2f;
    public float rainTimer = 0f;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        
        Gizmos.DrawLine(minDistance, maxDistance);
    }

    void Start()
    {
       
    }

    void Update()
    {
        float minPos = minDistance.x;
        float maxPos = maxDistance.x;
        
        if (rainTimer % rainRate == 0)
        {
            _spawnPosY = Random.Range(minPos, maxPos);
        }

        if (GameManager.instance.gameStarted == true)
        {
            rainTimer += Time.deltaTime;

            if (rainTimer >= rainRate)
            {
                print("SunInstantiated");
                Instantiate(sunPrefab, new Vector2(_spawnPosY, transform.position.y), Quaternion.identity);
                rainTimer = 0;
            }
        }
    } 
}
