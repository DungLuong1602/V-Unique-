using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using System.Collections.Generic;

public class LetterManager : MonoBehaviour
{
    public static LetterManager Instance; 

    [System.Serializable]
    public class LetterData
    {
        public string id;           // Mã thư 
        public string title;        // Tiêu đề 
        [TextArea(5, 10)]           // Tạo ô nhập văn bản to đùng trong Editor
        public string content;      // Nội dung thư
        public Sprite iconLocked;   // Hình phong bì đóng
        public Sprite iconUnlocked; // Hình phong bì mở
    }

    [Header("Dữ liệu")]
    public List<LetterData> allLetters; // Danh sách tất cả thư trong game

    [Header("UI References")]
    public GameObject mailPanel;        // Panel chứa danh sách thư
    public GameObject readPanel;        // Panel để đọc thư
    public Transform listContentContainer; // Cái Content trong ScrollView (nơi chứa các nút)
    public GameObject letterButtonPrefab;  // Prefab cái nút bấm thư

    [Header("Reader UI")]
    public TextMeshProUGUI readTitle;   // Text tiêu đề trong bảng đọc
    public TextMeshProUGUI readBody;    // Text nội dung trong bảng đọc

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // Ẩn các bảng đi lúc đầu
        if (mailPanel) mailPanel.SetActive(false);
        if (readPanel) readPanel.SetActive(false);
    }

    //CHỨC NĂNG 1: MỞ DANH SÁCH THƯ
    public void OpenMailBox()
    {
        mailPanel.SetActive(true);
        RefreshList();
    }

    public void CloseMailBox()
    {
        mailPanel.SetActive(false);
    }

    // CHỨC NĂNG 2: VẼ LẠI DANH SÁCH (Phiên bản có Máy Dò Lỗi)
    void RefreshList()
    {
        // Xóa nút cũ
        foreach (Transform child in listContentContainer)
        {
            Destroy(child.gameObject);
        }

        Debug.Log("--- BẮT ĐẦU KIỂM TRA TÚI THƯ ---");

        // Duyệt qua từng lá thư
        foreach (var letter in allLetters)
        {
            // Tạo cái chìa khóa để kiểm tra
            string keyCheck = letter.id;

            // Đọc dữ liệu từ bộ nhớ
            int status = PlayerPrefs.GetInt(keyCheck, 0);
            bool isUnlocked = status == 1;

            // IN RA MÀN HÌNH ĐỂ SOI
            Debug.Log($"Đang tìm thư ID: '{letter.id}' (Key: {keyCheck}) - Trạng thái: {status}");

            if (isUnlocked)
            {
                GameObject btnObj = Instantiate(letterButtonPrefab, listContentContainer);
                btnObj.GetComponent<Image>().sprite = letter.iconUnlocked;
                btnObj.GetComponentInChildren<TextMeshProUGUI>().text = letter.title;
                btnObj.GetComponent<Button>().onClick.AddListener(() => OpenReader(letter));
            }
        }
        Debug.Log("----------------------------------");
    }

    //CHỨC NĂNG 3: ĐỌC THƯ 
    void OpenReader(LetterData data)
    {
        readPanel.SetActive(true);
        readTitle.text = data.title;
        readBody.text = data.content;
    }

    public void CloseReader()
    {
        readPanel.SetActive(false);
    }

    //CHỨC NĂNG 4: NHẬN THƯ 
    public void UnlockLetter(string letterID)
    {
        // Lưu vào bộ nhớ
        PlayerPrefs.SetInt("Letter_" + letterID, 1);
        PlayerPrefs.Save();
        Debug.Log("Đã nhận được thư: " + letterID);

        // (Tùy chọn) Có thể hiện thông báo "New Mail!" ở đây
    }
}