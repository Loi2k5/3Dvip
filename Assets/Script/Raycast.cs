using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Raycast : MonoBehaviour
{
    [SerializeField]
    LayerMask layerMask;

    [SerializeField]
    TextMeshProUGUI destroyedCountText;
    [SerializeField]
    TextMeshProUGUI highScoreText;
    [SerializeField]
    TextMeshProUGUI notificationText;
    [SerializeField]
    GameObject exitButton;
    [SerializeField]
    GameObject continueButton;

    private int destroyedCount = 0;
    private int highScore = 0;
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
            }
            else
            {
                Debug.DrawRay(transform.position, transform.forward * 5, Color.yellow);
            }
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            ResetHighScore();
            UpdateHighScoreText();
        }
    }

    void UpdateDestroyedCountText()
    {
        destroyedCountText.text = "Loot: " + destroyedCount.ToString();
    }

    void UpdateHighScoreText()
    {
        highScoreText.text = "High Score: " + highScore.ToString();
    }

    void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }

    void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    void ResetHighScore()
    {
        highScore = 0;
        SaveHighScore();
    }

    void ResetDestroyedCount()
    {
        destroyedCount = 0;
        SaveDestroyedCount();
    }

    void SaveDestroyedCount()
    {
        PlayerPrefs.SetInt("DestroyedCount", destroyedCount);
        PlayerPrefs.Save();
    }

    void NotifyAndShowButtons()
    {
        notificationText.text = "You have reached the score of 10! Click Continue to keep playing or Exit to leave.";
        Debug.Log("You have reached the score of 10! Click Continue to keep playing or Exit to leave.");
        exitButton.SetActive(true);
        continueButton.SetActive(true);
        Time.timeScale = 0;
    }

    void ContinueGame()
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