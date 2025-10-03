using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class SunCollectable : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Rigidbody2D _rb2d;
    
    [Header("general")]
    public int sunGained = 50;
    public float gravityForce = 9.8f;
    public bool sunFlower = false;

    
    [Header("Jump variables")]
    public float jumpForce = 4f;
    public float jumpTimer = 1f;
    public float jumpTimerInitial = 1f;
    
    private bool sunFalling = false;

    
    private bool addForced = false;

    [Header("Fall variables")] 
    public float initialFallTimer = 3f;
    public float fallTimer = 3f;
    
    private bool sunDropping = true;
    private bool addedFall = false;

    [Header("MaxRandomFall")] 
    private bool randomSetted = false;
    public float maxRandomFall = 5f; 
    public float minRandomFall = 10f;
    private float activeTimer = 1000f;
    
    

    void Update()
    {
        SunDrop();
        // if (Input.GetMouseButtonDown(0))
        // {
        //     Vector3 mousePos = Input.mousePosition;
        //     Ray ray = Camera.main.ScreenPointToRay(mousePos);
        //
        //     if (Physics.Raycast(ray, out RaycastHit hit))
        //     {
        //         GameManager.instance.AddEnergy(sunGained);
        //         Destroy(gameObject);
        //     }
        //     
        //     
        // }
        
        //Sun spawned by sunflowers
        // if (sunFlower == true)
        // {
        //     if (sunFalling == true)
        //     {
        //         AddGravity();
        //     }
        //     else if (sunFalling == false && jumpTimer > 0f)
        //     {
        //         jumpTimer -= Time.deltaTime;
        //         if (addForced == false)
        //         {
        //             _rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        //             addForced = true;
        //         }
        //     }
        //
        //     if (jumpTimer <= 0f)
        //     {
        //         sunFalling = true;
        //         StartCoroutine(ForceTimer());
        //         jumpTimer = jumpTimerInitial;
        //         addForced = false;
        //     }
        // }
        // else
        // {
        //     if (sunDropping == true && jumpTimer > 0f)
        //     {
        //         fallTimer -= Time.deltaTime;
        //         if (addedFall == false)
        //         {
        //             _rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        //             addedFall = true;
        //         }
        //     }
        //     else if (fallTimer <= 0f)
        //     {
        //         sunDropping = false;
        //         if (addedFall == true)
        //         {
        //             _rb2d.AddForce(new Vector2(0, -jumpForce), ForceMode2D.Impulse);
        //             addedFall = false;
        //         }
        //     }
        // }


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
        
    }


    void OnMouseDown()
    {
        print("Sun Collectable");
        GameManager.instance.AddEnergy(sunGained);
        
        Destroy(gameObject);
    }

    IEnumerator ForceTimer()
    {
        yield return new WaitForSeconds(jumpTimer); 
        _rb2d.AddForce(new Vector2(0, -jumpForce), ForceMode2D.Impulse);

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick");
    }
}
