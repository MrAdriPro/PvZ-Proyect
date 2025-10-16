using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;

    private bool isOccupied = false;
    void Start()
    {
        
    }
    public void Init(bool isOffset)
    {
        _renderer.color = isOffset ? _offsetColor : _baseColor;
    }

    void OnMouseEnter()
    {
        if (GameManager.instance.gameStarted == true)
        {
            _highlight.SetActive(true);
        }
    }
    void OnMouseExit()
    {
        if (GameManager.instance.gameStarted == true)
        {
            _highlight.SetActive(false);
        }
    }

    void OnMouseDown()
    {
        if (GameManager.instance.gameStarted == true)
        {
            if (!isOccupied)
            {
                if (GameManager.instance.generatePlant(transform.position))
                {
                    isOccupied = true;
                }
            }
            else
            {
                print("Tile is occupied");
            }
        }
    }
}
