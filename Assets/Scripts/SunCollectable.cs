using UnityEngine;

public class SunCollectable : MonoBehaviour
{
    public int sunGained = 50;
    
    void OnMouseDown()
    {
        GameManager.instance.AddEnergy(sunGained);
    }
}
