using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthenticationManager : MonoBehaviour
{
    private static AuthenticationManager _instance;
    [SerializeField] private Button loginButton;
    [SerializeField] private Button registerButton;
    [SerializeField] private TMP_InputField idField;
    [SerializeField] private TMP_InputField passwordField;

    public static AuthenticationManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AuthenticationManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("AuthenticationManager");
                    _instance = obj.AddComponent<AuthenticationManager>();
                }
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Init();
        loginButton.onClick.AddListener(Login);
        registerButton.onClick.AddListener(SignUp);
    }

    private async void Init()
    {
        await InitializeUnityServices();
    }

    private async Task InitializeUnityServices()
    {
        try
        {
            await UnityServices.InitializeAsync();
            Debug.Log("Unity Services initialized");
        }
        catch(Exception e)
        {
            Debug.LogException(e);
        }
    }

    private async void Login()
    {
        string id = idField.text.Trim();
        string password = passwordField.text.Trim();

        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(password))
        {
            Debug.Log("아이디와 비밀번호를 입력하세요");
            return;
        }

        try
        {
            await InitializeUnityServices();
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(id, password);
            SceneManager.LoadScene("StartScene");
        }
        catch (AuthenticationException ex)
        {
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
        }
    }

    private async void SignUp()
    {
        string id = idField.text.Trim();
        string password = passwordField.text.Trim();

        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(password))
        {
            Debug.Log("아이디와 비밀번호를 입력하세요");
            return;
        }
        
        try
        {
            await InitializeUnityServices();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            await AuthenticationService.Instance.AddUsernamePasswordAsync(id, password);
        }
        catch (AuthenticationException ex)
        {
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
        }
    }
}
