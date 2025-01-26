using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginButton : MonoBehaviour
{
   public TMP_Text usernameField;

   private Button _button;

   private bool animating = false;

   private void OnEnable()
   {
      _button = GetComponent<Button>();
   }

   private void Update()
   {
       _button.interactable = !animating && usernameField.text.Length > 1;
   }

   public void OnClick()
   {
       animating = true;
       PlayerPrefs.SetString("Username", usernameField.text);
       SystemEventManager.RaiseEvent(SystemEventManager.SystemEventType.Login, null);
   }

   public void TryLogin()
   {
       if (_button.interactable)
       {
           OnClick();
       }
   }
}
