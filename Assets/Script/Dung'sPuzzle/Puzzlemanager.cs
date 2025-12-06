using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Puzzlemanager : MonoBehaviour
{
    public static Puzzlemanager Instance;

    public GameObject winPanel;
    public GameObject losePanel;
    public TextMeshProUGUI timerText;

    public Transform[] spawnPoints;
    public Puzzlepiece[] pieces;

    public float timeLimit = 60f;
    private bool gameEnded = false;
    private int totalPieces;
    private int piecesLocked = 0;

    public string currentLevelID = "Minigame1";
    public string defaultRoomScene = "Phòng ngủ";
    public string rewardLetterID = "Thu_1";
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        pieces = FindObjectsOfType<Puzzlepiece>();

 
        totalPieces = pieces.Length;

        if (spawnPoints.Length < pieces.Length)
        {
            return;
        }

        ShuffleAndSpawn();
    }

    void ShuffleAndSpawn()
    {
        List<Transform> availablePoints = new List<Transform>(spawnPoints);

        foreach (var piece in pieces)
        {
            if (availablePoints.Count > 0)
            {
                int randomIndex = Random.Range(0, availablePoints.Count);
                Transform randomPoint = availablePoints[randomIndex];

                // Di chuyển mảnh ghép đến vị trí spawn
                piece.transform.position = randomPoint.position;

                availablePoints.RemoveAt(randomIndex);
            }
        }
    }

    void Update()
    {
        if (gameEnded) return;

        if (timeLimit > 0)
        {
            timeLimit -= Time.deltaTime;
            if (timerText != null) timerText.text = "Time: " + Mathf.Round(timeLimit).ToString();
        }
        else
        {
            GameOver();
        }
    }

    public void CheckWinCondition()
    {
        piecesLocked++;
        // Debug để kiểm tra tiến độ

        if (piecesLocked >= totalPieces)
        {
            Victory();
        }
    }

    void Victory()
    {
        gameEnded = true;
        UIPopupEffect effect = winPanel.GetComponent<UIPopupEffect>();
        if (effect != null)
        {
            effect.Show(); // Gọi hàm hiện từ từ
        }
        else
        {
            // Cách 2: Nếu KHÔNG dùng script hiệu ứng, thì bật thủ công
            winPanel.SetActive(true);

            //Đảm bảo Alpha = 1 nếu lỡ có CanvasGroup
            CanvasGroup group = winPanel.GetComponent<CanvasGroup>();
            if (group != null)
            {
                group.alpha = 1f;
                group.blocksRaycasts = true;
            }
        }
        PlayerPrefs.SetInt(currentLevelID, 1);
        string letterKey = rewardLetterID;
        PlayerPrefs.SetInt(rewardLetterID, 1);
       
        PlayerPrefs.Save();

    }

    void GameOver()
    {
        gameEnded = true;

        UIPopupEffect effect = losePanel.GetComponent<UIPopupEffect>();
        if (effect != null)
        {
            effect.Show(); // Gọi hàm hiện từ từ
        }
        else
        {
            // Cách 2: Nếu KHÔNG dùng script hiệu ứng, thì bật thủ công
            losePanel.SetActive(true);

            // QUAN TRỌNG: Đảm bảo Alpha = 1 nếu lỡ có CanvasGroup
            CanvasGroup group = losePanel.GetComponent<CanvasGroup>();
            if (group != null)
            {
                group.alpha = 1f;
                group.blocksRaycasts = true;
            }
        }
    }

    public void ReturnToRoom()
    {

        string sceneToLoad = PlayerPrefs.GetString("LastScene", defaultRoomScene);

        Time.timeScale = 1f;

        //SceneManager.LoadScene(sceneToLoad);
        LevelLoader.Instance.LoadLevel(sceneToLoad);
    }
}