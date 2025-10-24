using UnityEngine;

[CreateAssetMenu(fileName = "NewPlant", menuName = "PVZ/Unit/Plant")]
public class PlantData : UnitData
{
    public int sunCost;
    public float attackCooldown;
    public GameObject projectilePrefab;
    public float attackRange;
    public float bulletSpeed = 5f;
    public int damage = 1;
    public PlantType plantType;
    
    [SerializeField] 
    public string description;
}
public enum PlantType : byte
{
    Peashooter,
    Sunflower,
    WallNut,
    CherryBomb

}
