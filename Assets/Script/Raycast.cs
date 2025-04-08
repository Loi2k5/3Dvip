using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Raycast : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] public TextMeshProUGUI destroyedCountText;
    [SerializeField] public TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI notificationText;
    [SerializeField] GameObject exitButton;
    [SerializeField] GameObject continueButton;

    public int destroyedCount = 0;
    public int highScore = 0;
    private float cooldownTime = 1f;
    private float nextRaycastTime = 0f;
    private bool hasShownWinScreen = false;

    void Start()
    {
        ResetDestroyedCount();
        LoadHighScore();
        UpdateDestroyedCountText();
        UpdateHighScoreText();
        notificationText.text = "";
        exitButton.SetActive(false);
        continueButton.SetActive(false);
        exitButton.GetComponent<Button>().onClick.AddListener(ExitGame);
        continueButton.GetComponent<Button>().onClick.AddListener(ContinueGame);
    }

    void Update()
    {
        if (Time.time >= nextRaycastTime)
        {
            PerformRaycast();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            ResetHighScore();
            UpdateHighScoreText();
        }
    }

    // Thêm phương thức này để có thể gọi từ NewTestScript.cs
    public bool PerformRaycast()
    {
        if (Physics.Raycast(transform.position, transform.forward, out var hit, 5, layerMask))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.blue);
            Destroy(hit.transform.gameObject);
            destroyedCount++;
            UpdateDestroyedCountText();

            if (destroyedCount >= 10 && !hasShownWinScreen)
            {
                NotifyAndShowButtons();
                hasShownWinScreen = true;
            }

            if (destroyedCount > highScore)
            {
                highScore = destroyedCount;
                SaveHighScore();
                UpdateHighScoreText();
            }

            nextRaycastTime = Time.time + cooldownTime;
            return true; // Trúng mục tiêu
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * 5, Color.yellow);
            return false; // Không trúng mục tiêu
        }
    }

    public void UpdateDestroyedCountText()
    {
        if (destroyedCountText != null)
        {
            destroyedCountText.text = "Loot: " + destroyedCount.ToString();
        }
    }

    public void UpdateHighScoreText()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore.ToString();
        }
    }

    public void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }

    public void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public void ResetHighScore()
    {
        highScore = 0;
        SaveHighScore();
    }

    public void ResetDestroyedCount()
    {
        destroyedCount = 0;
        SaveDestroyedCount();
    }

    public void SaveDestroyedCount()
    {
        PlayerPrefs.SetInt("DestroyedCount", destroyedCount);
        PlayerPrefs.Save();
    }

    public void NotifyAndShowButtons()
    {
        notificationText.text = "You have reached the score of 10! Click Continue to keep playing or Exit to leave.";
        Debug.Log("You have reached the score of 10! Click Continue to keep playing or Exit to leave.");
        exitButton.SetActive(true);
        continueButton.SetActive(true);
        Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        notificationText.text = "";
        exitButton.SetActive(false);
        continueButton.SetActive(false);
        Time.timeScale = 1;
    }

    void ExitGame()
    {
        Application.Quit();
    }
}
