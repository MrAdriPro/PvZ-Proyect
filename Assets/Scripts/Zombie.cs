using UnityEngine;

public class Zombie : MonoBehaviour
{
    public ZombieData data;
    private void Update()
    {
        transform.Translate(Vector3.left * data.speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        data.maxHealth -= damage;
        if (data.maxHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}
