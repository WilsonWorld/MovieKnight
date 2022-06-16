using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleCharacter : MonoBehaviour
{

    public CharacterEnum characterType;
    public DungeonEnum dungeonType;

    public bool isDead;
    public bool isDying;
    public bool isAttacking;
    public bool isDoneAttacking;
    public int health;
    public int attackMin;
    public int attackMax;
    public Animation anims;

    public int cooldown;
    public float attackTimer;

    // Start is called before the first frame update
    public abstract void Start();

    // Update is called once per frame
    public abstract void Update();

    public abstract void Death();

    public abstract void Attack();

}
