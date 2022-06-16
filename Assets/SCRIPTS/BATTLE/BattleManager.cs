using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    public GameObject managerUI;

    GameObject player;
    GameObject enemy;
    public GameObject playerSlot;
    public GameObject enemySlot;
    bool isPlayerTurn;

    public GameObject background;

    public GameObject testPrefab;

    public DungeonEnum dungeonType;

    float enemyStartTimer;


    // Start is called before the first frame update
    void Start()
    {

        BattleGlobals.BATTLE_LEVEL += 0.5f;

        isPlayerTurn = true;
        enemyStartTimer = 2.0f;

        //player = Instantiate(testPrefab, playerSlot.transform);
        //enemy = Instantiate(testPrefab, enemySlot.transform); 

        //Sprite[] backgrounds = Resources.LoadAll<Sprite>("BattleBackgrounds");
        //
        //if (dungeonType == DungeonEnum.Plushie) background.GetComponent<SpriteRenderer>().sprite = backgrounds[0];
        //if (dungeonType == DungeonEnum.Pumpkin) background.GetComponent<SpriteRenderer>().sprite = backgrounds[1];
        //if (dungeonType == DungeonEnum.Slime) background.GetComponent<SpriteRenderer>().sprite = backgrounds[2];
        //if (dungeonType == DungeonEnum.Underwater) background.GetComponent<SpriteRenderer>().sprite = backgrounds[3];

    }

    // Update is called once per frame
    void Update()
    {
        if(!isPlayerTurn)
        {
            enemyStartTimer -= Time.deltaTime;
            if (enemyStartTimer > 0) return;

            //player.health -= EnemyAttack(enemy.attack);
        }

        if (Input.GetKey(KeyCode.Space) && SceneManager.GetActiveScene().name == "SamTest") SceneManager.LoadScene("SamTestSwitchTo");
        else if (Input.GetKey(KeyCode.Space) && SceneManager.GetActiveScene().name != "SamTest")SceneManager.LoadScene("SamTest");


    }

    void EnemyDeath()
    {
        if (TryGetComponent(out BattleEnemy normalEnemy)) normalEnemy.Death();
        else enemy.transform.GetComponent<BattleBoss>().Death();
        if (enemy != null) return;
        SceneManager.LoadScene("Overworld");
    }

    int EnemyAttack()
    {
        return 0;
    }

    void BossSpecial()
    {

    }

    void PlayerDeath()
    {
        BattleGlobals.PLAYERATTACK_MIN = BattleGlobals.PLAYERATTACK_MINSTART;
        BattleGlobals.PLAYERATTACK_MAX = BattleGlobals.PLAYERATTACK_MAXSTART;
        BattleGlobals.ENEMYATTACK_MIN = BattleGlobals.ENEMYATTACK_MINSTART;
        BattleGlobals.ENEMYATTACK_MAX = BattleGlobals.ENEMYATTACK_MAXSTART;
        BattleGlobals.BATTLE_LEVEL = 0;
        SceneManager.LoadScene("Bedroom");
    }

    int PlayerAttack()
    {
        return 0;
    }

    int PlayerHeal()
    {
        return 0;
    }

}
