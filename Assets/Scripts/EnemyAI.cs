using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public bool secondPhase = false;
    public bool isAttacking = false;
    public bool isJumpBack = false;
    public bool forceOnce = false;
    public bool behavioring = false;
    public static bool transitionSecondPhase = true;
    public Animator _animator;
    public Rigidbody2D _rig;
    public GameObject _player;
    private float direction;
    public float normDirection;
    public static float storeNormDir;
    private float normDirectionStore;
    public int randomBehavior;
    public float speed;
    public float speedSecond;
    public GameObject _projectile;
    public GameObject _projectileSecond;
    public float attackTimer;
    public float attackTimerBeforeMove;
    public float attackTimerSecond;
    public float attackTimerSecond2;
    public float attackTimerBeforeDone;
    public float launchTimer;
    public GameObject _bow;
    public GameObject _sword;
    public RuntimeAnimatorController anim2;
    public bool dead;
    public GameObject _deadAnna;
    public bool transOnce;
    public bool transitioning;
    public float transitionTimer;
    public ParticleSystem _particleSystem;
    public AudioSource _audioSource;
    
    public AudioClip _bellJumpBack;
    public AudioClip _swordSwing;
    public AudioClip _walk;
    public AudioClip _transition;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        transOnce = true;
        dead = false;
        _rig = this.gameObject.GetComponent<Rigidbody2D>();
        _animator = this.gameObject.GetComponent<Animator>();
        _particleSystem = this.gameObject.GetComponent<ParticleSystem>();
        _player = GameObject.Find("Player");
        randomBehavior = 0;
    }
    // Update is called once per frame
    void Update()
    {
        _animator.SetBool("Dead", dead);
        _animator.SetBool("IsAttacking", isAttacking);
        _animator.SetBool("IsJumpBack", isJumpBack);
        direction = (_player.transform.position.x - transform.position.x);
        if(EnemyHealth.health <= 0)
        {
            dead = true;
        }
        if (dead == true)
        {
            Instantiate(_deadAnna, this.gameObject.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        if(EnemyHealth.health <= 100)
        {
            Transition();
            secondPhase = true;
            transitionSecondPhase = false;
        }
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                _audioSource.clip = _swordSwing;
                _audioSource.Play();
                attackTimerBeforeMove = 1f;
                ProjectileSpawn();
                randomBehavior = 0;
                forceOnce = false;
                isJumpBack = false;
                attackTimer = 0;
            }
        }
        if (attackTimerBeforeMove > 0)
        {
            attackTimerBeforeMove -= Time.deltaTime;
            if (attackTimerBeforeMove <= 0)
            {
                behavioring = false;
                attackTimerBeforeMove = 0;
            }
        }
        if (attackTimerSecond > 0)
        {
            attackTimerSecond -= Time.deltaTime;
            if (attackTimerSecond <= 0)
            {
                _audioSource.clip = _swordSwing;
                _audioSource.Play();
                _audioSource.PlayOneShot(_bellJumpBack);
                ProjectileSpawnSecond();
                //randomBehavior = 0;
                //forceOnce = false;
                //behavioring = false;
                isJumpBack = false;
                attackTimerSecond2 = 0.5f;
                attackTimerSecond = 0;
            }
        }
        if (attackTimerSecond2 > 0)
        {
            attackTimerSecond2 -= Time.deltaTime;
            if (attackTimerSecond2 <= 0)
            {
                _audioSource.clip = _swordSwing;
                _audioSource.Play();
                ProjectileSpawnSecond2();
                randomBehavior = 0;
                forceOnce = false;
                behavioring = false;
                isJumpBack = false;
                attackTimerSecond2 = 0;
            }
        }
        if (launchTimer > 0)
        {
            launchTimer -= Time.deltaTime;
            if (launchTimer <= 0)
            {
                _rig.AddForce(new Vector2(normDirectionStore*4f, 0f), ForceMode2D.Impulse);
                launchTimer = 0;
            }
        }
        if (attackTimerBeforeDone > 0)
        {
            attackTimerBeforeDone -= Time.deltaTime;
            if (attackTimerBeforeDone <= 0)
            {
                _rig.velocity = new Vector2(0, 0);
                isAttacking = false;
                behavioring = false;
                randomBehavior = Random.Range(0, 2);
                attackTimerBeforeDone = 0;
            }
        }
        if (transitionTimer > 0)
        {
            transitionTimer -= Time.deltaTime;
            if (transitionTimer <= 0)
            {
                _animator.SetBool("IsTransition", false);
                transitioning = false;
                transitionTimer = 0;
            }
        }
        if (direction < 0)
        {
            normDirection = -1;
        }
        else if (direction > 0)
        {
            normDirection = 1;
        }
        if(!isAttacking)
        {
            if(direction < 0 && !behavioring)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                _bow.SetActive(true);
            }
            if(direction > 0 && !behavioring)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                _bow.SetActive(false);
            }
        }
        if (secondPhase == false)
        {
            if (randomBehavior == 0)
            {
                MeleeAttack();
            }
            if (randomBehavior == 1)
            {
                RangedAttack();
            }
        }
        else if (secondPhase == true && transitionSecondPhase == false)
        {
            if (randomBehavior == 0)
            {
                MeleeAttackSecond();
            }
            if (randomBehavior == 1)
            {
                RangedAttackSecond();
            }
        }
    }
    public void MeleeAttack()
    {
        if (behavioring == false)
        {
            normDirectionStore = normDirection;
            if (!isAttacking && transitioning == false)
            {
                //_animator.Play("WalkAnnabeth");
                _audioSource.clip = _walk;
                _audioSource.Play();
                _rig.position += new Vector2(speed * normDirection * Time.deltaTime, 0f);
            }
            if (direction < 2 && direction > -2)
            {
                _audioSource.clip = _swordSwing;
                _audioSource.PlayDelayed(0.5f);
                //_rig.velocity = new Vector2(0, 0);
                isAttacking = true;
                //_animator.Play("AttackAnna");
                launchTimer = 0.6f;
                attackTimerBeforeDone = 1f;
                behavioring = true;
            }
        }
    }
    public void RangedAttack() 
    {
        if (behavioring == false)
        {
            //_animator.Play("Jump Back");
            isJumpBack = true;
            if (!forceOnce)
            {
                storeNormDir = normDirection;
                _audioSource.PlayOneShot(_bellJumpBack);
                _particleSystem.Play();
                attackTimer = 1.25f;
                _rig.AddForce(new Vector2(-normDirection * 5, 3.5f), ForceMode2D.Impulse);
                forceOnce = true;
            }
            behavioring = true;
        }
    }
    public void ProjectileSpawn()
    {
        isAttacking = false;
        Instantiate(_projectile, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 0.5f, -3.3f), Quaternion.identity);
    }
    public void MeleeAttackSecond()
    {
        if (behavioring == false)
        {
            normDirectionStore = normDirection;
            if (!isAttacking && transitioning == false)
            {
                //_animator.Play("WalkAnnabeth");
                _audioSource.clip = _walk;
                _audioSource.Play();
                _rig.position += new Vector2(speedSecond * normDirection * Time.deltaTime, 0f);
            }
            if (direction < 1.55f && direction > -1.55f)
            {
                _audioSource.clip = _swordSwing;
                _audioSource.PlayDelayed(0.3f);
                //_rig.velocity = new Vector2(0, 0);
                isAttacking = true;
                //_animator.Play("AttackAnna");
                launchTimer = 0.05f;
                attackTimerBeforeDone = 0.6f;
                behavioring = true;
            }
        }
    }
    public void RangedAttackSecond()
    {
        if (behavioring == false)
        {
            //_animator.Play("Jump Back");
            isJumpBack = true;
            if (!forceOnce)
            {
                storeNormDir = normDirection;
                _audioSource.PlayOneShot(_bellJumpBack);
                _particleSystem.Play();
                attackTimerSecond = 0.75f;
                _rig.AddForce(new Vector2(-normDirection * 5, 3.5f), ForceMode2D.Impulse);
                forceOnce = true;
            }
            behavioring = true;
        }
    }
    public void ProjectileSpawnSecond()
    {
        //isAttacking = false;
        Instantiate(_projectileSecond, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -3.3f), Quaternion.identity);
    }
    public void ProjectileSpawnSecond2()
    {
        isAttacking = false;
        Instantiate(_projectileSecond, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -3.3f), Quaternion.identity);
    }
    public void Transition()
    {
        if(transOnce == true)
        {
            _audioSource.PlayOneShot(_transition);
            transitioning = true;
            _animator.SetBool("IsTransition", true);
            _animator.runtimeAnimatorController = anim2 as RuntimeAnimatorController;
            Instantiate(_sword, transform.position, Quaternion.Euler(0, 0, 35));
            transOnce = false;
            transitionTimer = 1.2f;
        }
    }
}
