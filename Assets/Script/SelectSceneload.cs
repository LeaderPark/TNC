using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectSceneload : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("select");
            Debug.Log("플레이어가 아무 키를 누르고 있는 중입니다.");
        }
    }
}
