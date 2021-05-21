using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject levelMenu;


    public void SetMainMenu()
    {
        mainMenu.SetActive(true);
        levelMenu.SetActive(false);
    }

    public void SetLevelMenu()
    {
        mainMenu.SetActive(false);
        levelMenu.SetActive(true);
    }
}
