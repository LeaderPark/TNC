using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarTime : MonoBehaviour
{
    public Text timetext;

    // Update is called once per frame
    void Update()
    {
        timetext.text = "Clear Time : " + Mathf.Round(GameManager.instance.LimitTIme);
    }
}
