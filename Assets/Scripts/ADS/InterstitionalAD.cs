using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YandexMobileAds;
using YandexMobileAds.Base;
using UnityEngine.SceneManagement;

public class InterstitionalAD : MonoBehaviour
{
    //private Interstitial interstitial;

    //private void Start()
    //{
    //    string adUnitId = "R-M-*****-*";
    //    interstitial = new Interstitial(adUnitId);

    //    AdRequest request = new AdRequest.Builder().Build();
    //    interstitial.LoadAd(request);
    //}

    //public void ShowOnDisplay()
    //{
    //    if(interstitial == null)
    //        RequestInterstitial();

    //    ShowInterstitial();
    //}

    //private void RequestInterstitial()
    //{
    //    string adUnitId = "R-M-1835433-1";
    //    interstitial = new Interstitial(adUnitId);

    //    AdRequest request = new AdRequest.Builder().Build();
    //    interstitial.LoadAd(request);

    //    interstitial.OnInterstitialFailedToLoad += HandleInterstitialFailedToLoad;

    //    interstitial.OnReturnedToApplication += Completed;
    //    interstitial.OnLeftApplication += Completed;
    //    interstitial.OnInterstitialShown += Completed;
    //    interstitial.OnInterstitialDismissed += Completed;
    //    interstitial.OnImpression += Completed;
    //}

    //private void ShowInterstitial()
    //{
    //    if (this.interstitial.IsLoaded())
    //    {
    //        PlayerPrefs.SetInt("DiedCount", 0);
    //        interstitial.Show(); 
    //    }
    //    else
    //    { 
    //        SceneManager.LoadScene("GameScene");
    //    }
    //}
    //public void HandleInterstitialFailedToLoad(object sender, AdFailureEventArgs args)
    //{
    //    SceneManager.LoadScene("GameScene");
    //}
    //public void Completed(object sender, System.EventArgs args)
    //{
    //    SceneManager.LoadScene("GameScene");
    //}
}
