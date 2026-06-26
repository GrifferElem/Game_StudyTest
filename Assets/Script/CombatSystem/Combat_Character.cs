using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 战斗角色
/// </summary>
public class Combat_Character : MonoBehaviour
{
    private List<Combat_Character> connectChars = new();
    [HideInInspector]public Combat_CharacterInfo charInfo;

    public int id = 0;

    private void Start()
    {
        charInfo = Combat_ItemInfo.GetCharacterInfo(id);
    }
    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Attack();
        }
    }
    #region 放武器脚本
    private void OnTriggerEnter(Collider other)
    {
        Combat_Character character = other.GetComponent<Combat_Character>();
        connectChars.Add(character);
    }
    private void OnTriggerExit(Collider other)
    {
        Combat_Character character = other.GetComponent<Combat_Character>();
        connectChars.Remove(character);
    }
    #endregion

    public virtual void Attack()
    {
        foreach(var c in connectChars)
        {
            AttackPack attacker = new();
            attacker.attack = charInfo.attack;
            attacker.from = this;
            attacker.to = c;

            Combat_RuntimeManager.instance.EnterAttack(attacker);
        }
    }
    public virtual void Defense(AttackPack attacker)
    {
        //注意，直接在这里-hp不是很好
        charInfo.hp -= attacker.attack;
        if (charInfo.hp <= 0)
        {
            charInfo.hp = 0;
            OnDie(charInfo);
        }
    }
    public virtual void OnDie(Combat_CharacterInfo character)
    {
        Debug.Log($"{character.charName} Die!");
    }
}
