using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SunCollectable : MonoBehaviour
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
    

    void Update()
    {
        //Sun spawned by sunflowers
        if (sunFlower == true)
        {
            if (sunFalling == true)
            {
                AddGravity();
            }
            else if (sunFalling == false && jumpTimer > 0f)
            {
                jumpTimer -= Time.deltaTime;
                if (addForced == false)
                {
                    _rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                    addForced = true;
                }
            }

            if (jumpTimer <= 0f)
            {
                sunFalling = true;
                StartCoroutine(ForceTimer());
                jumpTimer = jumpTimerInitial;
                addForced = false;
            }
        }
        else
        {
            if (sunDropping == true && jumpTimer > 0f)
            {
                fallTimer -= Time.deltaTime;
                if (addedFall == false)
                {
                    _rb2d.AddForce(new Vector2(0, gravityForce), ForceMode2D.Impulse);
                    addedFall = true;
                }
            }
            else if (fallTimer <= 0f)
            {
                sunDropping = false;
                if (addedFall == true)
                {
                    _rb2d.AddForce(new Vector2(0, -gravityForce), ForceMode2D.Impulse);
                    addedFall = false;
                }
            }
        }
    }

    void AddGravity()
    {
        transform.Translate(Vector3.down * gravityForce * Time.deltaTime);
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
}
