using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SKCell;

public class CombatManager_Realtime : MonoSingleton<CombatManager_Realtime>
{
    public void ExecuteAttack(AttackPackage package)
    {
        ///在这里进行所有数值上的计算
        Debug.Log($"Attack: {package.from.name} to {package.to.name}");
        package.atkPower -= package.to.info.defensePower;

        package.to.OnAttacked(package);
    }
}
