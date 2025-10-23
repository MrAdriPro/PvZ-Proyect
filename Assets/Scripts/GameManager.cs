using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] GameObject[] plantPrefabs;
    [SerializeField] private TextMeshProUGUI currentEnergyNum;
    
    //public Dictionary<string, GameObject> dictio  = new Dictionary<string, GameObject>();    
    
    public int plantSelector = 0;
    
    public int energy = 100;
    
    public bool gameStarted = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        //Update energy number on interface
        currentEnergyNum.text = energy.ToString();
    }

    public void SetPlant(int plant)
    {
        plantSelector = plant;
    }

    public bool generatePlant(Vector2 plantPos)
    {
        if (!gameStarted)
            return false;

        if (plantSelector <= 0 || plantSelector >= plantPrefabs.Length)
        {
            Debug.LogWarning($"generatePlant: plantSelector inválido ({plantSelector}).");
            return false;
        }

        GameObject prefab = plantPrefabs[plantSelector];
        if (prefab == null)
        {
            Debug.LogWarning($"generatePlant: prefab en el índice {plantSelector} es null.");
            return false;
        }

        PlantController plantAtt = prefab.GetComponent<PlantController>();
        if (plantAtt == null)
        {
            Debug.LogWarning($"generatePlant: el prefab '{prefab.name}' no tiene PlantController.");
            return false;
        }

        if (plantAtt.data == null)
        {
            Debug.LogWarning($"generatePlant: PlantData no asignado en el PlantController del prefab '{prefab.name}'.");
            return false;
        }

        int energySpent = plantAtt.data.sunCost;

        if (energySpent <= energy)
        {
            Instantiate(prefab, plantPos, Quaternion.identity);
            energy -= energySpent;
            plantSelector = 0;
            Debug.Log("plant spawned");
            return true;
        }

        Debug.Log("plant failed to spawn: not enough energy");
        return false;
    }

    public void AddEnergy(int sunGained)
    {
        energy += sunGained;
    }
}
