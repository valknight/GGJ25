using System;
using System.Collections;
using System.Collections.Generic;
using Windows.Ads;
using Market_Simulation;
using UnityEngine;
using UnityEngine.UI;

public class EMan : MonoBehaviour, ISimValueProvider
{
   public Sprite idleSprite;
   public Sprite eatingSprite;

   public Image manImage;

   public GameObject cookiePrefab;
   public Transform mouthAnchor;
   public float cookieSpeed;
   
   private int cookiesEaten;
   private int cookiesAvailable = 0;

   private List<GameObject> cookies = new();
   private void Start()
   {
      manImage.sprite = idleSprite;
      SimManager.RegisterProvider(this);
      StartCoroutine(TryGetCookie());
   }

   public IEnumerator TryGetCookie()
   {
      while (gameObject.activeSelf)
      {
         yield return new WaitForSeconds(1f);
         var adsWindow = FindAnyObjectByType<AdsWindow>();
         
         if (adsWindow == null) continue;
         if (adsWindow.cookiesAccepted <= 0) continue;
         
         adsWindow.cookiesAccepted--;
         cookiesAvailable++;
         cookies.Add(Instantiate(cookiePrefab, adsWindow.cookieSpawnAnchor.transform.position, Quaternion.identity, mouthAnchor));
      }
   }

   public void OnCookieHit()
   {
      cookiesEaten++;
      StopCoroutine(PlayEatingAnim());
      StartCoroutine(PlayEatingAnim());
   }

   private void Update()
   {
      var cookiesToDestroy = new List<GameObject>();
      
      foreach (var cookie in cookies)
      {
         cookie.transform.Translate((mouthAnchor.transform.position - cookie.transform.position).normalized * cookieSpeed * Time.deltaTime);
         if (Vector2.Distance(cookie.transform.position, mouthAnchor.position) < 0.01f)
         {
            cookiesToDestroy.Add(cookie);
            OnCookieHit();
         }
      }

      foreach (var cookie in cookiesToDestroy)
      {
         cookies.Remove(cookie);
         Destroy(cookie.gameObject);
      }
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
      return cookiesEaten/10f;
   }

   public string GetProviderName()
   {
      return "Eman";
   }

   private void OnDisable()
   {
      foreach (var cookie in cookies)
      {
         Destroy(cookie.gameObject);
      }
      
      cookies.Clear();
   }
}
