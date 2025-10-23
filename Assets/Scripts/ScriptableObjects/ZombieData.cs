using UnityEngine;

[CreateAssetMenu(fileName = "NewZombie", menuName = "PVZ/Unit/Zombie")]
public class ZombieData : UnitData
{

    public float attackDamage;
    public float attackRate;
    public float attackRange;
    public bool isArmored;
    public override void Attack(GameObject target)
    {

    }
}
