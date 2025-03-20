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
                _animator.SetBool("Follow up", false);
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
                //_animator.SetBool("Follow up", false);
                isLightAttacking = false;
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
        if (lightAttackWait == false)
        {
            isLightAttacking = true;
            //_animator.Play("Light Attack Follow");
            _animator.SetBool("Follow up", true);
            timerLightAttack = 0.15f;
            timerLightAttackWait = 0.6f;
            lightAttackWait = true;
        }
    }
    public void HandleHeavyAttack(InputAction.CallbackContext ctx)
    {
        if (PlayerHealth.health > 50)
        {
            PlayerHealth.health -= 18;
            PlayerHealth.health = Mathf.Clamp(PlayerHealth.health, 32, 100);
            isStrongAttacking = true;
            //_animator.Play("Heavy Attack");
            timerStrongAttack = 0.6f;
        }
    }
}
