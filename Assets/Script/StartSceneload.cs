using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneload : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("start");
            Debug.Log("�÷��̾ �ƹ� Ű�� ������ �ִ� ���Դϴ�.");
        }
    }
}