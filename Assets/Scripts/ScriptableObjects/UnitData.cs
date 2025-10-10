using UnityEngine;

public abstract class UnitData : ScriptableObject
{
    //Comun properties
    public float maxHealth;
    public float speed;
    public GameObject modelPrefab;

    public abstract void Attack(GameObject target);
}
