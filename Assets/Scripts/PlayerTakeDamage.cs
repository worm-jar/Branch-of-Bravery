using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamage : MonoBehaviour
{
    public Rigidbody2D _rig;
    public Animator _animator;
    public GameObject _annabeth;
    public GameObject _fakeAnnabeth;
    public float knockBack;
    public float timerIFrames;
    // Start is called before the first frame update
    void Start()
    {
        _rig = this.gameObject.GetComponent<Rigidbody2D>();
        _annabeth = GameObject.Find("Annabeth");
        _fakeAnnabeth = GameObject.Find("Fake Annabeth");
        _animator = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIFrames > 0)
        {
            timerIFrames -= Time.deltaTime;
            if (timerIFrames <= 0)
            {
                this.gameObject.layer = LayerMask.NameToLayer("Player");
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
            PlayerHealth.health -= 18f;
            if (_fakeAnnabeth == null)
            {
                this.gameObject.layer = LayerMask.NameToLayer("Invincible");
                _animator.Play("Invincible");
                float relativePos = transform.position.x - _annabeth.transform.position.x;
                _rig.AddForce(new Vector2(relativePos * knockBack, 3.5f), ForceMode2D.Impulse);
                _rig.velocity = new Vector2(Mathf.Clamp(_rig.velocity.x, -5f, 5f), _rig.velocity.y);
                timerIFrames = 0.35f;
            }
        }
    }
}
