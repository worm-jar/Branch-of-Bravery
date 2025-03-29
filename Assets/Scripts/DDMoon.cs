using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDMoon : MonoBehaviour
{
    public static DDMoon instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(base.gameObject);
        }
        else
        {
            Destroy(base.gameObject);
        }
    }
}
