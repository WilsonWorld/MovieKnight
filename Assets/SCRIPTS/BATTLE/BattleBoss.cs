using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBoss : BattleCharacter
{

    public int cooldownSpecial = 3;

    // Start is called before the first frame update
    public override void Start()
    {
        isDying = false;
        isDead = false;
        health = BattleGlobals.BOSS_HEALTH * (int)BattleGlobals.BATTLE_LEVEL;
        attackMin = BattleGlobals.BOSS_MIN * (int)BattleGlobals.BATTLE_LEVEL;
        attackMax = BattleGlobals.BOSS_MAX * (int)BattleGlobals.BATTLE_LEVEL;
        anims = transform.GetComponent<Animation>();
        cooldown = 2;
        characterType = CharacterEnum.boss;
        attackTimer = 2.0f;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (!anims.isPlaying) anims.Play("Idle");
    }

    public override void Attack()
    {

        attackTimer -= Time.deltaTime;
        if (attackTimer > 0) return;

        if (isDoneAttacking) return;



        if (cooldownSpecial <= 0)
        {
            //Big special attack
            //anims.Stop();
            //anims.Play("BossAttack");
            //cooldown = 3
        }
        else if (cooldown <= 0)
        {
            //special attack
            //anims.Stop();
            //anims.Play("SpecialAttack");
            //cooldown = 2
        }
        //normal attack
        if (!anims.IsPlaying("Attack"))
        {
            //anims.Stop();
            //anims.Play("Attack");
        }

        if (anims.isPlaying) return;

        cooldown--;
        cooldownSpecial--;
        if (cooldown < 0) cooldown = 0;
        if (cooldownSpecial < 0) cooldownSpecial = 0;

        isDoneAttacking = true;
    }

    public override void Death()
    {
        if (health > 0) return;
        if (!anims.isPlaying && !isDying)
        {
            anims.Play("Death");
            isDying = true;
        }

        if (!anims.isPlaying && isDying)
        {
            anims.Stop();
            isDead = true;
        }
    }

}
