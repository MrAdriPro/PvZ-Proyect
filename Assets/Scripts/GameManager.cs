using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] plantPrefabs;
    [SerializeField] private TextMeshProUGUI currentEnergyNum;
    
    public int plantSelector = 0;
    
    public int energy = 100;

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
        //Getting current plant attributes
        PlaceholderPlant plantAtt = plantPrefabs[plantSelector].GetComponent<PlaceholderPlant>();
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
}
