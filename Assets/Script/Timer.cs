using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public Text textTImer;
    public GameObject gameover;
    public GameObject maincanvas;

    private void Start()
    {
        GameManager.instance.LimitTIme = 999;
    }

    void Update()
    {
        GameManager.instance.LimitTIme -= Time.deltaTime; 
        textTImer.text = "Time Left : " + Mathf.Round(GameManager.instance.LimitTIme);

        if (GameManager.instance.LimitTIme <= 0)
        {
            maincanvas.gameObject.SetActive(false);
            gameover.gameObject.SetActive(true);
            Debug.Log("시간초과로 사망");
        }
    }
}
