using System;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class SelectorManager : MonoBehaviour
{
     
    public static SelectorManager instanceSelect;

    public RectTransform frameSelector;
    
    public int selectedIndex = 0;
    public int viewSelect;
    
    public bool hardSelect = false;
    
    
    [Header("PlantData")]
    
    [SerializeField] private Image plantImage;
    
    [SerializeField] private TextMeshProUGUI plantNameText;
    [SerializeField] private TextMeshProUGUI plantCostText;
    [SerializeField] private TextMeshProUGUI plantDamageText;
    [SerializeField] private TextMeshProUGUI plantHealthText;
    [SerializeField] private TextMeshProUGUI plantRangeText;
    [SerializeField] private TextMeshProUGUI plantDescriptionText;
    
    private void Awake()
    {
        if (instanceSelect == null)
        {
            instanceSelect = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectPlantIndex(int index)
    {
        selectedIndex = index;
    }

    public void SetPlantData(Sprite setSprite, string setName, int setCost, int setDamage, float setHealth, float setRange, string setDescription)
    {
        plantImage.sprite = setSprite;
        
        plantNameText.text = setName;
        plantCostText.text = setCost.ToString();
        plantDamageText.text = setDamage.ToString();
        plantHealthText.text = setHealth.ToString();
        plantRangeText.text = setRange.ToString();
        plantDescriptionText.text = setDescription;
    }
}
