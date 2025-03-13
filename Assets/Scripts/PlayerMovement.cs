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
    public float timer = 0f;
    public float timerDash = 0f;
    public float jumpForce;
    private bool _hasDashed = false;
    private bool _falling = false;
    private bool _landTriggered = false;
    // Start is called before the first frame update
    void Start()
    {
        _rig = this.GetComponent<Rigidbody2D>();
        _animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetFloat("Walking", _axisx);
        _animator.SetBool("Grounded", _grounded);
        if (PlayerAttack.isStrongAttacking == false)
        {
           _rig.position += new Vector2(_axisx * speed * Time.deltaTime, 0f);
        }

        if (_axisx < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (_axisx > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
            }
        }
        if (timerDash > 0)
        {
            timerDash -= Time.deltaTime;
            if (timerDash <= 0)
            {
                if (_hasDashed)
                {
                _rig.velocity = new Vector2(0,0);
                }
                timerDash = 0;
            }
        }
        if (timer > 0 && _grounded == true)
        {
            if (_falling == false && PlayerAttack.isStrongAttacking == false)
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
            if(PlayerAttack.isStrongAttacking == false)
            {
                _animator.Play("Jump land");
            }
            _grounded = true;
            _falling = false;
            _landTriggered = false;
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
        if (PlayerAttack.isStrongAttacking == false)
        { 
        _animator.Play("Walk");
        }
        _axisx = ctx.ReadValue<float>();
    } 
    public void HandleMoveStop(InputAction.CallbackContext ctx)
    {
        if (PlayerAttack.isStrongAttacking == false)
        {
            _animator.Play("Idle");
        }
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
            if (_falling == false && !_landTriggered)
            {
                if (PlayerAttack.isStrongAttacking == false)
                {
                    _animator.Play("Jump up");
                }
                _landTriggered = true;
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
        if ((_axisx != 0 || _axisy != 0) && !_hasDashed && _grounded)
        {
            if (PlayerAttack.isStrongAttacking == false)
            {
                _animator.Play("Dash");
                _rig.velocity = Vector2.zero;
                _rig.AddForce(new Vector2(11f * _axisx, 7f * _axisy), ForceMode2D.Impulse);
            }
            _hasDashed = true;
            if(!ctx.canceled)
            {
                timerDash = 0.4f;
            }
        }
        else if ((_axisx != 0 || _axisy != 0) && !_hasDashed && !_grounded)
        {
            if (PlayerAttack.isStrongAttacking == false)
            {
                _animator.Play("Dash");
                _rig.velocity = Vector2.zero;

                _rig.AddForce(new Vector2(8f * _axisx, 10f * _axisy), ForceMode2D.Impulse);
                _rig.velocity = new Vector2(_rig.velocity.x, Mathf.Clamp(_rig.velocity.y, -9f, 9f));
                _hasDashed = true;
            }
            if(!ctx.canceled)
            {
                timerDash = 0.4f;
            }
        }
    }
    public void HandleDashCancel(InputAction.CallbackContext ctx)
    {
        _rig.velocity = new Vector2(0f,0f);
        _hasDashed = false;
    }
    public void HandleJumpDown(InputAction.CallbackContext ctx)
    {
        if (!_hasDashed && !_grounded)
        {
            if (PlayerAttack.isStrongAttacking == false)
            {
                _animator.Play("Jump down");
            }
            _rig.AddForce(new Vector2(0f, -7f), ForceMode2D.Impulse);
        }
    }
}
