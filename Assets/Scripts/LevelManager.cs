using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject levelMenu;
    [SerializeField] private GameObject levelUI;
    [SerializeField] private GameObject levelCompleted;
    [SerializeField] private TMP_Text ballCountTxt;
    [SerializeField] private TMP_Text canCountTxt;

    public bool levelIsCompleted = false;
    private bool levelMenuActive = false;

    public static LevelManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        levelIsCompleted = false;
        if (scene.buildIndex == 0)
        {
            levelUI.SetActive(false);
            levelCompleted.SetActive(false);
            levelMenu.SetActive(false);
            levelMenuActive = false;
            mainMenu.SetActive(true);
        }
        if (scene.buildIndex > 0)
        {
            levelUI.SetActive(true);
            levelCompleted.SetActive(false);
            levelMenu.SetActive(false);
            levelMenuActive = false;
            mainMenu.SetActive(false);
        }
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene(2);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowLevelMenu()
    {
        levelMenuActive = !levelMenuActive;
        if (levelIsCompleted) levelCompleted.SetActive(levelMenuActive);
        levelMenu.SetActive(levelMenuActive);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void UpdateBallCountTxt(int ballCount, int ballLimit, bool testMode)
    {
        ballCountTxt.text = $"{ballCount}/{ballLimit}";
        if (!testMode && ballCount == 0) ShowLevelMenu();
    }

    public void UpdateCanCountTxt(int canCount, int canLimit, bool testMode)
    {
        canCountTxt.text = $"{canCount}/{canLimit}";
        if (!testMode && canCount == 0)
        {
            // enable "Level is cleared" text
            levelIsCompleted = true;
            ShowLevelMenu();
        }
    }
}