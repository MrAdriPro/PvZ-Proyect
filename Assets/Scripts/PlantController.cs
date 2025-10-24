using UnityEngine;
using UnityEngine.Serialization;

public class PlantController : MonoBehaviour
{
    public PlantData data;
    private float shootTimer;
    public Transform projectileTransform;
    [SerializeField] private float currentHealth;

    private void Start()
    {
        currentHealth = data.maxHealth;
    }

    private void Update()
    {
        shootTimer += Time.deltaTime;
        if(shootTimer >= data.attackCooldown)
        {
            PlantsBehaviour();
            shootTimer = 0f;
        }
    }
    private void Shoot()
    {
        Instantiate(data.projectilePrefab, projectileTransform.position, Quaternion.identity);
    }
    private void PlantsBehaviour()
    {
        if(data.plantType == PlantType.Peashooter)
        {
            if (Physics2D.Raycast(transform.position, Vector2.right, data.attackRange, LayerMask.GetMask("Enemy")))
            {
                Shoot();
            }
        }
        if(data.plantType == PlantType.Sunflower)
        {
            Shoot();
        }

    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(this.gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * data.attackRange);
    }

}
