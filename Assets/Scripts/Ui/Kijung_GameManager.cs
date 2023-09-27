using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Kijung_GameManager : MonoBehaviour
{
    public string Scene_Name;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Game_Start()
    {
        SceneManager.LoadScene(Scene_Name);
    }
}
