using UnityEngine;
using UnityEngine.SceneManagement;

public class TrailerController : MonoBehaviour
{
    // Kéo và thả đối tượng Button "Skipping" vào ô này trong Inspector
    public GameObject skippingButton; 
    
    // Tên Scene tiếp theo (Game/`Level 1)
    public string gameSceneName = "Phòng ngủ"; 

    void Start()
    {
        // Đảm bảo nút "Skipping" bị ẩn khi Scene Menu khởi động
        if (skippingButton != null)
        {
            skippingButton.SetActive(false);
        }
    }

    // HÀM 1: Gắn vào sự kiện OnClick() của nút "PLAY"
    public void StartTrailer()
    {
        // 1. Hiện nút "Skipping" ngay khi bắt đầu trailer
        if (skippingButton != null)
        {
            skippingButton.SetActive(true);
        }

        // 2. Bắt đầu tải và chạy Trailer/Video (Thêm code chạy video ở đây)
        // Ví dụ: Load Scene chứa Trailer hoặc chạy component VideoPlayer
        
        // **LƯU Ý QUAN TRỌNG:** // Nếu Trailer nằm trong Scene này, bạn cần thêm code chạy video.
        // Nếu Trailer là Scene khác, bạn cần Load Scene đó.
    }

    // HÀM 2: Gắn vào sự kiện OnClick() của nút "Skipping"
    public void SkipTrailer()
    {
        // Ẩn nút Skipping sau khi bấm
        if (skippingButton != null)
        {
            skippingButton.SetActive(false);
        }
        
        // Chuyển sang Scene chính của Game
        SceneManager.LoadScene(gameSceneName);
    }
    
    // *BONUS*: Nếu bạn muốn nút TỰ ĐỘNG ẩn sau khi trailer kết thúc:
    public void HideSkipButton()
    {
        if (skippingButton != null)
        {
            skippingButton.SetActive(false);
        }
    }
}