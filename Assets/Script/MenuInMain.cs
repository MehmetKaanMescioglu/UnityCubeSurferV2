using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInMain : MonoBehaviour
{
    public GameObject firstPlay, gameMenu, PauseMenu, FailMenu, WinMenu;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void Awake()
    {
        Singleton();

    }

    #region Singleton

    public static MenuInMain Instance;

    public void Singleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }
    #endregion
    public void FirstPlayButton()
    {
        firstPlay.SetActive(false);
        SceneManager.LoadScene(1);
        //LevelManager.Instance.Setlevel(1);
    }

    public void PauseButton()
    {
        Time.timeScale = 0;
        gameMenu.SetActive(false);
        PauseMenu.SetActive(true);
        FailMenu.SetActive(false);
    }

    public void PlayButton()
    {
        Time.timeScale = 1;
        gameMenu.SetActive(true);
        PauseMenu.SetActive(false);
        FailMenu.SetActive(false);
    }


    public void RestartButton()
    {
        Time.timeScale = 1;
        //SceneManager.LoadScene(1);

        float chapter = LevelManager.Instance.Getlevel();
        LevelManager.Instance.RePoint();
        Debug.Log("$chapter");
        if (chapter % 3 == 0)
            SceneManager.LoadScene(3);
        if (chapter % 3 == 2)
            SceneManager.LoadScene(2);
        if (chapter % 3 == 1)
            SceneManager.LoadScene(1);

    }

    public void MenuButton()
    {
        Time.timeScale = 1;
        Application.Quit();
    }

    public void ActiveFail()
    {
        FailMenu.SetActive(true);
        gameMenu.SetActive(true);
        
    }

    public void FailButton()
    {
        Time.timeScale = 1;
        gameMenu.SetActive(true);
        LevelManager.Instance.RePoint();

        float chapter = LevelManager.Instance.Getlevel();

        Debug.Log("$chapter");
        if (chapter %3 == 0)
            SceneManager.LoadScene(3);
        if (chapter %3 == 2)
            SceneManager.LoadScene(2);
        if (chapter %3 == 1)
            SceneManager.LoadScene(1);
    }

    public void ActiveWin()
    {
        WinMenu.SetActive(true);
        gameMenu.SetActive(true);
    }

    public void WinButton()
    {
        Time.timeScale = 1;
        gameMenu.SetActive(true);
        WinMenu.SetActive(false);

        float chapter = LevelManager.Instance.Getlevel();
        LevelManager.Instance.Setlevel(1);
        Debug.Log("$chapter");
        if (chapter %3 == 1 )
            SceneManager.LoadScene(2);
        if (chapter %3  == 2)
            SceneManager.LoadScene(3);
        if (chapter %3 == 0)
            SceneManager.LoadScene(1);
    }
}
