using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour
{
    public float respawnTimer;
    public GameObject GameManager;
    public SceneLoader SceneLoader;
    public bool deadOnce;
    public Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        deadOnce = true;
        GameManager = GameObject.Find("GameManager");
        SceneLoader = GameManager.GetComponent<SceneLoader>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (respawnTimer > 0)
        {
            respawnTimer -= Time.deltaTime;
            if (respawnTimer <= 0)
            {
                _animator.SetBool("IsDead", false);
                if (RespawnPoint.hasCheckpoint)
                {
                    TriggerLoadScene.sceneName = "Respawn Pre Fight";
                    SceneLoader.Load();
                    this.gameObject.layer = LayerMask.NameToLayer("Player");
                }
                else
                {
                    this.gameObject.layer = LayerMask.NameToLayer("Player");
                    TriggerLoadScene.sceneName = "Respawn Bridge";
                    SceneLoader.Load();
                }
                deadOnce = true;
                respawnTimer = 0;
            }
        }
        if (PlayerHealth.health <= 0 && deadOnce == true) 
        {
            _animator.SetBool("IsDead", true);
            FakeAnnaAI.autoRunTimer = 9999f;
            FakeAnnaAI.timerStart = false;
            this.gameObject.layer = LayerMask.NameToLayer("Dead");
            respawnTimer = 2f;
            deadOnce = false;
        }
    }
}
