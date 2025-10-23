using UnityEngine;

public class Zombie : MonoBehaviour
{
    public ZombieData data;
    private float health;
    private float attackCooldownTimer = 0f;
    private bool isAttacking = false;

    private void Start()
    {
            health = data.maxHealth;
    }

    private void Update()
    {
        if (data != null)
            transform.Translate(Vector3.left * data.speed * Time.deltaTime);

        if (attackCooldownTimer > 0f)
            attackCooldownTimer -= Time.deltaTime;

        if (data != null && data.attackRate > 0f)
            AttackInRange();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void Attack(GameObject target)
    {
        if (target == null || data == null)
            return;

        var plantController = target.GetComponent<PlantController>();
        if (plantController != null)
        {
            plantController.TakeDamage(data.attackDamage);
        }
    }

    private void AttackInRange()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, data.attackRange, LayerMask.GetMask("Plant"));
        if (hit.collider != null)
        {
            
                isAttacking = true;
                if(isAttacking)
                Attack(hit.collider.gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * data.attackRange);
    }
}
