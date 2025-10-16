using UnityEngine;

public class Zombie : MonoBehaviour
{
    public ZombieData data;
    private float health;
    private void Start()
    {
        health = data.maxHealth;
    }
    private void Update()
    {
        transform.Translate(Vector3.left * data.speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
       health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}
