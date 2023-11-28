using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMagnament : MonoBehaviour
{
    public int SceneToLoad;

    public void LoadNewScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void LoadNewSceneFadeOut()
    {
        SceneManager.LoadScene(SceneToLoad);
    }

}
