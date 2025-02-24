using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Raycast : MonoBehaviour
{
    [SerializeField]
    LayerMask layerMask;

    [SerializeField]
    TextMeshProUGUI destroyedCountText;
    [SerializeField]
    TextMeshProUGUI highScoreText;

    private int destroyedCount = 0;
    private int highScore = 0;
    private float cooldownTime = 1f;  // Thời gian (giây) giữa các lần kiểm tra Raycast
    private float nextRaycastTime = 0f;

    void Start()
    {
        ResetDestroyedCount();  // Reset điểm số về 0 khi bắt đầu game mới
        LoadHighScore();  // Tải điểm cao nhất từ lần chơi trước
        UpdateDestroyedCountText();
        UpdateHighScoreText();
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

                if (destroyedCount > highScore)
                {
                    highScore = destroyedCount;
                    SaveHighScore();  // Lưu điểm cao nhất nếu đạt được điểm mới
                    UpdateHighScoreText();
                }

                nextRaycastTime = Time.time + cooldownTime;
            }
            else
            {
                Debug.DrawRay(transform.position, transform.forward * 5, Color.yellow);
            }
        }

        // Kiểm tra phím "O" để reset điểm cao nhất
        if (Input.GetKeyDown(KeyCode.O))
        {
            ResetHighScore();
            UpdateHighScoreText();
        }
    }

    void UpdateDestroyedCountText()
    {
        destroyedCountText.text = "Loot: " + destroyedCount.ToString();  // Cập nhật văn bản hiển thị số lượng đối tượng bị phá hủy
    }

    void UpdateHighScoreText()
    {
        highScoreText.text = "High Score: " + highScore.ToString();  // Cập nhật văn bản hiển thị điểm cao nhất
    }

    void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);  // Lưu điểm cao nhất vào PlayerPrefs
        PlayerPrefs.Save();  // Lưu tất cả các giá trị PlayerPrefs
    }

    void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);  // Tải điểm cao nhất từ PlayerPrefs
    }

    void ResetHighScore()
    {
        highScore = 0;  // Reset điểm cao nhất về 0
        SaveHighScore();  // Lưu điểm cao nhất đã reset vào PlayerPrefs
    }

    void ResetDestroyedCount()
    {
        destroyedCount = 0;  // Reset số lượng đối tượng bị phá hủy về 0
        SaveDestroyedCount();
    }

    void SaveDestroyedCount()
    {
        PlayerPrefs.SetInt("DestroyedCount", destroyedCount);  // Lưu số lượng đối tượng bị phá hủy vào PlayerPrefs
        PlayerPrefs.Save();  // Lưu tất cả các giá trị PlayerPrefs
    }

    void LoadDestroyedCount()
    {
        destroyedCount = PlayerPrefs.GetInt("DestroyedCount", 0);  // Tải số lượng đối tượng bị phá hủy từ PlayerPrefs
    }
}
