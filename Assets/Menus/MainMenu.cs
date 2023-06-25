using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Transform _uiAbout;

    public void GetAbout()
    {
        _uiAbout.gameObject.SetActive(true);
    }

    public void LeaveAbout()
    {
        _uiAbout.gameObject.SetActive(false);
    }
}
