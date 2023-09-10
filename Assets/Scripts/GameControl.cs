using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public static GameControl instance;
    public bool gameOver = false;
    public bool Win = false;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            SceneManager.LoadScene(0);
        }
        if (gameOver)
            StartCoroutine(KillnReset());
        if (Win)
            StartCoroutine(WinLevel());
    }
    IEnumerator KillnReset()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator WinLevel()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }
}
