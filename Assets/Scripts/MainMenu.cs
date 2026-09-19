using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [Header("Game背景音樂")]
    public AudioClip mainMenuBGM ; //主畫面的音樂
    
    private void Start()
    {
        // 按下Start後，AudioManager播放MainMenuBGM
        if (AudioManager.Instance != null && mainMenuBGM != null)
        {
            AudioManager.Instance.PlayBGM(mainMenuBGM);
        }
    }

    public void StartGame()
    {
        Debug.Log("Start Game");
        SceneManager.LoadScene("SampleScene"); // 載入遊戲場景
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}