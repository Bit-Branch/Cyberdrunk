using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	public static GameObject money;
	private static MainMenu instance;

	public Toggle musicToggle;

    public Toggle soundToggle;

    public static MainMenu GetInstance(){
        return instance;
    }

    private void Awake(){
	//	ToggleMusic();
     //   ToggleSound();
    	SetMoneyText();
 		transform.Find("Shop").gameObject.SetActive(false);
         transform.Find("Credits").gameObject.SetActive(false);

         transform.Find("Credits").Find("Back").GetComponent<Button>().onClick.AddListener(() => {
             SoundManager.PlayButtonClickSound();
             Debug.Log("back to menus");       
               	transform.Find("Main").gameObject.SetActive(true);
			transform.Find("Credits").gameObject.SetActive(false);
			
            transform.Find("Money").gameObject.SetActive(true);

    });

 		transform.Find("Shop").Find("Back").GetComponent<Button>().onClick.AddListener(() => {       
               	transform.Find("Main").gameObject.SetActive(true);
			transform.Find("Shop").gameObject.SetActive(false);
			SoundManager.PlayButtonClickSound();

    });

        transform.Find("Main").Find("Play").GetComponent<Button>().onClick.AddListener(() => {         	
			UnityEngine.SceneManagement.SceneManager.LoadScene("SomeScene"); 
        	SoundManager.PlayButtonClickSound();});

        transform.Find("Main").Find("ShopButton").GetComponent<Button>().onClick.AddListener(() => { 
            transform.Find("Main").gameObject.SetActive(false);
			transform.Find("Shop").gameObject.SetActive(true);
			SoundManager.PlayButtonClickSound();

    });

    transform.Find("Main").Find("CreditsButton").GetComponent<Button>().onClick.AddListener(() => { 
            transform.Find("Main").gameObject.SetActive(false);
			transform.Find("Credits").gameObject.SetActive(true);
            transform.Find("Money").gameObject.SetActive(false);
			SoundManager.PlayButtonClickSound();

    });

      
}

private void Update() {
//	ToggleMusic();
   //     ToggleSound();
}

 public void ToggleSound ()
    {
        if(soundToggle.isOn)
        {
            SoundManager.soundEnabled = true;
        }
        else
        {
            SoundManager.soundEnabled = false;
        }
    }

    public void ToggleMusic ()
    {
        if(musicToggle.isOn)
        {
            SoundManager.musicEnabled = true;
        }
        else
        {
            SoundManager.musicEnabled = false;
        }
    }


public void SetMoneyText(){
         	
         	String curMoney = PlayerPrefs.GetInt("money").ToString();
             Debug.Log("getmoney " + curMoney);
         	if (curMoney.Equals("") || curMoney == null) 
         	{
         		transform.Find("Money").Find("Text").GetComponent<Text>().text = "00";
         	}else {
         		transform.Find("Money").Find("Text").GetComponent<Text>().text = curMoney;
         	}
        	
        }


}
