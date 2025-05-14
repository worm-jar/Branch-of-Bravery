using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YourTurn : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Timer.waitTimer = 18f;
        this.gameObject.SetActive(false);
    }
}
