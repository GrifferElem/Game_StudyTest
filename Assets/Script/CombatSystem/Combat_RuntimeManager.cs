using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat_RuntimeManager : MonoBehaviour
{
    public static Combat_RuntimeManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void EnterAttack(AttackPack attacker)
    {
        Debug.Log($"¹¥»÷£º{attacker.from.name} ½ÓÊÕ{attacker.to.name}");
        attacker.attack -= attacker.to.charInfo.defense;

        attacker.to.Defense(attacker);
    }
}
