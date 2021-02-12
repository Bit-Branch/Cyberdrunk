using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{

    private void Awake(){

//    	transform.Find("Back").GetComponent<Button>().onClick.AddListener(() => { UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu"); /*Loader.Load(Loader.Scene.GameScene);*/ });
  //      transform.Find("retryBtn").GetComponent<Button_UI>().AddButtonSounds();
        
        transform.Find("Retry").GetComponent<Button>().onClick.AddListener(() => {
        	SoundManager.PlayButtonClickSound();
            Ball.GetInstance().UnPausePlayer(); 
        	gameObject.SetActive(false);
        	
    });

    transform.Find("Back").GetComponent<Button>().onClick.AddListener(() => {
        	SoundManager.PlayButtonClickSound();
            Ball.GetInstance().UnPausePlayer(); 
            gameObject.SetActive(false);
            Time.timeScale = 1;
            SaveManager.AddScoreAfterDeath(Level.GetInstance().GetBlocksPassed());
        	UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu"); 
            
    });
}

}
