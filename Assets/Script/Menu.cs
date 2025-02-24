using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public static bool isMenuActive = false; // Thêm biến tĩnh để lưu trạng thái của menu

    public GameObject menuPanel; // Panel UI cho menu
    public GameObject settingsPanel; // Panel UI cho cài đặt
    public Button resumeButton; // Nút Resume
    public Button optionsButton; // Nút Options
    public Button quitButton; // Nút Quit

    void Start()
    {
        menuPanel.SetActive(false); // Ẩn panel menu khi bắt đầu
        settingsPanel.SetActive(false); // Ẩn panel cài đặt khi bắt đầu

        // Gán chức năng cho các nút
        resumeButton.onClick.AddListener(ResumeGame);
        optionsButton.onClick.AddListener(ShowOptions);
        quitButton.onClick.AddListener(QuitGame);
    }

    void Update()
    {
        // Kiểm tra nếu phím ESC được nhấn
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Chuyển đổi trạng thái hiển thị của panel menu
            isMenuActive = !isMenuActive;
            menuPanel.SetActive(isMenuActive);

            // Khóa và hủy khóa con trỏ chuột khi menu được mở hoặc đóng
            Cursor.lockState = isMenuActive ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isMenuActive;

            // Tạm dừng hoặc tiếp tục trò chơi khi menu được mở hoặc đóng
            Time.timeScale = isMenuActive ? 0 : 1;
        }
    }

    // Hàm ResumeGame để tiếp tục trò chơi
    void ResumeGame()
    {
        isMenuActive = false;
        menuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    // Hàm ShowOptions để hiển thị tùy chọn cài đặt
    void ShowOptions()
    {
        settingsPanel.SetActive(true);
    }

    // Hàm QuitGame để thoát trò chơi
    void QuitGame()
    {
        Application.Quit();
    }
}
