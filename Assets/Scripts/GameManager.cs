using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] GameObject[] plantPrefabs;
    [SerializeField] private TextMeshProUGUI currentEnergyNum;

    public int plantSelector = 0;

    public int energy = 100;

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }
    }

    void Update()
    {
        currentEnergyNum.text = energy.ToString();
    }

    public void SetPlant(int plant)
    {
        plantSelector = plant;
    }

    public bool generatePlant(Vector2 plantPos)
    {
        if (plantSelector == 0 || plantSelector >= plantPrefabs.Length)
        {
            print("Selección de planta inválida.");
            return false;
        }

        PlantController plantControllerPrefab = plantPrefabs[plantSelector].GetComponent<PlantController>();

        if (plantControllerPrefab == null || plantControllerPrefab.data == null)
        {
            Debug.LogError("El prefab de la planta no tiene PlantController o PlantData asignado.");
            return false;
        }

        int energySpent = plantControllerPrefab.data.sunCost;

        if (energySpent <= energy)
        {
            Instantiate(plantPrefabs[plantSelector], plantPos, Quaternion.identity);
            energy -= energySpent;

            plantSelector = 0;
            print("plant spawned");
            return true;
        }
        else
        {
            print("plant failed to spawn: Not enough energy.");
            return false;
        }
    }

    public void AddEnergy(int sunGained)
    {
        energy += sunGained;
    }
}

