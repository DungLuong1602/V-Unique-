using UnityEngine;
using UnityEngine.SceneManagement; // Quan trọng: Cần namespace này

public class SceneSwitcher : MonoBehaviour
{

    public string nextSceneName = "Phòng Ngủ"; 

    // Hàm này sẽ được gọi khi nút "Skip" được nhấn
    public void SkipScene()
    {
        Debug.Log("Skipping to scene: " + nextSceneName);
        
        // Tải scene mới
        SceneManager.LoadScene(nextSceneName);
    }
}