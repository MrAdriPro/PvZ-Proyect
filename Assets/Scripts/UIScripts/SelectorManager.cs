using System;
using UnityEngine;
using System.Collections.Generic;

public class SelectorManager : MonoBehaviour
{
    public GameObject[] deckSelector;

    public GameObject plant1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPlantToDeck(int index, GameObject plant)
    {
        deckSelector[index] = plant;
    }
}
