using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdPlayer : CombatCharacter
{

    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public override void Attack()
    {
        anim.Play("PlayerAttack", 0, 0);

        base.Attack();
    }
}
