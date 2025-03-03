using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoEndController : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnVideoFinished; // Gọi hàm khi video kết thúc
    }

    void Update()
    {
        // Nếu người chơi nhấn phím Space, chuyển ngay sang scene game
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadGameScene();
        }
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        LoadGameScene();
    }

    void LoadGameScene()
    {
        SceneManager.LoadScene(0); // Đổi "1" thành tên scene game chính xác của bạn
    }
}
