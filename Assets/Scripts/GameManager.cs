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
        if (gameStarted == true)
        {
            //Getting current plant attributes
            PlantController plantAtt = plantPrefabs[plantSelector].GetComponent<PlantController>();
            int energySpent = plantAtt.plantCost;

            if (energySpent <= energy && plantSelector != 0)
            {
                //Generate plant in tileposition and spend energyCost previously extracted from plant
                Instantiate(plantPrefabs[plantSelector], plantPos, Quaternion.identity);
                energy -= energySpent;

                plantSelector = 0;
                print("plant spawned");
                return true;
            }
            else print("plant failed to spawn");

            return false;
        }
        else return false;
    }

    public void AddEnergy(int sunGained)
    {
        energy += sunGained;
    }
}
