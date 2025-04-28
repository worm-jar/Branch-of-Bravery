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
    private float normDirection;
    private float normDirectionStore;
    public int randomBehavior;
    public float speed;
    public float speedSecond;
    public GameObject _projectile;
    public GameObject _projectileSecond;
    public float attackTimer;
    public float attackTimerBeforeMove;
    public float attackTimerSecond;
    public float attackTimerBeforeDone;
    public float launchTimer;
    public GameObject _bow;
    public GameObject _sword;
    public RuntimeAnimatorController anim2;
    // Start is called before the first frame update
    void Start()
    {
        _rig = this.gameObject.GetComponent<Rigidbody2D>();
        _animator = this.gameObject.GetComponent<Animator>();
        _player = GameObject.Find("Player");
        randomBehavior = 0;
    }
    // Update is called once per frame
    void Update()
    {
        _animator.SetBool("IsAttacking", isAttacking);
        _animator.SetBool("IsJumpBack", isJumpBack);
        direction = (_player.transform.position.x - transform.position.x);
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
                ProjectileSpawnSecond();
                randomBehavior = 0;
                forceOnce = false;
                behavioring = false;
                isJumpBack = false;
                attackTimerSecond = 0;
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
            if(direction < 0)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                _bow.SetActive(true);
            }
            if(direction > 0)
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
            if (!isAttacking)
            {
                //_animator.Play("WalkAnnabeth");
                _rig.position += new Vector2(speed * normDirection * Time.deltaTime, 0f);
            }
            if (direction < 2 && direction > -2)
            {
                _rig.velocity = new Vector2(0, 0);
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
            if (!isAttacking)
            {
                //_animator.Play("WalkAnnabeth");
                _rig.position += new Vector2(speedSecond * normDirection * Time.deltaTime, 0f);
            }
            if (direction < 1.1f && direction > -1.1f)
            {
                _rig.velocity = new Vector2(0, 0);
                isAttacking = true;
                //_animator.Play("AttackAnna");
                launchTimer = 0.05f;
                attackTimerBeforeDone = 0.75f;
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
                attackTimerSecond = 0.75f;
                _rig.AddForce(new Vector2(-normDirection * 5, 3.5f), ForceMode2D.Impulse);
                forceOnce = true;
            }
            behavioring = true;
        }
    }
    public void ProjectileSpawnSecond()
    {
        isAttacking = false;
        Instantiate(_projectileSecond, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -3.3f), Quaternion.identity);
    }
    public void Transition()
    {
        _animator.runtimeAnimatorController = anim2 as RuntimeAnimatorController;
        _animator.SetBool("IsTransition", true);
        //throw sword here
        _animator.SetBool("IsTransition", false);
    }
}
