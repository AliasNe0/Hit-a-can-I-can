using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject levelMenu;
    [SerializeField] private GameObject levelUI;
    [SerializeField] private TMP_Text levelNameTxt;
    [SerializeField] private TMP_Text messageTxt;
    [SerializeField] private TMP_Text ballCountTxt;
    [SerializeField] private TMP_Text canCountTxt;
    [Header("Text effects")]
    [SerializeField] private float punchScale = 0.1f;
    [SerializeField] private float punchRotation = 15f;
    [SerializeField] private float punchLength = 0.5f;

    public bool levelIsCompleted = false;
    private bool levelMenuIsActive = false;

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
        Time.timeScale = 1f;
        levelIsCompleted = false;
        if (scene.buildIndex == 0)
        {
            levelUI.SetActive(false);
            levelMenu.SetActive(false);
            levelMenuIsActive = false;
            mainMenu.SetActive(true);
        }
        if (scene.buildIndex > 0)
        {
            levelNameTxt.text = SceneManager.GetActiveScene().name;
            levelUI.SetActive(true);
            levelMenu.SetActive(false);
            levelMenuIsActive = false;
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
        levelMenuIsActive = !levelMenuIsActive;
        if (levelMenuIsActive)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1f;
        }
        levelMenu.SetActive(levelMenuIsActive);
    }

    public void LoadNextLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void UpdateBallCount(int ballCount, int ballLimit, bool testMode)
    {
        PunchText(ballCountTxt.gameObject.transform);
        ballCountTxt.text = $"{ballCount}/{ballLimit}";
        if (ballCount == 0 && !levelMenuIsActive && !testMode)
        {
            // the delay is needed if all balls are spent but there are still cans moving or falling
            StartCoroutine(DelayGameEnd());
        }
    }

    public void UpdateCanCount(int canCount, int canLimit, bool testMode)
    {
        PunchText(canCountTxt.gameObject.transform);
        canCountTxt.text = $"{canCount}/{canLimit}";
        if (canCount == 0 && !levelMenuIsActive && !testMode)
        {
            levelIsCompleted = true;
            UpdateGameEndMessage();
            ShowLevelMenu();
        }
    }

    IEnumerator DelayGameEnd()
    {
        yield return new WaitForSeconds(2f);
        if (!levelMenuIsActive)
        {
            UpdateGameEndMessage();
            ShowLevelMenu();
        }
    }

    private void PunchText(Transform countTransform)
    {
        countTransform.DOPunchScale(punchScale * (new Vector3(1f, 1f, 0f)), punchLength, 1, 1);
        countTransform.DOPunchRotation(punchRotation * (new Vector3(0f, 0f, -1f)), punchLength, 1, 1).OnComplete(
            () => countTransform.DOPunchRotation(punchRotation * (new Vector3(0f, 0f, 1f)), punchLength, 1, 1));
    }

    private void UpdateGameEndMessage()
    {
        if (levelIsCompleted)
        {
            levelNameTxt.text = "Level is cleared";
            messageTxt.text = "Good job, mate!";
            UnlockAchievement();
        }
        else
        {
            levelNameTxt.text = "Game over";
            messageTxt.text = "Try again? =)";
        }
    }

    private void UnlockAchievement()
    {
        NotificationManager notificationManager = NotificationManager.Instance;
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            notificationManager.SendAchievementNotification("Can Slayer");
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            notificationManager.SendAchievementNotification("Demonic Ball");
        }
    }
}