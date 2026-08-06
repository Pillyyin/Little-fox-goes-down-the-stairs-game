using System.Collections ;
using System.Collections.Generic ;
using TMPro ;
using Unity.VisualScripting ;
using UnityEngine ;
using UnityEngine.UI ;
using UnityEngine.SceneManagement ;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f ;
    GameObject currentFloor ; 

    [SerializeField] int Hp ;
    [SerializeField] GameObject HpBar ;
    [SerializeField]Text scoreText ;
    int score ;
    float scoreTime ;
    Animator anim ;
    SpriteRenderer render ;
    AudioSource deathsound ;
    [SerializeField] GameObject restartButton ;
    [SerializeField] GameObject mainMenuButton ;
    AudioSource  Backgroundmusic ;

    [Header("音效設定")]
    public AudioClip normalPlatformSound ; // 普通平台音效
    public AudioClip trapPlatformSound ;   // 陷阱平台音效
    public AudioClip topTrapSound ;        // 天花板陷阱音效

    void Start()
    {
        Hp = 10 ;
        score = 0 ;
        scoreTime = 0 ;
        anim = GetComponent<Animator>() ; 
        render = GetComponent<SpriteRenderer>() ; 
        deathsound = GetComponent<AudioSource>() ; 

        GameObject musicObject = GameObject.Find("Background") ;
        Backgroundmusic = musicObject.GetComponent<AudioSource>() ;
        Backgroundmusic.Play() ;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) // 向右移動
        {
            transform.Translate(moveSpeed*Time.deltaTime,0,0) ;
            render.flipX = false ; // 面向右邊
            anim.SetBool("run",true) ;
        }
        else if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) // 向左移動
        {
            transform.Translate(-moveSpeed*Time.deltaTime,0,0) ;
            render.flipX = true ; // 面向左邊
            anim.SetBool("run",true) ;
        }
        else
        {
            anim.SetBool("run",false) ;
        }
        UpdateScore() ; // 更新分數（幾層）
    }


    void OnCollisionEnter2D(Collision2D other) // 碰撞偵測（踩到平台或碰到陷阱）
    {
        if(other.gameObject.tag == "Normal") // 判斷 tag 是否為 Normal
        {
            if(other.contacts[0].normal == new Vector2(0f,1f)) // 從上方踩到（碰撞法線向上）
            {
                Debug.Log("踩到普通平台") ;
                currentFloor = other.gameObject ; // 記錄當前所在平台
                ModifyHp(1); // 踩到普通平台回血

                AudioManager.Instance.PlaySFX(normalPlatformSound) ; 
            }
        }
        else if(other.gameObject.tag == "Trap") // 判斷 tag 是否為 Trap
        {
            if(other.contacts[0].normal == new Vector2(0f,1f)) // 從上方踩到陷阱
            {
                Debug.Log("踩到陷阱平台") ;
                currentFloor = other.gameObject ; // 記錄當前所在平台
                ModifyHp(-3); // 踩到陷阱扣血
                anim.SetTrigger("hurt"); // 觸發受傷動畫

                AudioManager.Instance.PlaySFX(trapPlatformSound) ; 
            }
        }
        else if(other.gameObject.tag == "TopTrap") 
        {
            Debug.Log("碰到天花板陷阱") ; // 碰到天花板陷阱，讓當前平台消失
            currentFloor.GetComponent<BoxCollider2D>().enabled = false ;
            ModifyHp(-3) ; // 扣血
            anim.SetTrigger("hurt") ; // 觸發受傷動畫

            AudioManager.Instance.PlaySFX(topTrapSound) ; 
        }
    }

    void OnTriggerEnter2D(Collider2D other) // 觸發死亡線
    {
        if(other.gameObject.tag == "DeathLine")
        {
            Debug.Log("落下死亡") ;
            Die() ; 
        }
    }


    void ModifyHp(int num) // 修改血量
    {
        Hp += num ;
        if(Hp>10)
        {
            Hp = 10 ;
        }
        else if(Hp<=0)
        {
            Hp = 0 ;
            Die() ; 
        }
        UpdateHpBar() ; // 更新血條顯示
    }        

    void UpdateHpBar() // 更新血條顯示，SetActive 控制顯示(true)或隱藏(false)
    {
        for(int i=0; i<HpBar.transform.childCount; i++)
        {
            if(Hp>i)
            {
                HpBar.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                HpBar.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }


    void UpdateScore() // 更新分數（層數）
    {
        scoreTime += Time.deltaTime; // 每次 Update 累加時間
        if(scoreTime>2f)  // scoreTime 累積超過 2 秒
        {
            score++;
            scoreTime = 0f ; // 重置計時器
            scoreText.text = "地下" + score.ToString() + "層" ; // 顯示層數
        }
    }

    void Die()
    {
        /*
        deathsound.Play() ; // 播放死亡音效
        Time.timeScale = 0f ; // 時間縮放設為 0，遊戲暫停
        GameObject.Find("Background");
        Backgroundmusic.Pause();
        restartButton.SetActive(true) ; //重生按鈕出現
        mainMenuButton.SetActive(true) ; //回主畫面按鈕出現
        */
        if (deathsound != null) deathsound.Play(); // 播放死亡音效

        if (deathsound != null) deathsound.Play(); // 播放死亡音效

        // 🟢 呼叫 AudioManager 專用的 PauseBGM 方法
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PauseBGM();
        }

        Time.timeScale = 0f; // 時間縮放設為 0，遊戲暫停

        if (restartButton != null) restartButton.SetActive(true); // 重生按鈕出現
        if (mainMenuButton != null) mainMenuButton.SetActive(true); // 回主畫面按鈕出現
    }


    public void Restart()
    {
        Debug.Log("Restart");
        Time.timeScale = 1f ; // 時間縮放恢復為 1，遊戲繼續
        SceneManager.LoadScene("SampleScene") ; 
    }


    public void GameStart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f ; // 確保時間恢復正常
        SceneManager.LoadScene("MainMenu") ;
    }
}