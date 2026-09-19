using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get ; private set ; }

    [Header("背景音樂設定")]
    public AudioClip gameBGM ;

    private void Awake()
    {
        // 確保場景中只有一個 GameManager
        if (Instance == null)
        {
            Instance = this ;
        }
        else
        {
            Destroy(gameObject) ;
        }
    }

    private void Start()
    {
        // 進入 SampleScene 時切換遊戲音樂
        if (AudioManager.Instance != null && gameBGM != null)
        {
            AudioManager.Instance.PlayBGM(gameBGM) ;
        }
    }

    // 重新開始遊戲 (供死亡或重來按鈕呼叫)
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 回到主選單 (供選單按鈕呼叫)
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
