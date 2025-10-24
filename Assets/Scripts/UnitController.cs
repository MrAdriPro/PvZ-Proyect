using UnityEngine;

public class UnitController : MonoBehaviour
{
    public UnitData unitData;

    private float currentHealth;

    private void Start()
    {
        if (unitData == null)
        {
            Debug.LogError("UnitData no está asignado en " + gameObject.name);
            return;
        }

        currentHealth = unitData.maxHealth;


    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    

    private void Die()
    {
        Destroy(gameObject);
    }
}

