using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveManager 
{


    public static int GetHighscore(){
    	return PlayerPrefs.GetInt("highscore");
    }

    public static bool SetNewHighscore(int score){
    	int curHighscore = GetHighscore();
    	if (score > curHighscore) 
    	{
    		PlayerPrefs.SetInt("highscore",score);
    		PlayerPrefs.Save();
    		return true;
    	}else {
    		return false;
    	}
    }

    public static void ResetHighscore() {
    	PlayerPrefs.SetInt("highscore",0);
    	PlayerPrefs.Save();
    }

    public static Sprite GetLastSkin(){
	String name = PlayerPrefs.GetString("lastSkin");
	Debug.Log("SaveManager GetLastSkin lastskin is" + name);
	if (name != null && !name.Equals("")) 
	{
		Sprite[] spri = Resources.LoadAll<Sprite>("Sprites/" + "Drunkers");
		foreach (Sprite spr in spri) 
		{
			if (spr.name.Equals(name)) 
			{
				return spr;
			}
		}
		return null;
	}
	else {
		return null;
	}
	
	
	/*foreach (Skin skin in skins) 
	{
		if (skin.name.Equals(name)) 
		{
			return skin.sprite;
		}
	}
	return playerDefaultSprite;*/
	}

	public static void AddScoreAfterDeath(int score) {
		Debug.Log("AddScoreAfterDeath + " + score);
		int money = PlayerPrefs.GetInt("money");
		PlayerPrefs.SetInt("money",money + score);
		Debug.Log("Added Score After Death + " + (money + score));
	}

	
}
