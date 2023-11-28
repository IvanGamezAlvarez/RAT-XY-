using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class UIMannager : MonoBehaviour
{
    public TextMeshProUGUI SwitchingText;
    void Start()
    {
      
    }

    void Update()
    {
        RestartLevel();
    }
    void BlinkUi(Color objColor)
    {
       // SwitchingText.color 



    }
    void RestartLevel()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {

            Scene ActiveSceen = SceneManager.GetActiveScene();
            SceneManager.LoadScene(ActiveSceen.name);

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            
            SceneManager.LoadScene(0);

        }
    }
}
