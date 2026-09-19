using UnityEngine;
using UnityEngine.SceneManagement;

using System.Runtime.InteropServices; // 必須引入此命名空間以使用 DllImport
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

    // 2. 宣告調用剛才建立的 WebGLQuit.jslib 內的方法
    [DllImport("__Internal")]
    private static extern void QuitWebGL();

    public void StartGame()
    {
        Debug.Log("Start Game");
        SceneManager.LoadScene("SampleScene"); // 載入遊戲場景
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");

        #if UNITY_WEBGL && !UNITY_EDITOR
            // 如果是在網頁 (WebGL) 平台上運行，調用 JavaScript 關閉分頁
            QuitWebGL();
        #else
            // 如果是在 Unity 編輯器測試，或是打包成單機版 (PC/Mac)
            Application.Quit();
        #endif
    }
}