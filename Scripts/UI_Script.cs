using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Script : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI pauseText;
    public Button continueButton;
    public TextMeshProUGUI deathText;
    public Button restartButton;
    public Button quitButton;
    public TextMeshProUGUI victoryText;
    public Button nextLevelButton;



    public void PauseMenu()
    {
        pauseText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        continueButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        deathText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void Victory()
    {
        victoryText.gameObject.SetActive(true);
        nextLevelButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }
    public void ContinueButton()
    {
        pauseText.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevelButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

}
