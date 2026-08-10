using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {

        // 讀取動態全局速度（時間越久速度越快）
        transform.Translate(0, FloorManager.GlobalSpeed * Time.deltaTime, 0);

        // 移出畫面上方時直接摧毀（生成已改由 FloorManager 統一計時控制）
        if (transform.position.y > 6f)
        {
            Destroy(gameObject);
        }

    }
}