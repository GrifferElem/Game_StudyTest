using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SKCell;
public class SkillFrame : MonoBehaviour
{
    public CombatSkill skill;
    [SerializeField] SKText nameText, atkText;
    
    public void UpdateInfo()
    {
        nameText.UpdateTextDirectly(skill.name);
        atkText.UpdateTextDirectly(skill.attackPower.ToString("f0"));
    }

    public void OnClick()
    {
        AttackPackage package = new AttackPackage();
        package.atkPower = skill.attackPower;
        package.dir = AttackDirection.PlayerToEnemy;

        CombatManager_TurnedBased.instance.ExecuteAttack(package);
    }
}
