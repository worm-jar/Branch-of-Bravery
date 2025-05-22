using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static DDGameManager instance = null;
    private void Awake()
    {
        Screen.SetResolution(1920, 1080, true);
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Update()
    {

    }
}
