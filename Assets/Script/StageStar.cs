using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageStar : MonoBehaviour
{
    public float star1 = 30;
    public float star2 = 60f;
    public float star3 = 90f;

    public bool isplayer = false;

    Player player;
    Rigidbody2D rigid;

    public GameObject maincanvas;
    public GameObject starcanvas;
    public GameObject star1ob;
    public GameObject star2ob;
    public GameObject star3ob;

    void Awake()
    {
        player = FindObjectOfType<Player>();
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isplayer)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                print("f입력");
                if (0 < player.currentHP && player.currentHP < star1)
                {
                    maincanvas.gameObject.SetActive(false);
                    starcanvas.gameObject.SetActive(true);
                    star1ob.gameObject.SetActive(true);
                    Debug.Log("1별");
                    Time.timeScale = 0;
                    GameManager.instance.oneStarClear = true;
                }

                if (star1 <= player.currentHP && player.currentHP < star2)
                {
                    maincanvas.gameObject.SetActive(false);
                    starcanvas.gameObject.SetActive(true);
                    star2ob.gameObject.SetActive(true);
                    Debug.Log("2별");
                    Time.timeScale = 0;
                    GameManager.instance.twoStarClear = true;
                }

                if (star2 <= player.currentHP && player.currentHP <= 100)
                {
                    maincanvas.gameObject.SetActive(false);
                    starcanvas.gameObject.SetActive(true);
                    star3ob.gameObject.SetActive(true);
                    Debug.Log("3별");
                    Time.timeScale = 0;
                    GameManager.instance.threeStarClear = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player") //Player 이라는 태그의 object에 닿았을때 실행
        {
            isplayer = true;
            Debug.Log("플레이어in");
        }
    }
    private void OnTriggerExit2D(Collider2D col) //object에서 나왔을때를 체크
    {
        if (col.gameObject.tag == "Player") //Player 이라는 태그의 object에 닿았을때 실행
        {
            isplayer = false;
            Debug.Log("플레이어out");
        }
    }
}

