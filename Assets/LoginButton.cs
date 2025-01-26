using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginButton : MonoBehaviour
{
   public TMP_Text usernameField;

   private Button _button;

   private void OnEnable()
   {
      _button = GetComponent<Button>();
   }

   private void Update()
   {
       _button.interactable = usernameField.text.Length > 1;
   }

   public void OnClick()
   {
       PlayerPrefs.SetString("Username", usernameField.text);
       SystemEventManager.RaiseEvent(SystemEventManager.SystemEventType.Login, null);
   }
}
