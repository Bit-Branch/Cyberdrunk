// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.Advertisements;
// //using UnityEngine.Monetization;
// using System;

// public class CustomAdvertisement : MonoBehaviour
// {

//     public Text rewardText;

//     private int rewardCount = 125;

//     private string gameId = "3846473";

//     string idVideo = "video";
//     string idRewardedVideo = "rewardedVideo";

//     private bool testMode = true;
//     // Start is called before the first frame update
//     void Start()
//     {
//         if(Application.platform == RuntimePlatform.IPhonePlayer){
//             gameId = "3846472";
//         }
//         else{
//             gameId = "3846473";
//         }

//       Advertisement.Initialize(gameId,testMode);
//     }
// public void ShowAd(){
//     StartCoroutine(WaitForAd());
// }

// public void ShowRewardedAd(){
//     StartCoroutine(WaitForAd(true));
// }

// IEnumerator WaitForAd(bool rewarded = false){
//     string placementId = rewarded ? idRewardedVideo : idVideo; 
//     while(Advertisement.IsReady(placementId)){
//         yield return null;
//     }
    

//     // ShowAdPlacementContent ad = null;
//     // ad = Advertisement.GetPlacementContent(placementId) as ShowAdPlacementContent;

//     // if(ad != null){
//         // if(rewarded){
//         // ad.Show(AdFinished);
//         // }else{
//         //     ad.Show();
//         // }
//          Advertisement.Show();
//         if(rewarded){
       
//         int count =Int32.Parse(rewardText.text) + 125;
//          rewardText.text = count.ToString();
//         }
//     }
// }

// // void AdFinished(ShowResult result){
// //     if(result == ShowResult.Finished){
// //         int count =Int32.Parse(rewardText.text) + 125;
// //         rewardText.text = count.ToString();
// //     }
// // }

