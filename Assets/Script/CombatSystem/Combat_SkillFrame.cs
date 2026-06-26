using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Combat_SkillFrame : MonoBehaviour
{
    public Combat_Skill skill;
    public TMP_Text nameText, atkText;

    public void UpdateInfo()
    {
        nameText.text = skill.skillName;
        atkText.text = skill.attack.ToString("f0");
    }
    public void OnClick()
    {
        AttackPack attacker = new AttackPack();
        attacker.attackDir = AttackDir.PlayerToEnemy;
        attacker.attack = 10f;

        Combat_TurnManager.instance.EnterAttack(attacker);
    }
}
