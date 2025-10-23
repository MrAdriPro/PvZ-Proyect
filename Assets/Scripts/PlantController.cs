using UnityEngine;
using UnityEngine.Serialization;

public class PlantController : MonoBehaviour
{
    public PlantData data;
    private float shootTimer;
    public Transform projectileTransform;


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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * data.attackRange);
    }

}
