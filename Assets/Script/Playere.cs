using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour
{
    public Slider masterVolumeSlider; // Thanh trượt điều khiển âm lượng chính
    public Slider bgmVolumeSlider; // Thanh trượt điều khiển âm lượng nhạc nền
    public Slider sfxVolumeSlider; // Thanh trượt điều khiển âm lượng hiệu ứng âm thanh

    private void Save()
    {
        PlayerPrefs.SetInt("MasterVolume", (int)masterVolumeSlider.value); // Lưu giá trị của thanh trượt âm lượng chính
        PlayerPrefs.SetInt("BGMVolume", (int)bgmVolumeSlider.value); // Lưu giá trị của thanh trượt âm lượng nhạc nền
        PlayerPrefs.SetInt("SFXVolume", (int)sfxVolumeSlider.value); // Lưu giá trị của thanh trượt âm lượng hiệu ứng âm thanh
        PlayerPrefs.Save();
    }

    private void LoadPref()
    {
        int masterVolume = PlayerPrefs.GetInt("MasterVolume", 100);
        int bgmVolume = PlayerPrefs.GetInt("BGMVolume", 50);
        int sfxVolume = PlayerPrefs.GetInt("SFXVolume", 50);

        masterVolumeSlider.value = masterVolume; // Tải giá trị của thanh trượt âm lượng chính
        bgmVolumeSlider.value = bgmVolume; // Tải giá trị của thanh trượt âm lượng nhạc nền
        sfxVolumeSlider.value = sfxVolume; // Tải giá trị của thanh trượt âm lượng hiệu ứng âm thanh
    }

    // Start được gọi trước khung hình đầu tiên của trò chơi
    void Start()
    {
        LoadPref(); // Tải các giá trị đã lưu khi bắt đầu

        // Thêm các listeners để lưu cài đặt bất cứ khi nào giá trị của thanh trượt thay đổi
        masterVolumeSlider.onValueChanged.AddListener(delegate { Save(); });
        bgmVolumeSlider.onValueChanged.AddListener(delegate { Save(); });
        sfxVolumeSlider.onValueChanged.AddListener(delegate { Save(); });
    }
}
