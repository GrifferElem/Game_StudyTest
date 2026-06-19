using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SKCell;
public class CombatManager_TurnedBased : MonoSingleton<CombatManager_TurnedBased>
{
    [HideInInspector]
    public AttackDirection currentTurn;

    [SerializeField] GameObject skillFramePF, skillFrameContainer;
    [SerializeField] SKSlider playerHPBar, enemyHPBar;

    public CombatCharacterInfo playerInfo, enemyInfo;

    void Start()
    {
        EnterBattle(0, 1);
    }

    public void EnterBattle(int playerID, int enemyID)
    {
        currentTurn = AttackDirection.PlayerToEnemy;
        playerInfo = ItemInfo_Combat.GetCombatCharacterInfo(playerID);
        enemyInfo = ItemInfo_Combat.GetCombatCharacterInfo(enemyID);

        InitializeBattle();
    }

    public void InitializeBattle()
    {

        playerHPBar.SetValue(1);
        enemyHPBar.SetValue(1);

        playerHPBar.totalValue = (int)playerInfo.hp;
        enemyHPBar.totalValue = (int)enemyInfo.hp;

        //初始化技能按钮
        foreach (var skill in playerInfo.skillList)
        {
            SkillFrame frame = Instantiate(skillFramePF, skillFrameContainer.transform).GetComponent<SkillFrame>();
            frame.skill = skill;
            frame.UpdateInfo();
        }
    }
    public void SetTurn(AttackDirection dir)
    {
        currentTurn = dir;
    }
    public void ExecuteAttack(AttackPackage atk)
    {
        print(atk.dir);
        float damage = atk.atkPower + (atk.dir == AttackDirection.PlayerToEnemy ? playerInfo.attackPower : enemyInfo.attackPower);
        float realDamage = damage - (atk.dir == AttackDirection.PlayerToEnemy ? enemyInfo.defensePower : playerInfo.defensePower);
        DealDamage(atk.dir, realDamage);

        SetTurn(atk.dir == AttackDirection.PlayerToEnemy ? AttackDirection.EnemyToPlayer : AttackDirection.PlayerToEnemy);

        if (atk.dir == AttackDirection.PlayerToEnemy)
            StartCoroutine(EnemyAttack());

    }

    IEnumerator EnemyAttack()
    {
        yield return new WaitForSeconds(3);
        AttackPackage atk = new AttackPackage();
        atk.dir = AttackDirection.EnemyToPlayer;
        atk.atkPower = enemyInfo.skillList[Random.Range(0, enemyInfo.skillList.Count)].attackPower;
        ExecuteAttack(atk);
    }

    public void DealDamage(AttackDirection dir, float damage)
    {
        CombatCharacterInfo info = dir == AttackDirection.PlayerToEnemy ? enemyInfo : playerInfo;
        //这里直接修改了info的hp，但这么做并不是很好。
        info.hp -= damage;
        if(info.hp <=0)
        {
            OnCharacterDefeat();
            info.hp = 0;
        }
        UpdateHPBars();
    }

    public void UpdateHPBars()
    {
        playerHPBar.SetValue(playerInfo.hp / playerInfo.maxhp);
        enemyHPBar.SetValue(enemyInfo.hp / enemyInfo.maxhp);
    }


    public void OnCharacterDefeat()
    {
        //
    }
}

public class AttackPackage
{
    public AttackDirection dir;
    public float atkPower;

    public CombatCharacter from, to;
}
public enum AttackDirection
{
    PlayerToEnemy,
    EnemyToPlayer
}

