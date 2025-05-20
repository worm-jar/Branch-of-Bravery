using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.Animations;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    public GameObject _canvasHeath;
    public Animator _animator;
    public float speed;
    public InputActionAsset _asset;
    public Rigidbody2D _rig;
    public static float _axisx;
    public float _axisy;
    public bool _grounded = false;
    public float timer = 0f;
    public float timerDash = 0f;
    public float jumpForce;
    public static bool _hasDashed = false;
    private bool _falling = false;
    private bool _landTriggered = false;
    public static bool _isDead;
    private bool _dashAir;
    public TrailRenderer _trail;
    public float deathTimer;
    public GameObject _interactSewer;
    public GameObject _interactJump;
    public GameObject _interactAttack;
    public GameObject _interactDash;
    public GameObject _interactAttack2;
    public GameObject _interactAttack3;
    public GameObject _interactAttack4;
    public static bool isInteracting;
    public static bool isRespInteracting;
    public bool dashAnimated;
    public float bridgeTimer;
    public AudioSource _audioSource;
    public float _respTimer;
    public ParticleSystem _particleSystem;
    public float soundTimer;
    public float DownTimer;
    public float DownJumpTimer;
    public bool downable;
    public CameraShake Camera;
    public static float _axisxStore;
    public PlayerTakeDamage PlayerTakeDamage;
    public ParticleSystem _dust;
    public ParticleSystem _dust0;
    public GameObject CanvasFade;
    public Image _grayImage;
    public Animator _grayAnim;
    public GameObject particleInteract;
    //public GameObject _respTr;
    //public GameObject _respTr0;
    //public GameObject _whiteImage;
    //public GameObject _whiteImage0;
    //public Image _whiteImageReal;
    //public Image _whiteImageReal0;
    //public Animator _whiteAnim;

    public AudioClip _walk;
    public AudioClip _jump;
    public AudioClip _dash;
    public AudioClip _paper;
    public AudioClip _sewer;
    // Start is called before the first frame update
    private void Awake()
    {
        _axisxStore = 1;
        _isDead = false;
        _dashAir = true;
    }
    void Start()
    {
        isInteracting = false;
        _dust = gameObject.transform.Find("Dust").gameObject.GetComponent<ParticleSystem>();
        _dust0 = gameObject.transform.Find("Dust (1)").gameObject.GetComponent<ParticleSystem>();
        PlayerTakeDamage = GetComponent<PlayerTakeDamage>();
        _particleSystem = this.gameObject.GetComponent<ParticleSystem>();
        _audioSource = GetComponent<AudioSource>();
        _rig = this.GetComponent<Rigidbody2D>();
        _animator = this.GetComponent<Animator>();
        _trail = this.GetComponent<TrailRenderer>();
        //CanvasFade = this.gameObject.transform.Find("GrayCanvas").gameObject;
        //_grayImage = CanvasFade.transform.Find("Image").gameObject.GetComponent<Image>();
        //_grayAnim = _grayImage.GetComponent<Animator>();
        //_respTr = GameObject.Find("RespawnTrigger");
        //_whiteImage = _respTr.transform.Find("WhiteCanvas").gameObject.transform.Find("Image").gameObject;
        //_whiteImageReal = _whiteImage0.GetComponent<Image>();
        //if (_whiteImage == null)
        //{
        //    _respTr0 = GameObject.Find("RespawnTrigger2");
        //    _whiteImage0 = _respTr.transform.Find("WhiteCanvas").gameObject.transform.Find("Image").gameObject;
        //    _whiteImageReal0 = _whiteImage0.GetComponent<Image>();
        //}
        //else if (_whiteImage0 == null)
        //{
        //    return;
        //}
        //_whiteAnim = _whiteImage.GetComponent<Animator>();
        //if (_whiteAnim == null)
        //{
        //    _whiteAnim = _whiteImage0.GetComponent<Animator>();
        //} 
    }
    // Update is called once per frame
    void Update()
    {
        particleInteract = this.gameObject.transform.Find("SewerParticle").gameObject;
        _particleSystem = particleInteract.GetComponent<ParticleSystem>();
        _canvasHeath = GameObject.Find("CanvasHealth");
        _interactSewer = _canvasHeath.transform.Find("Sewer").gameObject;
        _interactJump = _canvasHeath.transform.Find("Jump").gameObject;
        _interactAttack = _canvasHeath.transform.Find("Attack").gameObject;
        _interactAttack2 = _canvasHeath.transform.Find("Attack2").gameObject;
        _interactAttack3 = _canvasHeath.transform.Find("Attack3").gameObject;
        _interactAttack4 = _canvasHeath.transform.Find("Attack4").gameObject;
        _interactDash = _canvasHeath.transform.Find("Dash").gameObject;
        textMeshProUGUI = _interactSewer.transform.Find("ActualText").gameObject.GetComponent<TextMeshProUGUI>();
        _animator.SetFloat("Walking", _axisx);
        _animator.SetBool("Grounded", _grounded);
        _animator.SetBool("IsDashing", _hasDashed && !dashAnimated);
        _animator.SetBool("IsDead", _isDead);
        _animator.SetBool("IsInvincible", PlayerTakeDamage._isInvincible);
        _animator.SetBool("IsLAttacking", PlayerAttack.isLightAttacking);
        _animator.SetBool("IsHAttacking", PlayerAttack.isStrongAttacking);
        if (PlayerHealth.health <= 0)
        {
            _isDead = true;
            deathTimer = 0.5f;
        }
        if (PlayerAttack.isLightAttacking && _axisxStore == 0)
        {
            speed = 0.5f;
        }
        else
        {
            speed = 4;
        }
        if (PlayerAttack.isStrongAttacking == false && !_isDead)
        {
            _rig.position += new Vector2(_axisx * speed * Time.deltaTime, 0f);
        }
        if (PlayerAttack.isStrongAttacking == false && PlayerAttack.isLightAttacking == false && _axisx < 0 && !_isDead && !Pause.paused)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (PlayerAttack.isStrongAttacking == false && PlayerAttack.isLightAttacking == false && _axisx > 0 && !_isDead && !Pause.paused)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (PlayerAttack.isStrongAttacking == true && PlayerTakeDamage.strongInterruptable == true && _axisx > 0 && !_isDead && !Pause.paused)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (PlayerAttack.isStrongAttacking == true && PlayerTakeDamage.strongInterruptable == true && _axisx < 0 && !_isDead && !Pause.paused)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
            }
        }
        if (DownTimer > 0)
        {
            DownTimer -= Time.deltaTime;
            if (DownTimer <= 0)
            {
                downable = true;
                DownTimer = 0;
            }
        }
        if (DownJumpTimer > 0)
        {
            DownJumpTimer -= Time.deltaTime;
            if (DownJumpTimer <= 0)
            {
                _rig.AddForce(new Vector2(0f, -7f), ForceMode2D.Impulse);
                DownJumpTimer = 0;
            }
        }
        if (soundTimer > 0)
        {
            soundTimer -= Time.deltaTime;
            if (soundTimer <= 0)
            {
                _audioSource.Stop();
                soundTimer = 0;
            }
        }
        if (bridgeTimer > 0)
        {
            bridgeTimer -= Time.deltaTime;
            if (bridgeTimer <= 0)
            {
                _grounded = false;
                bridgeTimer = 0;
            }
        }
        if (deathTimer > 0)
        {
            deathTimer -= Time.deltaTime;
            if (deathTimer <= 0)
            {
                PlayerTakeDamage.timerIFrames = 0.2f;
                _isDead = false;
                deathTimer = 0;
            }
        }
        if (timerDash > 0)
        {
            timerDash -= Time.deltaTime;
            if (timerDash <= 0)
            {
                dashAnimated = true;
                if (_hasDashed)
                {
                _rig.velocity = new Vector2(0,0);
                }
                _trail.enabled = false;
                timerDash = 0;
            }
        }
        if (_respTimer > 0)
        {
            _respTimer -= Time.deltaTime;
            if (_respTimer <= 0)
            {
                Camera.amount = 0;
                isRespInteracting = false;
                _respTimer = 0;
            }
        }
        if (timer > 0 && _grounded == true)
        {
            _audioSource.PlayOneShot(_jump);
            _dust.Play();
            _rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            _rig.velocity = new Vector2(_rig.velocity.x, Mathf.Clamp(_rig.velocity.y, -9f, 12f));
            timer = 0f;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && _rig.velocity.y == 0)
        {
            if (PlayerAttack.isStrongAttacking == false && !_isDead)
            {
                //_animator.Play("Jump land");
            }
            _grounded = true;
            _dashAir = true;
            _falling = false;
            _landTriggered = false;
        }
        if(collision.gameObject.CompareTag("Bridge"))
        {
            bridgeTimer = 0f;
            _dashAir = true;
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
            _dashAir = true;
        }
        if (collision.gameObject.CompareTag("Bridge") && _rig.velocity.y == 0)
        {
            _grounded = true;
            _dashAir = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _grounded = false;
        }
        if (collision.gameObject.CompareTag("Bridge"))
        {
            bridgeTimer = 0.6f;
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
        
        _asset.FindAction("Player/Interact").started += HandleInteract;
        _asset.FindAction("Player/Interact").performed += HandleInteract;
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

        _asset.FindAction("Player/Interact").started -= HandleInteract;
        _asset.FindAction("Player/Interact").performed -= HandleInteract;
    }
    public void HandleMove(InputAction.CallbackContext ctx)
    {
        if (PlayerAttack.isStrongAttacking == false && !_isDead && !AnnaDeath.dead)
        {
            _audioSource.clip = _walk;
            _audioSource.Play();
        }
        _axisx = ctx.ReadValue<float>();
    } 
    public void HandleMoveStop(InputAction.CallbackContext ctx)
    {
        _axisxStore = _axisx;
        if (isInteracting)
        {
            soundTimer = 0.2f;
        }
        else if (PlayerAttack.isStrongAttacking == false && PlayerAttack.isLightAttacking == false && isRespInteracting == false)
        {
            _audioSource.Stop();
        }
        _axisx = ctx.ReadValue<float>();
    } 
    public void HandleUp(InputAction.CallbackContext ctx)
    {
        _axisy = ctx.ReadValue<float>();
    }
    public void HandleJump(InputAction.CallbackContext ctx)
    {
        if (_grounded && !_isDead && !Pause.paused)
        {
            if (_falling == false && !_landTriggered)
            {
                _landTriggered = true;
            }
            _dust.Play();
            downable = false;
            _audioSource.PlayOneShot(_jump);
            _rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            _rig.velocity = new Vector2(_rig.velocity.x, Mathf.Clamp(_rig.velocity.y, -9f, 12f));
            _grounded = false;
            DownTimer = 0.15f;
            timer = 0f;
        }
        else if (!ctx.canceled && !_isDead && !Pause.paused)
        {
            timer = 0.15f;
        }

        //if (ctx.ReadValue<float>() > 0.5f && _grounded && timer > 0)
        //{
        //   _rig.AddForce(new Vector2(0f, 7f), ForceMode2D.Impulse);
        //   timer = 0;
        //}
    }
    public void HandleDash(InputAction.CallbackContext ctx)
    {
        if (!_hasDashed && _grounded && _dashAir)
        {
            if (PlayerAttack.isStrongAttacking == false && !_isDead)
            {
                _dust0.Play();
                _audioSource.PlayOneShot(_dash);
                _trail.enabled = true;
                //_animator.Play("Dash");
                _rig.velocity = Vector2.zero;
                _rig.AddForce(new Vector2(11f * _axisx, 5.5f * _axisy), ForceMode2D.Impulse);
                if(_axisx == 0 && _axisy == 0)
                {
                    _rig.AddForce(new Vector2(11f * _axisxStore, 5.5f * _axisy), ForceMode2D.Impulse);
                }
                _rig.velocity = new Vector2(_rig.velocity.x, Mathf.Clamp(_rig.velocity.y, -9f, 9f));
                _hasDashed = true;
            }
            if(!ctx.canceled)
            {
                timerDash = 0.4f;
            }
            _dashAir = false;
        }
        else if ((_axisx != 0 || _axisy != 0) && !_hasDashed && !_grounded && _dashAir)
        {
            if (PlayerAttack.isStrongAttacking == false && !_isDead)
            {
                _dust0.Play();
                _audioSource.PlayOneShot(_dash);
                _trail.enabled = true;
                //_animator.Play("Dash");
                _rig.velocity = Vector2.zero;
                _rig.AddForce(new Vector2(8f * _axisx, 12f * _axisy), ForceMode2D.Impulse);
                _rig.velocity = new Vector2(_rig.velocity.x, Mathf.Clamp(_rig.velocity.y, -9f, 11f));
                _hasDashed = true;
            }
            if(!ctx.canceled)
            {
                timerDash = 0.4f;
            }
            _dashAir = false;
        }
    }
    public void HandleDashCancel(InputAction.CallbackContext ctx)
    {
        dashAnimated = false;
        _rig.velocity = new Vector2(0f,0f);
        _hasDashed = false;
    }
    public void HandleJumpDown(InputAction.CallbackContext ctx)
    {
        if (!_hasDashed && !_grounded && !_isDead && downable)
        {
            if (PlayerAttack.isStrongAttacking == false)
            {
                //_animator.Play("Jump down");
            }
            _rig.AddForce(new Vector2(0f, -7f), ForceMode2D.Impulse);
        }
        else if (DownTimer >= 0)
        {
            DownJumpTimer = DownTimer;
        }
    }
    public void HandleInteract(InputAction.CallbackContext ctx)
    {
        if(RespawnPoint.touchingInteract)
        {
            if (RespawnPoint.interactName == "RespawnTrigger")
            {
                //if(_grayAnim !=  null)
                //{
                //    _grayAnim.Play("white flash");
                //} 
                _particleSystem.Play();
                isRespInteracting = true;
                _respTimer = 0.35f;
                _audioSource.PlayOneShot(_sewer);
                PlayerHealth.health = 100f;
                RespawnPoint.hasCheckpoint = true;
                if (!ctx.canceled)
                {
                    _particleSystem.Play();
                }
            }
            
            if (RespawnPoint.interactName == "RespawnTrigger2")
            {
                //if (_grayAnim != null)
                //{
                //    _grayAnim.Play("white flash");
                //}
                _particleSystem.Play();
                isRespInteracting = true;
                _respTimer = 0.35f;
                _audioSource.PlayOneShot(_sewer);
                PlayerHealth.health = 100f;
                RespawnPoint.hasCheckpoint2 = true;
                if (!ctx.canceled)
                {
                    _particleSystem.Play();
                }
            }
            if (RespawnPoint.interactName == "RespawnText")
            {
                _audioSource.PlayOneShot(_paper);
                isInteracting = true;
                _interactSewer.SetActive(true);
                textMeshProUGUI.text = "Interact with sewers to set spawn and refill health";
            }
            if (RespawnPoint.interactName == "Jump")
            {
                _audioSource.PlayOneShot(_paper);
                _interactJump.SetActive(true);
                isInteracting = true;
            }
            if (RespawnPoint.interactName == "Attack1")
            {
                _audioSource.PlayOneShot(_paper);
                _interactAttack.SetActive(true);
                isInteracting = true;
            }
            if (RespawnPoint.interactName == "Attack3")
            {
                _audioSource.PlayOneShot(_paper);
                _interactAttack3.SetActive(true);
                isInteracting = true;
            }
            if (RespawnPoint.interactName == "Dash")
            {
                _audioSource.PlayOneShot(_paper);
                _interactDash.SetActive(true);
                isInteracting = true;
            }
        }
    }
}
