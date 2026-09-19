using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningPanel : MonoBehaviour
{
    [Header("設定面板物件")]
    public GameObject learningPanel ;
    
    [Header("關閉的其他面板")]
    public GameObject settingPanel ;

    // 設定面板ㄉ開關
    public void ToggleLearningPanel()
    {
        //防呆
        if(learningPanel != null)
        {
            // 用activeSelf 取得當前顯示狀態
            bool isCurrentlyActive = learningPanel.activeSelf ;
            bool nextState = !isCurrentlyActive ;
            learningPanel.SetActive(nextState) ; 
            
            // 按問號關設定
            if (nextState && settingPanel != null)
            {
                settingPanel.SetActive(false) ;
            }
        }
    }

    public void CloseLearningPanel()
    {
        if(learningPanel != null)
        {
            learningPanel.SetActive(false) ;
        }
    }
}
