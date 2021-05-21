using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectScript : MonoBehaviour
{
    public void SelectLevel()
    {
        Debug.Log("This.Button.Name: " + this.gameObject.name);
        switch (this.gameObject.name)
        {
            case "Level_1_Button":
                SceneManager.LoadScene("Level_1");
                break;
            case "Level_2_Button":
                SceneManager.LoadScene("Level_2");
                break;
            case "Level_3_Button":
                SceneManager.LoadScene("Level_3");
                break;
        }
    }
}
