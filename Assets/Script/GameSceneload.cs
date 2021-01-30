using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneload : MonoBehaviour
{
    public void GoGameScene()
    {
        //추가
        //임시변경
        //기존: SceneManager.LoadScene("game");
        SceneManager.LoadScene("new");
            Debug.Log("플레이어가 아무 키를 누르고 있는 중입니다.");
    }
}

