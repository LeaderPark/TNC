using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneload : MonoBehaviour
{
    public void GoGameScene()
    {
        //�߰�
        //�ӽú���
        //����: SceneManager.LoadScene("game");
        SceneManager.LoadScene("new");
            Debug.Log("�÷��̾ �ƹ� Ű�� ������ �ִ� ���Դϴ�.");
    }
}

