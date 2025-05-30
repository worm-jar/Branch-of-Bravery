using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    public InputActionAsset _asset;
    public static bool paused;
    public GameObject _player;
    public PlayerMovement PlayerMovement;
    public bool once;
    public AudioClip _paper;
    public AudioSource _audioSource;
    // Start is called before the first frame update
    public void Start()
    {
        _player = GameObject.Find("Player");
        PlayerMovement = _player.GetComponent<PlayerMovement>();
        _audioSource = _player.GetComponent<AudioSource>();
        this.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        _asset.Enable();
        _asset.FindAction("Player/Pause").started += HandlePause;
        _asset.FindAction("Player/Pause").performed += HandlePause;
        _asset.FindAction("Player/Pause").canceled += HandlePause;
    }
    public void HandlePause(InputAction.CallbackContext ctx)
    {
        //if (ctx.canceled)
        //{
        //    once = true;
        //}
        if (ctx.started)
        {
            if (PlayerMovement._interactAttack2.activeInHierarchy == true)
            {
                PlayerMovement._interactAttack2.SetActive(false);
                PlayerMovement.isInteracting = false;
            }
            else if (PlayerMovement._interactAttack4.activeInHierarchy == true)
            {
                PlayerMovement._interactAttack4.SetActive(false);
                PlayerMovement.isInteracting = false;
            }
            else if (PlayerMovement.isInteracting == true && PlayerMovement._interactAttack.activeInHierarchy == true && PlayerMovement._interactAttack2.activeInHierarchy == false)
            {
                Debug.Log("this");
                _audioSource.PlayOneShot(_paper);
                PlayerMovement._interactAttack2.SetActive(true);
                PlayerMovement._interactAttack.SetActive(false);
            }
            else if (PlayerMovement.isInteracting == true && PlayerMovement._interactAttack3.activeInHierarchy == true && PlayerMovement._interactAttack4.activeInHierarchy == false)
            {
                _audioSource.PlayOneShot(_paper);
                PlayerMovement._interactAttack4.SetActive(true);
                PlayerMovement._interactAttack3.SetActive(false);
            }
            else if (PlayerMovement.isInteracting == true && (PlayerMovement._interactAttack.activeInHierarchy == false && PlayerMovement._interactAttack3.activeInHierarchy == false))
            {
                PlayerMovement._interactSewer.SetActive(false);
                PlayerMovement._interactJump.SetActive(false);
                PlayerMovement._interactDash.SetActive(false);
                PlayerMovement.textMeshProUGUI.text = "";
                PlayerMovement.isInteracting = false;
            }
            else if (this.gameObject.activeInHierarchy == true && PlayerMovement.isInteracting == false)
            {
                Time.timeScale = 1f;
                paused = false;
                this.gameObject.SetActive(false);
            }
            else if (this.gameObject.activeInHierarchy == false && PlayerMovement.isInteracting == false)
            {
                Time.timeScale = 0f;
                paused = true;
                this.gameObject.SetActive(true);
            }
        }
    }
}
