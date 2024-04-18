using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using UnityEngine;

public class AdsManager : MonoBehaviour
{

   public static AdsManager instance;
#if UNITY_ANDROID
  private string _adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
  private string _adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
  private string _adUnitId = "unused";
#endif

  BannerView _bannerView;


  // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
  private string _adUnitIdInterstitialAd = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
  private string _adUnitIdInterstitialAd = "ca-app-pub-3940256099942544/4411468910";
#else
  private string _adUnitIdInterstitialAd = "unused";
#endif

  private InterstitialAd interstitialAd;
public void Start()
    {

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
        });
        CreateBanner();
        LoadInterstitialAd();
        //LoadBannerAd();
    }
public void Awake(){
        if(instance==null){
            instance=this;
        }
        DontDestroyOnLoad(this);
    }
public void CreateBanner()
  {
      Debug.Log("Creating banner view");

      // If we already have a banner, destroy the old one.
      if (_bannerView != null)
      {
          //DestroyAd();
      }

      // Create a 320x50 banner at top of the screen
      _bannerView = new BannerView(_adUnitId, AdSize.Banner, AdPosition.Top);
  }
public void LoadBannerAd()
{
    // create an instance of a banner view first.
    if(_bannerView == null)
    {
        CreateBanner();
    }
    // create our request used to load the ad.
    var adRequest = new AdRequest();
    adRequest.Keywords.Add("unity-admob-sample");

    // send the request to load the ad.
    Debug.Log("Loading banner ad.");
    _bannerView.LoadAd(adRequest);
}
public void LoadInterstitialAd()
  {
      // Clean up the old ad before loading a new one.
      if (interstitialAd != null)
      {
            interstitialAd.Destroy();
            interstitialAd = null;
      }

      Debug.Log("Loading the interstitial ad.");    

      // create our request used to load the ad.
      var adRequest = new AdRequest();
      adRequest.Keywords.Add("unity-admob-sample");

      // send the request to load the ad.
      InterstitialAd.Load(_adUnitIdInterstitialAd, adRequest,
          (InterstitialAd ad, LoadAdError error) =>
          {
              // if error is not null, the load request failed.
              if (error != null || ad == null)
              {
                  Debug.LogError("interstitial ad failed to load an ad " +
                                 "with error : " + error);
                  return;
              }

              Debug.Log("Interstitial ad loaded with response : "
                        + ad.GetResponseInfo());

              interstitialAd = ad;
          });
  }

  /// <summary>
/// Shows the interstitial ad.
/// </summary>
public void ShowinterstitialAd()
{
    if (interstitialAd != null && interstitialAd.CanShowAd())
    {
        Debug.Log("Showing interstitial ad.");
        interstitialAd.Show();
    }
    else
    {
        Debug.LogError("Interstitial ad is not ready yet.");
    }
}
/// <summary>
/// Destroys the ad.
/// </summary>
public void DestroyAd()
{
    if (_bannerView != null)
    {
        Debug.Log("Destroying banner ad.");
        _bannerView.Destroy();
        _bannerView = null;
    }
}
}
