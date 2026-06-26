using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 战斗角色的数据信息
/// </summary>
public class Combat_CharacterInfo
{
    public string charName;
    public float maxHp,hp;
    public float attack;
    public float defense;

    public List<Combat_Skill> skills;
}

public class Combat_Skill
{
    public string skillName;
    public float attack;
}