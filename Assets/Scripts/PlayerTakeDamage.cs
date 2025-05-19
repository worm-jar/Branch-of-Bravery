using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamage : MonoBehaviour
{
    public Rigidbody2D _rig;
    public SpriteRenderer _renderer;
    public GameObject _slashSound;
    public AudioSource _slashAudio;
    public Animator _animator;
    public GameObject _annabeth;
    public GameObject _fakeAnnabeth;
    public float knockBack;
    public static float timerIFrames;
    public CameraShake Camera;
    public static bool _isInvincible = false;
    public bool strongInterruptable;
    public GameObject _hurtSource;
    public AudioSource _hurtAudio;
    // Start is called before the first frame update
    void Start()
    {
        _slashAudio = _slashSound.GetComponent<AudioSource>();
        _rig = this.gameObject.GetComponent<Rigidbody2D>();
        _renderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _hurtAudio = _hurtSource.GetComponent<AudioSource>();
        _animator = this.gameObject.GetComponent<Animator>();
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
        if(collision2D.gameObject.CompareTag("Norm Enemy Attack"))
        {
            _hurtAudio.Play();
            StartCoroutine(ScreenStop());
            if (collision2D.gameObject.name == "FlyHB")
            {
                PlayerHealth.health -= 8f;
            }
            else if(collision2D.gameObject.name == "JumpManHB")
            {
                PlayerHealth.health -= 11f;
            }
            this.gameObject.layer = LayerMask.NameToLayer("Invincible");
            PlayerAttack.timerLightAttackWait = 0.85f;
            _isInvincible = true;
            _renderer.color = Color.black;
            float relativePos = transform.position.x - collision2D.gameObject.transform.position.x;
            _rig.AddForce(new Vector2(relativePos * knockBack, 4.5f), ForceMode2D.Impulse);
            _rig.velocity = new Vector2(Mathf.Clamp(_rig.velocity.x, -8f, 8f), Mathf.Clamp(_rig.velocity.y, 0f, 3.5f));
            if (PlayerHealth.health <= 0)
            {
                _rig.AddForce(new Vector2(relativePos * knockBack * 1.3f, 5.5f), ForceMode2D.Impulse);
            }
            timerIFrames = 0.55f;
            Camera.amount = 0.5f;
            if (strongInterruptable)
            {
                _slashAudio.Stop();
                _animator.Play("KnockBack");
            }

        }
        else if (collision2D.gameObject.CompareTag("EnemyAttack"))
        {
            _hurtAudio.Play();
            StartCoroutine(ScreenStop());
            if (EnemyAI.transitionSecondPhase == true)
            {
                PlayerHealth.health -= 13.5f;
            }
            else
            {
                PlayerHealth.health -= 10.5f;
            }
            if (_fakeAnnabeth == null)
            {
                this.gameObject.layer = LayerMask.NameToLayer("Invincible");
                PlayerAttack.timerLightAttackWait = 0.85f;
                _isInvincible = true;
                _renderer.color = Color.black;
                //_animator.Play("Invincible");
                float relativePos = transform.position.x - _annabeth.transform.position.x;
                _rig.AddForce(new Vector2(relativePos * knockBack, 4.5f), ForceMode2D.Impulse);
                _rig.velocity = new Vector2(Mathf.Clamp(_rig.velocity.x, -8f, 8f), Mathf.Clamp(_rig.velocity.y, 0f, 3.5f));
                if(PlayerHealth.health <= 0)
                {
                    _rig.AddForce(new Vector2(relativePos * knockBack * 1.3f, 5.5f), ForceMode2D.Impulse);
                }
                timerIFrames = 0.55f;
                Camera.amount = 0.5f;
                if(strongInterruptable)
                {
                    _slashAudio.Stop();
                    _animator.Play("KnockBack");
                }
            }
            else
            {
                FakeAnnaAI.autoRunTimer = 15f;
                this.gameObject.layer = LayerMask.NameToLayer("Invincible");
                _isInvincible = true;
                _renderer.color = Color.black;
                float relativePos = transform.position.x - _fakeAnnabeth.transform.position.x;
                _rig.AddForce(new Vector2(relativePos * knockBack, 4.5f), ForceMode2D.Impulse);
                _rig.velocity = new Vector2(Mathf.Clamp(_rig.velocity.x, -8f, 8f), Mathf.Clamp(_rig.velocity.y, 0f, 3.5f));
                if (PlayerHealth.health <= 0)
                {
                    _rig.AddForce(new Vector2(relativePos * knockBack * 1.3f, 5.5f), ForceMode2D.Impulse);
                }
                timerIFrames = 0.55f;
                Camera.amount = 0.5f;
                if (strongInterruptable)
                {
                    _slashAudio.Stop();
                    _animator.Play("KnockBack");
                }
            }
        }
    }
    public IEnumerator ScreenStop()
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(0.01f);
        Time.timeScale = 1f;
    }
}
