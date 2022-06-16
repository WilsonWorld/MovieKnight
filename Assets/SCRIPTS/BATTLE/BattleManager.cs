using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        if (SceneManager.GetActiveScene().name == "SamTest") SceneManager.LoadScene("SamTestSwitchTo");
        else SceneManager.LoadScene("SamTest");
    }

}
