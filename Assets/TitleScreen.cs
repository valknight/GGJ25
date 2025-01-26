using System;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    private static readonly int Login = Animator.StringToHash("Login");

    private void Start()
    {
        SystemEventManager.Subscribe(SystemEventManager.SystemEventType.Login, OnLogin);
    }

    private void OnLogin(object obj)
    {
        GetComponent<Animator>().SetTrigger(Login);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            var button = FindFirstObjectByType<LoginButton>();
            if(button != null) button.TryLogin();
        }
    }

    private void OnDisable()
    {
        SystemEventManager.Unsubscribe(SystemEventManager.SystemEventType.Login, OnLogin);
    }
}
