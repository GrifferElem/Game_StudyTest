using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat_ItemInfo
{
    //这里最好用数据库读写来加载数据
    private static Dictionary<int, Combat_CharacterInfo> charInfoDict = new()
    {
        {0,new Combat_CharacterInfo()
            {
                charName = "A",
                hp = 100,
                attack = 10,
                defense = 5,
                skills = new()
                {
                    new Combat_Skill()
                    {
                        skillName = "Fire",
                        attack = 15,
                    },
                    new Combat_Skill()
                    {
                        skillName = "Ice",
                        attack = 12,
                    }
                }
            }
        },
        {1,new Combat_CharacterInfo()
            {
                charName = "A",
                hp = 200,
                attack = 10,
                defense = 5,
                skills = new()
                {
                    new Combat_Skill()
                    {
                        skillName = "Boom1",
                        attack = 25,
                    },
                    new Combat_Skill()
                    {
                        skillName = "Boom2",
                        attack = 22,
                    }
                }
            }
        }
    };

    public static Combat_CharacterInfo GetCharacterInfo(int id)
    {
        charInfoDict[id].maxHp = charInfoDict[id].hp;
        return charInfoDict[id];
    }
}