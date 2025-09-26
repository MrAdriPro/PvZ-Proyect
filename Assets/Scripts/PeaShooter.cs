using UnityEngine;

public class PeaShooter : MonoBehaviour
{
    [SerializeField] private GameObject peaPrefab;
    [SerializeField] private Transform shootingPoint;
    public float shootInterval = 2f;
    private float shootTimer;

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
        Instantiate(peaPrefab, shootingPoint.position, Quaternion.identity);
    }
}
