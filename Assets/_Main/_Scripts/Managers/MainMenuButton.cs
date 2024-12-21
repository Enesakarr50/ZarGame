using UnityEngine;
using UnityEngine.SceneManagement; // Scene geçiþi için
using UnityEngine.UI; // UI bileþenlerini kullanabilmek için

public class MainMenuButton : MonoBehaviour
{


  

    // Play butonuna týklandýðýnda yapýlacak iþlemler
    public void OnPlayButtonClicked()
    {
        // Yeni bir sahneye geçiþ yap (bu örnekte sahne "Game" olarak varsayýlmýþtýr)
        SceneManager.LoadScene("_Main"); // Burada "Game" sahnesi, mevcut oyun sahnenizi temsil etmeli
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
