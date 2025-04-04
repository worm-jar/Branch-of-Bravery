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
    // Start is called before the first frame update
    void Start()
    {
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
                timerStrongAttack = 0;
                isStrongAttacking = false;
            }
        }
        if (timerLightAttack > 0)
        {
            timerLightAttack -= Time.deltaTime;
            if (timerLightAttack <= 0)
            {
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
        if (lightAttackWait == false && PlayerTakeDamage.timerIFrames == 0)
        {
            isLightAttacking = true;
            //_animator.Play("Light Attack Follow");
            //_animator.SetBool("Follow up", true);
            timerLightAttack = 0.4f;
            timerLightAttackWait = 0.65f;
            lightAttackWait = true;
        }
    }
    public void HandleHeavyAttack(InputAction.CallbackContext ctx)
    {
        if (PlayerHealth.health > 50 && isStrongAttacking == false)
        {
            PlayerHealth.health -= 20f;
            PlayerHealth.health = Mathf.Clamp(PlayerHealth.health, 32, 100);
            isStrongAttacking = true;
            //_animator.Play("Heavy Attack");
            timerStrongAttack = 0.85f;
        }
    }
}
