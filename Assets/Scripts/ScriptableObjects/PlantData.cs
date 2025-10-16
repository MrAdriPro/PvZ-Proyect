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
    


    public override void Attack(GameObject target)
    {
        //if(projectilePrefab != null && target != null)
        //{
        //    GameObject projectile = Instantiate(projectilePrefab);
        //    Bullet projController = projectile.GetComponent<Bullet>();
            
        //}
    }
}
