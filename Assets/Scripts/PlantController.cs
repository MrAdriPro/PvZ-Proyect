using UnityEngine;
using UnityEngine.Serialization;

public class PlantController : MonoBehaviour
{
    public PlantData data;
    private float shootTimer;
    public Transform projectileTransform;


    private void Update()
    {
        shootTimer += Time.deltaTime;
        if(shootTimer >= data.attackCooldown)
        {
            Shoot();
            shootTimer = 0f;
        }
    }
    private void Shoot()
    {
        Instantiate(data.projectilePrefab, projectileTransform.position, Quaternion.identity);
    }

}
