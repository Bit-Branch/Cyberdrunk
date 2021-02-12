using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    public MainMenu mainMenu;
    public Text rewardText;

    private int rewardCount = 125;

    private string gameId = "3846473";

    string idVideo = "video";
    string idRewardedVideo = "rewardedVideo";

    private bool testMode = true;


    // Start is called before the first frame update
    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId,testMode);
        mainMenu = MainMenu.GetInstance();

        // while(Advertisement.IsReady(idRewardedVideo)){
        //     yield return null;
        // }
        // Advertisement.Show(idRewardedVideo);
    }

    public void ShowAd(String placementId){
        Advertisement.Show(placementId);
         DeathHandler.adShowed = true;
          mainMenu.SetMoneyText();
    }

    public void OnUnityAdsDidFinish(String placementId,ShowResult showResult){
        if(showResult == ShowResult.Finished && placementId.Equals(idRewardedVideo)){
            if(rewardText != null){
            Debug.Log("Finished ad ^ " + rewardText.text);
            int count =Int32.Parse(rewardText.text) + 125;
            rewardText.text = count.ToString();
            Debug.Log("rewardText ^ " + rewardText.text);
            }
            SaveManager.AddScoreAfterDeath(125);
            mainMenu.SetMoneyText();
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu"); 
        }else if(showResult == ShowResult.Failed){

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(DeathHandler.deathCount %5 == 0 & DeathHandler.deathCount > 0 && !DeathHandler.adShowed){
          ShowAd("video");
         
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        throw new NotImplementedException();
    }

    public void OnUnityAdsDidError(string message)
    {
        throw new NotImplementedException();
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        throw new NotImplementedException();
    }
}
