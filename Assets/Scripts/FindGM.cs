using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FindGM : MonoBehaviour
{
    public GameObject _gm;
    public Reset reset;
    public Button button;
    // Start is called before the first frame update
    void Awake()
    {
        button = GetComponent<Button>();
        _gm = GameObject.Find("GameManager");
        reset = _gm.GetComponent<Reset>();
    }
    //public void OnEnable()
    //{
    //    button.onClick.AddListener(StartGame0);
    //}
    //public void OnDisable()
    //{
    //    button.onClick.RemoveListener(StartGame0);
    //}
    public void Reset0()
    {
        reset.ResetNow();
    }
    public void StartGame0()
    {
        reset.StartGame();
    }
    public void StartCutsene0()
    {
        reset.StartCutscene();
    }
    public void Credits0()
    {
        reset.Credits();
    }
    public void Quit0()
    {
        reset.QuitApp();
    }
}
