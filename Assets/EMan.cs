using System;
using System.Collections;
using Market_Simulation;
using UnityEngine;
using UnityEngine.UI;

public class EMan : MonoBehaviour, ISimValueProvider
{
   public Sprite idleSprite;
   public Sprite eatingSprite;

   public Image manImage;

   private int cookiesEaten;
   private void Start()
   {
      manImage.sprite = idleSprite;
      SimManager.RegisterProvider(this);
   }

   public void OnCookieHit()
   {
      cookiesEaten++;
      StopAllCoroutines();
      StartCoroutine(PlayEatingAnim());
   }
   
   private IEnumerator PlayEatingAnim()
   {
      manImage.sprite = eatingSprite;
      yield return new WaitForSeconds(0.3f);
      manImage.sprite = idleSprite;
   }

   private void OnDestroy()
   {
      SimManager.DeRegisterProvider(this);
   }

   public float GetValue()
   {
      return cookiesEaten;
   }

   public string GetProviderName()
   {
      return "Eman";
   }
}
