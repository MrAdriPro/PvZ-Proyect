using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class SunCollectable : MonoBehaviour, IPointerClickHandler
{
    
    [Header("general")]
    public int sunValue = 50;
    public float gravityForce = 9.8f;
    public bool sunFlower = false;

    [Header("MaxRandomFall")] 
    private bool randomSetted = false;
    public float maxRandomFall = 5f; 
    public float minRandomFall = 10f;
    private float activeTimer = 1000f;

    [Header("SunflowerVar")] 
    
    private bool _ascend = true;
    
    public float ascendForce = 8f;
    public float descendForce = 5f;
    
    public float ascendTimer = 1f;
    public float descendTimer = 2f;
    
    private bool _sideMovement = true;
    private bool randomSideSetted = false;
    public float maxSideForce = 0.5f;
    public float minSideForce = -0.5f;
    private float sideForce = 0f;

        

    void Update()
    {
        if (sunFlower == false)
        { 
            SunDrop();
        }
        else
        {
            SunFlowerDrop();
        }
    }

    void SunDrop()
    {
        bool fallActive = true;
        
        if (randomSetted == false)
        {
            activeTimer = Random.Range(minRandomFall, maxRandomFall);
            randomSetted = true;
        }
        
        if (fallActive && activeTimer >= 0)
        {
            activeTimer -= Time.deltaTime;
            transform.Translate(Vector3.down * (gravityForce * Time.deltaTime));
        }
        
        if (activeTimer <= 0)
        {
            print("Caída terminada");
            fallActive = false;
        }
    }

    void SunFlowerDrop()
    {
        
        if (randomSideSetted == false)
        {
            sideForce = Random.Range(minSideForce, maxSideForce);
            randomSideSetted = true;
        }
        
        if (_ascend == true && ascendTimer > 0)
        {
            transform.Translate(Vector2.up * (ascendForce * Time.deltaTime));
            ascendTimer -= Time.deltaTime;
        }
        else if (_ascend == false && descendTimer > 0)
        {
            transform.Translate(Vector2.down * (descendForce * Time.deltaTime));
            descendTimer -= Time.deltaTime;
        }
        else
        {
            _sideMovement = false;
        }

        if (_sideMovement == true)
        {
            transform.Translate(Vector2.right * (sideForce * Time.deltaTime));
        }
        
        if (ascendTimer < 0)
        {
            _ascend = false;
        }
    }


    void OnMouseDown()
    {
        print("Sun Collectable");
        GameManager.instance.AddEnergy(sunValue);
        
        Destroy(gameObject);
    }
    

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick");
    }
}
