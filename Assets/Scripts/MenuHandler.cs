using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuHandler : MonoBehaviour
{
    public void GotoLevel(int lvl)
    {
        SceneManager.LoadScene(lvl % SceneManager.sceneCountInBuildSettings);
    }
}
