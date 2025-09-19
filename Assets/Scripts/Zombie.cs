using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private float speed = 1f;
    private void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
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
