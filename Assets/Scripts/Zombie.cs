using UnityEngine;

public class Zombie : MonoBehaviour
{
    public ZombieData data;
    private float health;
    private float attackCooldownTimer = 0f;
    private bool isAttacking = false;
    [SerializeField] private Animator animatorZombie;
    private EnemySpawner spawner;
    private int lineIndex;

    public void SetSpawnerReference(EnemySpawner s, int index, int enemyCost)
    {
        spawner = s;
        lineIndex = index;
    }

    private void Start()
    {
        // Importante: Asume que ZombieData hereda de UnitData que contiene 'maxHealth'
        health = data != null ? data.maxHealth : 0f;
        animatorZombie = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        // Movimiento: Solo si no está atacando
        if (data != null && !isAttacking)
        {
            transform.Translate(Vector3.left * data.speed * Time.deltaTime);
            animatorZombie.SetBool("isWalking", true);
        }

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
        if (spawner != null)
        {
            // Notifica al spawner para liberar 1 slot en la línea
            spawner.EnemyDied(lineIndex);
        }
        Destroy(gameObject);
    }

    private void Attack(GameObject target)
    {
        if (target == null || data == null)
            return;

        // Asume que el objetivo tiene un script PlantController con TakeDamage
        var plantController = target.GetComponent<PlantController>();
        if (plantController != null)
        {
            plantController.TakeDamage(data.attackDamage);
        }
    }

    private void AttackInRange()
    {
        if (data == null)
            return;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, data.attackRange, LayerMask.GetMask("Plant"));
        if (hit.collider != null)
        {
            isAttacking = true;
            animatorZombie.SetBool("isAttacking", true);
            animatorZombie.SetBool("isWalking", false);

            if (attackCooldownTimer <= 0f)
            {
                Attack(hit.collider.gameObject);

                if (data.attackRate > 0f)
                    attackCooldownTimer = 1f / data.attackRate;
            }
        }
        else
        {
            // Moviéndose
            isAttacking = false;
            animatorZombie.SetBool("isAttacking", false);
        }
    }

    private void OnDrawGizmos()
    {
        if (data == null)
            return;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * data.attackRange);
    }
}