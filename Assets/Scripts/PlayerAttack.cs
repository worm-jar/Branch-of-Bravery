using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public Animator _animator;
    public float timerStrongAttack;
    public float timerLightAttack;
    public static float timerLightAttackWait;
    public InputActionAsset _asset;
    public static bool isStrongAttacking;
    public static bool isLightAttacking;
    public static bool lightAttackWait = false;
    public AudioSource _audioSource;
    public Transform _slashSound;
    public AudioSource _slashAudioSource;
    public CameraShake Camera;

    public AudioClip _slash;
    public AudioClip _stab;
    public AudioClip _no;
    // Start is called before the first frame update
    void Start()
    {
        _slashSound = this.gameObject.transform.Find("SlashSound");
        _slashAudioSource = _slashSound.GetComponent<AudioSource>();
        _audioSource = GetComponent<AudioSource>();
        _animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetBool("IsLAttacking", isLightAttacking);
        _animator.SetBool("IsHAttacking", isStrongAttacking);
        if (timerStrongAttack > 0)
        {
            timerStrongAttack -= Time.deltaTime;
            if (timerStrongAttack <= 0)
            {
                _audioSource.volume = 0.86f;
                isStrongAttacking = false;
                timerStrongAttack = 0;
            }
        }
        if (timerLightAttack > 0)
        {
            timerLightAttack -= Time.deltaTime;
            if (timerLightAttack <= 0)
            {
                _audioSource.volume = 0.86f;
                isLightAttacking = false;
                timerLightAttack = 0;
            }
        }
        if (timerLightAttackWait > 0)
        {
            timerLightAttackWait -= Time.deltaTime;
            if (timerLightAttackWait <= 0)
            {
                lightAttackWait = false;
                timerLightAttackWait = 0;
            }
        }
    }
    private void OnEnable()
    {
        _asset.Enable();
        _asset.FindAction("Player/Light Attack").started += HandleLightAttack;
        _asset.FindAction("Player/Light Attack").performed += HandleLightAttack;

        _asset.FindAction("Player/Heavy Attack").started += HandleHeavyAttack;
        _asset.FindAction("Player/Heavy Attack").performed += HandleHeavyAttack;
    }
    private void OnDisable()
    {
        _asset.Disable();
        _asset.FindAction("Player/Light Attack").started -= HandleLightAttack;
        _asset.FindAction("Player/Light Attack").performed -= HandleLightAttack;

        _asset.FindAction("Player/Heavy Attack").started -= HandleHeavyAttack;
        _asset.FindAction("Player/Heavy Attack").performed -= HandleHeavyAttack;
    }
    public void HandleLightAttack(InputAction.CallbackContext ctx)
    {
        if (lightAttackWait == false && PlayerTakeDamage.timerIFrames == 0 && !Pause.paused)
        {
            PlayerMovement._axisxStore = PlayerMovement._axisx;
            isLightAttacking = true;
            _audioSource.volume = 0.45f;
            _audioSource.PlayOneShot(_stab);
            //_animator.Play("Light Attack Follow");
            //_animator.SetBool("Follow up", true);
            timerLightAttack = 0.45f;
            timerLightAttackWait = 0.52f;
            lightAttackWait = true;
        }
    }
    public void HandleHeavyAttack(InputAction.CallbackContext ctx)
    {
        if (PlayerHealth.health >= 50 && isStrongAttacking == false && !Pause.paused)
        {
            _audioSource.volume = 0.5f;
            _slashAudioSource.PlayDelayed(0.5f);
            PlayerHealth.health -= 20f;
            PlayerHealth.health = Mathf.Clamp(PlayerHealth.health, 32, 100);
            isStrongAttacking = true;
            //_animator.Play("Heavy Attack");
            timerStrongAttack = 0.85f;
        }
        else if (PlayerHealth.health < 50 && isStrongAttacking == false && !Pause.paused)
        {
            _audioSource.volume = 0.5f;
            _audioSource.PlayOneShot(_no);
            Camera.amount = 0.2f;
            StartCoroutine(Wait2());
        }
    }
    public IEnumerator Wait2()
    {
        yield return new WaitForSeconds(0.25f);
        Camera.amount = 0f;
        _audioSource.volume = 0.82f;
    }
}
