using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private UI_Script uiScript;

    public GameObject playerPrefab;
    public GameObject startPos;
    public GameObject player;

    private float timer;
    private float showTimer;

    private bool pauseGame;

    // Start is called before the first frame update
    void Start()
    {
        player = (GameObject)Instantiate(playerPrefab, startPos.transform.position, Quaternion.identity);
        uiScript = GameObject.FindWithTag("GameManager").GetComponent<UI_Script>();

        pauseGame = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
        {
            pauseGame = true;
            uiScript.PauseMenu();
        }
        if (pauseGame == true)
        {
            Time.timeScale = 0;
        }
        else if (pauseGame == false)
        {
            Time.timeScale = 1;
        }

        IncrementTimer();
    }

    public void IncrementTimer()
    {
        if (pauseGame == false)
        {
            timer += Time.deltaTime;
            showTimer = (float)Math.Round(timer, 2);
            uiScript.timerText.text = "timer: " + showTimer;
        }
    }

    public void PauseMenu()
    {
        pauseGame = true;
        uiScript.PauseMenu();
    }

    public void GameOver()
    {
        player.SetActive(false);
        pauseGame = true;
        uiScript.GameOver();
    }

    public void Victory()
    {
        pauseGame = true;
        uiScript.Victory();
    }

    public void ContinueButton()
    {
        pauseGame = false;
        uiScript.ContinueButton();
    }
    public void RestartButton()
    {
        player.SetActive(true);
        uiScript.RestartButton();
    }

    public void NextLevelButton()
    {
        uiScript.NextLevelButton();
    }

    public void QuitToMenu()
    {
        uiScript.QuitToMenu();
    }
}
