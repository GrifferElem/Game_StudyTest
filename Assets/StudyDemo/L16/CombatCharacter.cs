using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 战斗人物。（玩家和敌人都属于这个类别）
/// </summary>
public class CombatCharacter : MonoBehaviour
{
    public int id = 0;

    public bool canAttackActively;

    [HideInInspector]
    public CombatCharacterInfo info;

    /// <summary>
    /// 当前接触到的（可被攻击的）对象
    /// </summary>
    private List<CombatCharacter> contactedCharacter = new List<CombatCharacter>();

    private void Start()
    {
        info = ItemInfo_Combat.GetCombatCharacterInfo(id);
    }

    private void Update()
    {
        if (canAttackActively && Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    public virtual void Attack()
    {
        foreach (var item in contactedCharacter)
        {
            AttackPackage package = new AttackPackage();
            package.atkPower = info.attackPower;
            package.from = this;
            package.to = item;
            CombatManager_Realtime.instance.ExecuteAttack(package);
        }
    }

    public virtual void OnAttacked(AttackPackage package)
    {
        info.hp -= package.atkPower;
        if (info.hp < 0)
        {
            info.hp = 0;
            OnDeath();
        }
    }

    public virtual void OnDeath()
    {
        print($"Death: {id}");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CombatCharacter character = collision.GetComponent<CombatCharacter>();
        contactedCharacter.Add(character);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CombatCharacter character = collision.GetComponent<CombatCharacter>();
        contactedCharacter.Remove(character);
    }

}



