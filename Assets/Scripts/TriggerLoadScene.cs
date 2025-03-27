using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLoadScene : MonoBehaviour
{
    public static string sceneName;
    public GameObject GameManager;
    public SceneLoader SceneLoader;
    // Start is called before the first frame update
    void Start()
    {
        SceneLoader = GameManager.GetComponent<SceneLoader>();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        sceneName = other.name;
        SceneLoader.Load();
    }
}
