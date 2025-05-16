using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeAnnaAI : MonoBehaviour
{
    public Rigidbody2D _rig;
    public GameObject _player;
    public Animator _animator;
    public SpriteRenderer _sprite;
    public float attackTimer;
    public bool runTimer;
    public bool runTimerOnce;
    public bool waitAttack;
    public bool hasComeClose;
    public GameObject _projectile;
    public static float autoRunTimer;
    public static bool timerStart;
    public AudioSource _audioSource;
    public AudioClip _bells;
    public AudioClip _slash;
    public bool audioOnce = false;
    public GameObject _bow;
    public float timerBeforeDestroy;
    // Start is called before the first frame update
    void Start()
    {
        runTimerOnce = true;
        timerStart = false;
        waitAttack = false;
        hasComeClose = false;
        _player = GameObject.Find("Player");
        _animator = GetComponent<Animator>();
        _rig = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = transform.position.x - _player.transform.position.x;
        if (timerBeforeDestroy > 0) 
        {
            timerBeforeDestroy -= Time.deltaTime;
            if (timerBeforeDestroy <= 0)
            {
                runTimer = false;
                timerBeforeDestroy = 0;
            }
        }
        if (FakeAnnabethGone.gone == true && runTimer && runTimerOnce)
        {
            _animator.Play("WalkFakeAnna");
            _rig.velocity = new Vector2(4.5f, _rig.velocity.y);
            Destroy(this.gameObject, 10f);
            timerBeforeDestroy = 5f;
            runTimerOnce = false;
        }
        else if (FakeAnnabethGone.gone == true && !runTimer)
        {
            Destroy(this.gameObject);
        }
        if (autoRunTimer > 0)
        {
            autoRunTimer -= Time.deltaTime;
            if (autoRunTimer <= 0)
            {
                Run();
                autoRunTimer = 0;
            }
        }
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                _audioSource.clip = _bells;
                _audioSource.PlayDelayed(0.7f);
                ProjectileSpawn();
                attackTimer = 2.94f;
            }
        }
        //if (runTimer > 0)
        //{
        //    runTimer -= Time.deltaTime;
        //    if (runTimer <= 0)
        //    {
        //        FakeAnnabethGone.gone = true;
        //        runTimer = 0;
        //    }
        //}
        if (distance <= 9.5f)
        {
            hasComeClose = true;
            Attack();
            if (distance <= 2f)
            {
                Run();
            }
        }
        else if (hasComeClose == false)
        {
            _animator.Play("FakeAnnabethIdle");
        }
    }
    public void Attack()
    {
        if(timerStart == false)
        {
            autoRunTimer = 10f;
            timerStart = true;
        }
        if (waitAttack == false)
        {
            _animator.Play("AttackFakeAnna");
            attackTimer = 0.8f;
            waitAttack = true;
        }
    }
    public void Run()
    {
        FakeAnnabethGone.gone = true;
        _bow.SetActive(false);
        attackTimer = 0f;
        runTimer = true;
        _sprite.flipX = false;
    }
    public void ProjectileSpawn()
    {
        _audioSource.PlayOneShot(_slash);
        Instantiate(_projectile, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 0.5f, -3.3f), Quaternion.identity);
    }
}
