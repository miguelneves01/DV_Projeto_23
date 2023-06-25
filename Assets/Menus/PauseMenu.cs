using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Transform _uiBlocker;
    [SerializeField] private Transform _uiMenu;
    [SerializeField] private Transform _uiInstructions;

    private bool _isActive;
    // Start is called before the first frame update
    void Start()
    {
        _isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            _isActive = !_isActive;
            _uiMenu.gameObject.SetActive(_isActive);
            _uiBlocker.gameObject.SetActive(_isActive);

            if (!_isActive)
            {
                LeaveInstructions();
            }
        }
    }

    public void GetInstructions()
    {
        _uiInstructions.gameObject.SetActive(true);
    }

    public void LeaveInstructions()
    {
        _uiInstructions.gameObject.SetActive(false);
    }

    public void Resume()
    {
        _isActive = false;
        _uiMenu.gameObject.SetActive(_isActive);
        _uiBlocker.gameObject.SetActive(_isActive);
        LeaveInstructions();
    }
}
