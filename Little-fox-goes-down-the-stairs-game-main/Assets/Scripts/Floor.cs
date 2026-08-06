using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f ;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(0, moveSpeed*Time.deltaTime,0); // 平台向上移動（模擬下樓）
        if(transform.position.y >6f) // 當平台移出畫面上方時，刪除並生成新平台
        {
            Destroy(gameObject);
            transform.parent.GetComponent<FloorManager>().SpawnFloor() ;
        }
    }
}