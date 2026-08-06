using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField] GameObject[] FloorPrefebs ; 

    public void SpawnFloor()
    {
        int r = Random.Range(0,FloorPrefebs.Length);
        GameObject floor = Instantiate(FloorPrefebs[r] , transform); // 在此物件下生成隨機平台
        floor.transform.position = new Vector3(Random.Range(-2.6f,3.43f), -6f , 0f) ; // 設定生成位置（隨機X，固定在畫面下方）
    }
}