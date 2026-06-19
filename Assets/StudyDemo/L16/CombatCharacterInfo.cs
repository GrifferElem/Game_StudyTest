using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 战斗角色的数据信息
/// </summary>
public class CombatCharacterInfo
{
    public string name;

    public float maxhp, hp;
    public float attackPower;
    public float defensePower;

    public List<CombatSkill> skillList; 
}

public class CombatSkill
{
    public string name;
    public float attackPower;
}
