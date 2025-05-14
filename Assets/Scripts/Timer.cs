using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static float waitTimer;
    public GameObject _canvas;
    public void Update()
    {
        if (waitTimer > 0)
        {
            Debug.Log(waitTimer);
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0)
            {
                _canvas.SetActive(true);
                waitTimer = 0;
            }
        }
    }
}
