using UnityEngine;
using UnityEngine.Serialization;

public class PlantController : MonoBehaviour
{
    [SerializeField] private GameObject attackPrefab;
    [SerializeField] private Transform shootingPoint;
    public float shootInterval = 2f;
    private float shootTimer;

    public int plantCost;

    private void Update()
    {
        shootTimer += Time.deltaTime;
        if(shootTimer >= shootInterval)
        {
            Shoot();
            shootTimer = 0f;
        }
    }
    private void Shoot()
    {
        Instantiate(attackPrefab, shootingPoint.position, Quaternion.identity);
    }

}
