using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo_Combat
{

    /// <summary>
    /// 这里最好用数据库读写来加载数据
    /// </summary>
    private static Dictionary<int, CombatCharacterInfo> characterInfoDict = new Dictionary<int, CombatCharacterInfo>()
    {
        {0, new CombatCharacterInfo()
        {
            hp = 200,
                attackPower = 10,
                defensePower = 2,
                name = "Alex",
                skillList = new List<CombatSkill>()
                {
                    new CombatSkill()
                    {
                        attackPower = 20,
                        name = "Attack 1"
                    },
                    new CombatSkill()
                    {
                        attackPower = 50,
                        name = "Super Attack"
                    }
                }
            }
        },
          {1, new CombatCharacterInfo()
        {
              hp = 100,
                attackPower = 25,
                defensePower = 0,
                name = "Enemy",
                skillList = new List<CombatSkill>()
                {
                    new CombatSkill()
                    {
                        attackPower = 30,
                        name = "Attack 1"
                    },
                    new CombatSkill()
                    {
                        attackPower = 60,
                        name = "Super Attack"
                    }
                }
            }
        }
    };


    public static CombatCharacterInfo GetCombatCharacterInfo(int id)
    {
        characterInfoDict[id].maxhp = characterInfoDict[id].hp;
        return characterInfoDict[id];
    }

}
