using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public PlantData data;
    public float bulletLifetime = 8f;

    private void Update()
    {
        transform.Translate(Vector3.right * data.bulletSpeed * Time.deltaTime);
        bulletLifetime -= Time.deltaTime;
        if (bulletLifetime <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Zombie"))
        {
            Zombie zombie = collision.GetComponent<Zombie>();
            if (zombie != null)
            {
                zombie.TakeDamage(data.damage);
            }
            Destroy(gameObject);
        }
    }
}
