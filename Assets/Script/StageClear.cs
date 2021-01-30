using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClear : MonoBehaviour
{
    public GameObject oneStar;
    public GameObject twoStar;
    public GameObject threestar;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance.oneStarClear == true)
        {
            oneStar.gameObject.SetActive(true);
        }
        if (GameManager.instance.twoStarClear == true)
        {
            twoStar.gameObject.SetActive(true);
        }
        if (GameManager.instance.threeStarClear == true)
        {
            threestar.gameObject.SetActive(true);
        }
    }
}
