using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat_ItemInfo
{
    //这里最好用数据库读写来加载数据
    private Dictionary<int, Combat_CharacterInfo> charInfoDict = new()
    {
        {0,new Combat_CharacterInfo()
            {
                charName = "A",
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
        }
    };

    public Combat_CharacterInfo GetCharacterInfo(int id)
    {
        return charInfoDict[id];
    }
}