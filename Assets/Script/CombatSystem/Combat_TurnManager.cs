using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Combat_TurnManager : MonoBehaviour
{
    public static Combat_TurnManager instance;

    [HideInInspector] AttackDir currentAttackTurn;
    public GameObject skillFramePrefab, skillFrameContainer;
    public Slider playerHp, enemyHp;
    public Combat_CharacterInfo playerInfo, enemyInfo;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        EnterCombat(0, 1);
    }
    public void EnterCombat(int playerID,int enemyID)
    {
        currentAttackTurn = AttackDir.PlayerToEnemy;
        playerInfo = Combat_ItemInfo.GetCharacterInfo(playerID);
        enemyInfo = Combat_ItemInfo.GetCharacterInfo(enemyID);

        InitaialCombat();
    }
    public void InitaialCombat()
    {
        playerHp.SetValueWithoutNotify(1);
        enemyHp.SetValueWithoutNotify(1);
        foreach(var s in playerInfo.skills)
        {
            Combat_SkillFrame skillframe = Instantiate(skillFramePrefab,skillFrameContainer.transform).GetComponent<Combat_SkillFrame>();
            skillframe.skill = s;
            skillframe.UpdateInfo();
        }
    }
    public void EnterAttack(AttackPack attacker)
    {
        float damage = attacker.attack+(currentAttackTurn == AttackDir.PlayerToEnemy?playerInfo.attack:enemyInfo.attack);
        float finalDamage = damage-(currentAttackTurn == AttackDir.PlayerToEnemy?enemyInfo.defense:playerInfo.defense);
        Debug.Log($"伤害：{finalDamage}，{currentAttackTurn}");

        DealDamage(currentAttackTurn, finalDamage);

        AttackDir nextAttackTurn = currentAttackTurn == AttackDir.PlayerToEnemy ? AttackDir.EnemyToPlayer : AttackDir.PlayerToEnemy;
        SetTurn(nextAttackTurn);
        if(nextAttackTurn == AttackDir.EnemyToPlayer)
        {
            EnemyAttack();
        }
    }
    private void EnemyAttack()
    {
        AttackPack attacker = new AttackPack();
        attacker.attackDir = AttackDir.EnemyToPlayer;
        attacker.attack = enemyInfo.skills[Random.Range(0, enemyInfo.skills.Count)].attack;

        EnterAttack(attacker);
    }
    private void SetTurn(AttackDir attackDir)
    {
        currentAttackTurn = attackDir;
    }
    private void DealDamage(AttackDir attackDir,float damage)
    {
        Combat_CharacterInfo charInfo = attackDir == AttackDir.PlayerToEnemy ? enemyInfo : playerInfo;
        //注意，直接在这里-hp不是很好
        charInfo.hp -= damage;
        if (charInfo.hp <= 0)
        {
            OnCharacterDefeat(attackDir);
            charInfo.hp = 0;
        }
        UpdateHpBar();
    }
    private void UpdateHpBar()
    {
        playerHp.value = (float)playerInfo.hp / playerInfo.maxHp;
        enemyHp.value = (float)enemyInfo.hp / enemyInfo.maxHp;
    }
    private void OnCharacterDefeat(AttackDir attackDir)
    {
        string defenser = attackDir == AttackDir.PlayerToEnemy ? "enemy" : "player";
        Debug.Log($"{defenser} Die!");
    }
}

public class AttackPack
{
    public AttackDir attackDir;
    public float attack;

    public Combat_Character from, to;
}
public enum AttackDir
{
    PlayerToEnemy,
    EnemyToPlayer
}
