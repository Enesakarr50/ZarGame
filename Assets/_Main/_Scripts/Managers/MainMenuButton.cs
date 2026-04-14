using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    const string MainGameSceneName = "_Main";

    /// <summary>_Main içindeyken oyunu ba?lat?r; ba?ka sahneden _Main'e yükler.</summary>
    public void OnPlayButtonClicked() {
        var s = SceneManager.GetActiveScene();
        if (s.name == MainGameSceneName || s.buildIndex == 0) {
            LevelList.Instance?.StartSelectedLevel();
            return;
        }
        SceneManager.LoadScene(MainGameSceneName);
    }

    public void OnMainMenuButtonClicked()
    {
        // Yeni bir sahneye geçiþ yap (bu örnekte sahne "Game" olarak varsayýlmýþtýr)
        SceneManager.LoadScene("MainMenu"); // Burada "Game" sahnesi, mevcut oyun sahnenizi temsil etmeli
    }

    // Quit butonuna týklandýðýnda yapýlacak iþlemler
    public void OnQuitButtonClicked()
    {
        // Oyunu kapat
        Debug.Log("Quit Game");
        Application.Quit();

        // Editor'de oyun kapatýlmaya çalýþýldýðýnda, uygulama kapanmaz, ancak aþaðýdaki kodu editor'de oyun kapandýðýný simüle eder:
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
