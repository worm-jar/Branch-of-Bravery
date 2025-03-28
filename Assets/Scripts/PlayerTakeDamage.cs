using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamage : MonoBehaviour
{
    public Rigidbody2D _rig;
    public SpriteRenderer _renderer;
    public Animator _animator;
    public GameObject _annabeth;
    public GameObject _fakeAnnabeth;
    public float knockBack;
    public static float timerIFrames;
    public CameraShake Camera;
    public static bool _isInvincible = false;
    // Start is called before the first frame update
    void Start()
    {
        _rig = this.gameObject.GetComponent<Rigidbody2D>();
        _animator = this.gameObject.GetComponent<Animator>();
        _renderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _annabeth = GameObject.Find("Annabeth");
        _fakeAnnabeth = GameObject.Find("Fake Annabeth");
        _animator.SetBool("IsInvincible", _isInvincible);
        if (timerIFrames > 0)
        {
            timerIFrames -= Time.deltaTime;
            if (timerIFrames <= 0)
            {
                Camera.amount = 0f;
                if (PlayerHealth.health > 0)
                {
                    this.gameObject.layer = LayerMask.NameToLayer("Player");
                    _renderer.color = Color.white;
                    _isInvincible = false;
                }
                timerIFrames = 0;
            }
        }
    }
    //public void OnCollisionEnter2D(Collision2D collision2D)
    //{
    //    if (collision2D.gameObject.CompareTag("Enemy"))
    //    {
    //        PlayerHealth.health -= 7.5f;
    //    }
    //}
    public void OnTriggerEnter2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("EnemyAttack"))
        {
            if (EnemyAI.transitionSecondPhase == true)
            {
                PlayerHealth.health -= 13.5f;
            }
            else
            {
                PlayerHealth.health -= 8.5f;

            }
            Camera.amount = 0.5f;
            if (_fakeAnnabeth == null)
            {
                this.gameObject.layer = LayerMask.NameToLayer("Invincible");
                _isInvincible = true;
                _renderer.color = Color.black;
                //_animator.Play("Invincible");
                float relativePos = transform.position.x - _annabeth.transform.position.x;
                _rig.AddForce(new Vector2(relativePos * knockBack, 3.5f), ForceMode2D.Impulse);
                _rig.velocity = new Vector2(Mathf.Clamp(_rig.velocity.x, -5f, 5f), _rig.velocity.y);
                if(PlayerHealth.health <= 0)
                {
                    _rig.AddForce(new Vector2(relativePos * knockBack * 1.3f, 5.5f), ForceMode2D.Impulse);
                }
                timerIFrames = 0.25f;
            }
        }
    }
}
