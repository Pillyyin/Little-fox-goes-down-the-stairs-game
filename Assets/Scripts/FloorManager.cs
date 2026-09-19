using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [Header("Floor Prefabs")]
    [SerializeField] GameObject[] FloorPrefebs ; 

    [Header("Spawn Settings")]
    [SerializeField] float spawnInterval = 1.3f ;     // 生成時間間隔
    [SerializeField] float minSpawnInterval = 0.6f ;  // 最小生成間隔

    [Header("Difficulty Settings")]
    public static float GlobalSpeed = 2f ;            // 基礎上升速度
    [SerializeField] float maxSpeed = 8f ;            // 最大上升速度
    [SerializeField] float speedAcceleration = 0.025f;// 每秒增加的速度

    private float timer = 0f ;
    private List<int> spawnQueue = new List<int>() ;  // 洗牌隊列
    
    void Start()
    {
        GlobalSpeed = 2f ;
        ShuffleQueue() ; // 遊戲開始先洗牌一次
    }

    void Update()
    {
        // 速度隨時間增加
        if (GlobalSpeed < maxSpeed)
        {
            GlobalSpeed += speedAcceleration * Time.deltaTime ;
        }

        // 速度變快，縮短生成間隔
        float currentInterval = Mathf.Max(minSpawnInterval, spawnInterval * (2f / GlobalSpeed)) ;

        // 定時生成
        timer += Time.deltaTime ;
        if (timer >= currentInterval)
        {
            SpawnFloor() ;
            timer = 0f ;
        }
    }

    public void SpawnFloor()
    {
        if (FloorPrefebs == null || FloorPrefebs.Length == 0) return;

        // 如果佇列空了，重新洗牌
        if (spawnQueue.Count == 0)
        {
            ShuffleQueue() ;
        }

        // 從洗好的牌堆中抽出一張
        int nextIndex = spawnQueue[0] ;
        spawnQueue.RemoveAt(0) ;

        GameObject floor = Instantiate(FloorPrefebs[nextIndex], transform) ; // 在此物件下生成
        floor.transform.position = new Vector3(Random.Range(-2.6f, 3.43f), -6f, 0f) ; // 設定生成位置
    }

    // Prefab using Fisher-Yates Shuffle
    void ShuffleQueue()
    {
        spawnQueue.Clear() ;
        for (int i = 0; i < FloorPrefebs.Length; i++)
        {
            spawnQueue.Add(i) ;
        }

        // 隨機打亂
        for (int i = 0; i < spawnQueue.Count; i++)
        {
            int temp = spawnQueue[i] ;
            int randomIndex = Random.Range(i, spawnQueue.Count) ;
            spawnQueue[i] = spawnQueue[randomIndex] ;
            spawnQueue[randomIndex] = temp ;
        }
    }
}