using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FHD : MonoBehaviour
{
    void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.SetResolution(1920, 1080, true);
    }
}
