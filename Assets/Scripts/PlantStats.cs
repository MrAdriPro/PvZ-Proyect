using UnityEngine;

[CreateAssetMenu(fileName = "Plant", menuName = "New Plant/Plant")]
public class PlantStats : ScriptableObject
{
    public int health = 100;
    public int damage = 20;
    public float attackRate = 1f;
    public int cost = 50;
    public float range = 5f;

    public GameObject plantPrefab;
    public AudioClip plantSound;
    public AudioClip attackSound;
    public PlantType plantType;
}
public enum PlantType : byte
{
    Peashooter,
    Sunflower,
    Wallnut,
    CherryBomb
}
