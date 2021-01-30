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
                print("f�Է�");
                if (0 < player.currentHP && player.currentHP < star1)
                {
                    maincanvas.gameObject.SetActive(false);
                    starcanvas.gameObject.SetActive(true);
                    star1ob.gameObject.SetActive(true);
                    Debug.Log("1��");
                    Time.timeScale = 0;
                    GameManager.instance.oneStarClear = true;
                }

                if (star1 <= player.currentHP && player.currentHP < star2)
                {
                    maincanvas.gameObject.SetActive(false);
                    starcanvas.gameObject.SetActive(true);
                    star2ob.gameObject.SetActive(true);
                    Debug.Log("2��");
                    Time.timeScale = 0;
                    GameManager.instance.twoStarClear = true;
                }

                if (star2 <= player.currentHP && player.currentHP <= 100)
                {
                    maincanvas.gameObject.SetActive(false);
                    starcanvas.gameObject.SetActive(true);
                    star3ob.gameObject.SetActive(true);
                    Debug.Log("3��");
                    Time.timeScale = 0;
                    GameManager.instance.threeStarClear = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player") //Player �̶�� �±��� object�� ������� ����
        {
            isplayer = true;
            Debug.Log("�÷��̾�in");
        }
    }
    private void OnTriggerExit2D(Collider2D col) //object���� ���������� üũ
    {
        if (col.gameObject.tag == "Player") //Player �̶�� �±��� object�� ������� ����
        {
            isplayer = false;
            Debug.Log("�÷��̾�out");
        }
    }
}

