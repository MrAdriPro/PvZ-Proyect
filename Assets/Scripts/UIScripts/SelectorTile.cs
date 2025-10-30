using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectorTile : MonoBehaviour , IPointerClickHandler ,  IPointerEnterHandler, IPointerExitHandler
{
    private int tileIndex = 1;
    
    private RectTransform rectTransform;

    [SerializeField] private PlantData data;
    
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SelectorManager.instanceSelect.SelectPlantIndex(tileIndex);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        print("OnMouseEnter");
        SelectorManager.instanceSelect.viewSelect = tileIndex;

        SelectorManager.instanceSelect.frameSelector.position = rectTransform.position;

        SelectorManager.instanceSelect.SetPlantData(data.plantSprite, data.plantName, data.sunCost, data.damage, data.maxHealth, data.attackRange ,data.description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
}
