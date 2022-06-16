using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEnemy : BattleCharacter
{

    // Start is called before the first frame update
    public override void Start()
    {
        isDying = false;
        isDead = false;
        health = BattleGlobals.ENEMY_HEALTH * (int)BattleGlobals.BATTLE_LEVEL;
        attackMin = BattleGlobals.ENEMYATTACK_MIN * (int)BattleGlobals.BATTLE_LEVEL;
        attackMax = BattleGlobals.ENEMYATTACK_MAX * (int)BattleGlobals.BATTLE_LEVEL;
        anims = transform.GetComponent<Animation>();
        cooldown = 2;
        characterType = CharacterEnum.enemy;
        
    }

    // Update is called once per frame
    public override void Update()
    {
        if (isDead) Destroy(transform.gameObject);
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

    public override void Attack()
    {
        if(cooldown == 0)
        {
            //special attack
            //cooldown = 2
        }
        //normal attack
        cooldown--;
    }

}
