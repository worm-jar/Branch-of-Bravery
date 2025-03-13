using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Animator _animator;
    public float speed;
    public InputActionAsset _asset;
    public Rigidbody2D _rig;
    public float _axisx;
    public float _axisy;
    public bool _grounded = false;
    public SpriteRenderer _sprite;
    public float timer = 0f;
    public float jumpForce;
    private bool _hasDashed = false;
    private bool _falling = false;
    // Start is called before the first frame update
    void Start()
    {
        _rig = this.GetComponent<Rigidbody2D>();
        _sprite = this.GetComponent<SpriteRenderer>();
        _animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _rig.position += new Vector2(_axisx * speed * Time.deltaTime, 0f);
        if (_axisx < 0)
        {
            _sprite.flipX = true;
        }
        else if (_axisx > 0)
        {
            _sprite.flipX = false;
        }
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
            }
        }
        if (timer > 0 && _grounded == true)
        {
            if (_falling == false)
            {
                _animator.Play("Jump up");
            }
            _rig.AddForce(new Vector2(0f, jumpForce*2f), ForceMode2D.Impulse);
            timer = 0f;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && _rig.velocity.y == 0)
        {
            _animator.Play("Jump land");
            _grounded = true;
            _falling = false;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && _rig.velocity.y == 0)
        {
            _grounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _grounded = false;
        }
    }
    private void OnEnable()
    {
        _asset.Enable();
        _asset.FindAction("Player/Move").started += HandleMove;
        _asset.FindAction("Player/Move").performed += HandleMove;
        _asset.FindAction("Player/Move").canceled += HandleMoveStop;

        _asset.FindAction("Player/Jump").started += HandleJump;
        _asset.FindAction("Player/Jump").performed += HandleJump;
        _asset.FindAction("Player/Jump").canceled += HandleJumpDown;
        
        _asset.FindAction("Player/Dash").started += HandleDash;
        _asset.FindAction("Player/Dash").performed += HandleDash;
        _asset.FindAction("Player/Dash").canceled += HandleDashCancel;
        
        _asset.FindAction("Player/Vertical").started += HandleUp;
        _asset.FindAction("Player/Vertical").performed += HandleUp;
        _asset.FindAction("Player/Vertical").canceled += HandleUp;
    }
    private void OnDisable()
    {
        _asset.Disable();
        _asset.FindAction("Player/Move").started -= HandleMove;
        _asset.FindAction("Player/Move").performed -= HandleMove;
        _asset.FindAction("Player/Move").canceled -= HandleMoveStop;

        _asset.FindAction("Player/Jump").started -= HandleJump;
        _asset.FindAction("Player/Jump").performed -= HandleJump;
        _asset.FindAction("Player/Jump").canceled -= HandleJumpDown;

        _asset.FindAction("Player/Dash").started -= HandleDash;
        _asset.FindAction("Player/Dash").performed -= HandleDash;
        _asset.FindAction("Player/Dash").canceled -= HandleDashCancel;

        _asset.FindAction("Player/Vertical").started -= HandleUp;
        _asset.FindAction("Player/Vertical").performed -= HandleUp;
        _asset.FindAction("Player/Vertical").canceled -= HandleUp;
    }
    public void HandleMove(InputAction.CallbackContext ctx)
    {
        _animator.Play("Walk");
        _axisx = ctx.ReadValue<float>();
    } 
    public void HandleMoveStop(InputAction.CallbackContext ctx)
    {
        _animator.Play("Idle");
        _axisx = ctx.ReadValue<float>();
    } 
    public void HandleUp(InputAction.CallbackContext ctx)
    {
        _axisy = ctx.ReadValue<float>();
    }
    public void HandleJump(InputAction.CallbackContext ctx)
    {
        if (_grounded)
        {
            if (_falling == false)
            {
                _animator.Play("Jump up");
            }
            _rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            timer = 0f;
        }
        else if (!ctx.canceled)
        {
            timer = 0.1f;
        }

        //if (ctx.ReadValue<float>() > 0.5f && _grounded && timer > 0)
        //{
        //   _rig.AddForce(new Vector2(0f, 7f), ForceMode2D.Impulse);
        //   timer = 0;
        //}
    }
    public void HandleDash(InputAction.CallbackContext ctx)
    {
        if ((_axisx != 0 || _axisy != 0) && !_hasDashed)
        {
            _animator.Play("Dash");
            _rig.velocity = Vector2.zero;
            _rig.AddForce(new Vector2(7f * _axisx, 10f * _axisy), ForceMode2D.Impulse);
            _hasDashed = true;
        }
    }
    public void HandleDashCancel(InputAction.CallbackContext ctx)
    {
        _rig.velocity = new Vector2(0f,0f);
        _hasDashed = false;
    }
    public void HandleJumpDown(InputAction.CallbackContext ctx)
    {
        if (!_hasDashed)
        {
            _animator.Play("Jump down");
            _rig.AddForce(new Vector2(0f, -7f), ForceMode2D.Impulse);
        }
    }
}
