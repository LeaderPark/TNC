using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HPSlider : MonoBehaviour
{
    private Slider hpBar;
    public Text HPtext;

    Player player;
    
    public GameObject gameover;
    public GameObject maincanvas;


    void Start()
    {
        player = FindObjectOfType<Player>();
        hpBar = GetComponent<Slider>();
    }

    void Update()
    {
        HPtext.text = Mathf.Round(player.currentHP) + "/" + player.maxHP;

        if (player.currentHP <= 0) //HP�� 0�� �Ǹ� ���ӿ��������� �̵�
        {
            //�߰�
            player.PlaySound("gameover");

            maincanvas.gameObject.SetActive(false);
            gameover.gameObject.SetActive(true);
            Debug.Log("HP�� 0�̵Ǿ� ���");
        }
    }

    public void sethp(float damage)
    {
        hpBar.value -= damage;
        Debug.Log(damage + (" ������"));
    }
}
