using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour
{
    [SerializeField] private GameObject _success;
    [SerializeField] private GameObject _wilt;
    [SerializeField] private GameObject _overwater;

    [Range(0,5)]
    [SerializeField] private float _inactiveTime;
    
    void Awake()
    {
        SetInactive();
    }

    public void ActivateSuccessText()
    {
        _success.SetActive(true);
        Invoke("SetInactive", _inactiveTime);
    }

    public void ActivateWiltText()
    {
        _wilt.SetActive(true);
        Invoke("SetInactive", _inactiveTime);
    }

    public void ActivateOverwaterText()
    {
        _overwater.SetActive(true);
        Invoke("SetInactive", _inactiveTime);
    }

    private void SetInactive()
    {
        _success.SetActive(false);
        _wilt.SetActive(false);
        _overwater.SetActive(false);
    }

}
