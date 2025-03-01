using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour
{
    public Slider masterVolumeSlider; // Thanh trượt điều khiển âm lượng chính
    public Slider bgmVolumeSlider; // Thanh trượt điều khiển âm lượng nhạc nền
    public Slider sfxVolumeSlider; // Thanh trượt điều khiển âm lượng hiệu ứng âm thanh

    public AudioSource bgmSource; // Nguồn âm thanh nhạc nền
    public AudioSource sfxSource; // Nguồn âm thanh hiệu ứng âm thanh

    private void Save()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolumeSlider.value / 100f); // Lưu giá trị của thanh trượt âm lượng chính
        PlayerPrefs.SetFloat("BGMVolume", bgmVolumeSlider.value / 100f); // Lưu giá trị của thanh trượt âm lượng nhạc nền
        PlayerPrefs.SetFloat("SFXVolume", sfxVolumeSlider.value / 100f); // Lưu giá trị của thanh trượt âm lượng hiệu ứng âm thanh
        PlayerPrefs.Save();

        // Cập nhật âm lượng
        UpdateAudio();
    }

    private void LoadPref()
    {
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        float bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.5f);

        masterVolumeSlider.value = masterVolume * 100f; // Tải giá trị của thanh trượt âm lượng chính
        bgmVolumeSlider.value = bgmVolume * 100f; // Tải giá trị của thanh trượt âm lượng nhạc nền
        sfxVolumeSlider.value = sfxVolume * 100f; // Tải giá trị của thanh trượt âm lượng hiệu ứng âm thanh

        // Cập nhật âm lượng ban đầu
        UpdateAudio();
    }

    // Cập nhật âm lượng dựa trên giá trị của các thanh trượt
    private void UpdateAudio()
    {
        AudioListener.volume = masterVolumeSlider.value / 100f; // Cập nhật âm lượng chính
        if (bgmSource != null)
        {
            bgmSource.volume = bgmVolumeSlider.value / 100f; // Cập nhật âm lượng nhạc nền
        }
        if (sfxSource != null)
        {
            sfxSource.volume = sfxVolumeSlider.value / 100f; // Cập nhật âm lượng hiệu ứng âm thanh
        }
    }

    // Start được gọi trước khung hình đầu tiên của trò chơi
    void Start()
    {
        LoadPref(); // Tải các giá trị đã lưu khi bắt đầu

        // Thêm các listeners để lưu cài đặt và cập nhật âm lượng bất cứ khi nào giá trị của thanh trượt thay đổi
        masterVolumeSlider.onValueChanged.AddListener(delegate { Save(); });
        bgmVolumeSlider.onValueChanged.AddListener(delegate { Save(); });
        sfxVolumeSlider.onValueChanged.AddListener(delegate { Save(); });
    }
}
