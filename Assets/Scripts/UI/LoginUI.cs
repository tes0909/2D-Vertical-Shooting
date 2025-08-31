using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginUI : UIBase
{
    [SerializeField] private Button loginButton;
    [SerializeField] private Button registerButton;
    [SerializeField] private TMP_InputField idField;
    [SerializeField] private TMP_InputField passwordField;

    public override void Init()
    {
        base.Init();
        loginButton.onClick.AddListener(OnLogin);
        registerButton.onClick.AddListener(OnSignUp);
        Debug.Log("LoginUI Init");
    }

    private void OnSignUp()
    {
        AuthenticationManager.Instance.SignUp(idField, passwordField);
    }

    private void OnLogin()
    {
        AuthenticationManager.Instance.Login(idField, passwordField);
    }
}
