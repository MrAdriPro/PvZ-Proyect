using UnityEngine;
using UnityEngine.Serialization;

public class PlantController : MonoBehaviour
{
    public PlantData data;
    private float shootTimer;
    public Transform projectileTransform;
    private float health = 0;

    private void Start()
    {
        health = data.maxHealth;
    }

    private void Update()
    {
        shootTimer += Time.deltaTime;
        if(shootTimer >= data.attackCooldown)
        {
            InRange();
            shootTimer = 0f;
        }
    }
    private void Shoot()
    {
        Instantiate(data.projectilePrefab, projectileTransform.position, Quaternion.identity);
    }
    private void InRange()
    {
        if(Physics2D.Raycast(transform.position, Vector2.right, data.attackRange, LayerMask.GetMask("Enemy")))
        {
            Shoot();
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if(data.maxHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * data.attackRange);
    }

}
