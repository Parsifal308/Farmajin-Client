using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Farmanji.Auth;
using System;
using System.Text.RegularExpressions;

public class LoginCanvas : MonoBehaviour
{
    #region FIELDS

    [SerializeField] private Login _loginData;
    [Header("ERROR PANEL:")]
    [SerializeField] private RectTransform loginPanel;
    [SerializeField] private RectTransform submitButton;
    [Header("ERROR PANEL:")]
    [SerializeField] private RectTransform errorPanel;
    [SerializeField] private TextMeshProUGUI codeErrorValueText;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Button cancelButton;

    [Header("MESSAGES:")]
    [SerializeField] private LoginMessages msg;
    #endregion

    #region UNITY METHODS
    private void Start()
    {
        _loginData.OnLoginFailed += SetErrorMessage;
    }
    #endregion

    #region PRIVATE METHODS
    #endregion

    #region PUBLIC METHODS
    public void SetErrorMessage(long code)
    {
        foreach (Errors error in msg.errors)
        {
            if (error.code == code)
            {
                codeErrorValueText.text = error.type;
                messageText.text = error.msg;
            }
        }
    }
    #endregion
}
