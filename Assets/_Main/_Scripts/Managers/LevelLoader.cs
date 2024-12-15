using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class LevelLoader : MonoBehaviour
{
    public GameObject levelPrefab1;
    public GameObject levelPrefab2;
    public GameObject levelPrefab3;

    public GameObject mainMenuPanel;
    public GameObject InGameMenuPanel;
    public GameObject DeadScreen;

    public GameObject curtain;
    public TMP_Text curtainText;

    public TMP_Text moveLimitText; // Hareket limitini gösterecek text
    public TMP_Text levelIndexText; // Level numarasýný gösterecek yeni text

    private GameObject currentLevel;
    private GameObject lastLoadedLevelPrefab;

    private int remainingMoveLimit;
    private int currentLevelIndex = 0;

    private int totalEndTiles;
    private int completedEndTiles;

    private void Start()
    {
        if (curtain != null) curtain.SetActive(false);
    }

    public void LoadLevel1()
    {
        LoadLevel(levelPrefab1, new Vector3(3, 0, 12));
    }

    public void LoadLevel2()
    {
        LoadLevel(levelPrefab2, new Vector3(4, 0, 16));
    }

    public void LoadLevel3()
    {
        LoadLevel(levelPrefab3, new Vector3(1, 0, 11));
    }

    private void LoadLevel(GameObject levelPrefab, Vector3 position)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }

        // Yeni level'i yükle
        currentLevel = Instantiate(levelPrefab, position, Quaternion.identity);
        lastLoadedLevelPrefab = levelPrefab;

        LevelPrefab levelPrefabScript = currentLevel.GetComponent<LevelPrefab>();
        if (levelPrefabScript != null)
        {
            remainingMoveLimit = levelPrefabScript.moves;
            currentLevelIndex = levelPrefabScript.CurrentLevelIndex;
            totalEndTiles = levelPrefabScript.totalEndTiles; // EndTile sayýsýný alýyoruz
            completedEndTiles = 0; // Baþlangýçta tamamlanan EndTile sayýsý 0

            UpdateMoveLimitUI();
            UpdateLevelIndexUI(); // Level index deðerini ekrana yazdýr
            ShowCurtainWithLevelText("Level " + currentLevelIndex);
        }

        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(false);
            InGameMenuPanel.SetActive(true);
        }
    }

    private void ShowCurtainWithLevelText(string levelText)
    {
        if (curtain != null)
        {
            curtain.SetActive(true);
            if (curtainText != null)
            {
                curtainText.text = levelText;
            }
        }
    }

    public void DecreaseMoveLimit()
    {
        if (remainingMoveLimit > 0)
        {
            remainingMoveLimit--;
            UpdateMoveLimitUI();

            if (remainingMoveLimit == 0)
            {
                DeadScreen.SetActive(true);
            }
        }
    }

    private void UpdateMoveLimitUI()
    {
        if (moveLimitText != null)
        {
            moveLimitText.text = "" + remainingMoveLimit;
        }
    }

    private void UpdateLevelIndexUI()
    {
        if (levelIndexText != null)
        {
            levelIndexText.text = "Level: " + currentLevelIndex;
        }
    }

    // EndTile tamamlandýðýnda çaðrýlacak metod
    public void EndTileCompleted()
    {
        completedEndTiles++; // Tamamlanan EndTile sayýsýný artýr
        if (completedEndTiles == totalEndTiles) // Eðer tüm EndTile'lar tamamlandýysa
        {
            CompleteLevel();
        }
    }

    public void CompleteLevel()
    {
        if (curtain != null)
        {
            curtain.SetActive(false);
        }

        if (currentLevelIndex == 1)
        {
            // Bir saniyelik delay ekleyelim
            Invoke("LoadLevel2", 1f); // 1 saniye sonra Level2'yi yükle
        }
        else if (currentLevelIndex == 2)
        {
            // Bir saniyelik delay ekleyelim
            Invoke("LoadLevel3", 1f); // 1 saniye sonra Level3'ü yükle
        }
        else if (currentLevelIndex == 3)
        {
            Debug.Log("Tüm level'ler tamamlandý!");
        }
    }
}
