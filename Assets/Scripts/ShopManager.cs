using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
	public class Skin{
		public String name;
		public Sprite sprite;
		public bool isObtained;

		public Skin(Sprite sprite){
			name = sprite.name;
			this.sprite = sprite;
			isObtained = PlayerPrefs.GetInt(name) == 1 ? true:false;
		}

	}

	private bool isSkinSelected = false;

	public Button buyButton;
	public Ball player;
	private Sprite oldSprite;
	public Sprite playerDefaultSprite;
public GameObject mainScreen;
public MainMenu menuScreen;
public List<Sprite> sprites = new List<Sprite>();
	public List<Skin> skins;

	private int currentOption = 0;

	private static ShopManager instance;

    public static ShopManager GetInstance(){
        return instance;
    }

	public void NextOption(){
		SoundManager.PlayButtonClickSound();
		currentOption++;
		if (currentOption >= skins.Count) 
		{
			currentOption = 0;
		}
		player.GetSpriteRenderer().sprite = sprites[currentOption];
		SetTextOfBuyButton();
		if (!skins[currentOption].isObtained) 
		{
			
			player.GetSpriteRenderer().color = new Color(0,0,0);
		}
		else {
			player.GetSpriteRenderer().color = new Color(255,255,255);
		}
	}


	public void PrevOption(){
		SoundManager.PlayButtonClickSound();
		currentOption--;
		if (currentOption < 0) 
		{
			currentOption = skins.Count -1;
		}
		player.GetSpriteRenderer().sprite = sprites[currentOption];
		SetTextOfBuyButton();
		if (!skins[currentOption].isObtained) 
		{
			
			player.GetSpriteRenderer().color = new Color(0,0,0);
		}
		else {
			player.GetSpriteRenderer().color = new Color(255,255,255);
			
		}
	}

	private void SetTextOfBuyButton(){
		ColorBlock colors = transform.Find("Buy").GetComponent<Button>().colors;
		if (!skins[currentOption].isObtained) 
		{
			transform.Find("Buy").Find("Text").GetComponent<Text>().text = "500";
			colors.normalColor = new Color(0,0,0);
		}
		else {
			if (skins[currentOption].sprite.Equals(oldSprite)) 
			{
				transform.Find("Buy").Find("Text").GetComponent<Text>().text = "Selected";
				colors.normalColor = new Color(0,255,0);
			}else{
				transform.Find("Buy").Find("Text").GetComponent<Text>().text = "Select";
				colors.normalColor = new Color(0,0,0);

			}
			
		}
		transform.Find("Buy").GetComponent<Button>().colors = colors;
	}

	public void ReturnToMenu(){
		player.GetSpriteRenderer().sprite = oldSprite;
		player.GetSpriteRenderer().color = new Color(255,255,255);
			mainScreen.SetActive(true);
			transform.gameObject.SetActive(false);
	}

void Start(){
	//PlayerPrefs.SetInt("money",500);
	//Debug.Log("Statr");
	//Debug.Log(PlayerPrefs.GetInt("money").ToString());
	//menuScreen = MainMenu.GetInstance();
	player = Ball.GetInstance();
	oldSprite = player.GetSpriteRenderer().sprite;
	skins = new List<Skin>();
	foreach (Sprite sprite in sprites) 
	{
		skins.Add(new Skin(sprite));
	}
	skins[0].isObtained = true;
	PlayerPrefs.SetInt(skins[0].name,1);
	if (PlayerPrefs.GetString("lastSkin") == null) 
	{
		Debug.Log("ShopManager start lastskin is" + PlayerPrefs.GetString("lastSkin"));
		PlayerPrefs.SetString("lastSkin",skins[0].name);
	}
	SetTextOfBuyButton();
}

public Sprite GetLastSkin(){
	String name = PlayerPrefs.GetString("lastSkin");
	foreach (Skin skin in skins) 
	{
		if (skin.name.Equals(name)) 
		{
			return skin.sprite;
		}
	}
	return playerDefaultSprite;
	}


public void Buy(){
	SoundManager.PlayButtonClickSound();
	if(skins[currentOption].isObtained){
		oldSprite = sprites[currentOption];
		PlayerPrefs.SetString("lastSkin",skins[currentOption].name);
		transform.Find("Buy").Find("Text").GetComponent<Text>().text = "Selected";
		ColorBlock colors = transform.Find("Buy").GetComponent<Button>().colors;
		colors.normalColor = new Color(0,255,0);
		transform.Find("Buy").GetComponent<Button>().colors = colors;
	}else {
		int money = PlayerPrefs.GetInt("money");
		if (money >= 500) 
		{
			skins[currentOption].isObtained = true;
			PlayerPrefs.SetInt(skins[currentOption].name,1);
			player.GetSpriteRenderer().color = new Color(255,255,255);
			money -= 500;
			PlayerPrefs.SetInt("money",money);
			transform.Find("Buy").Find("Text").GetComponent<Text>().text = "Select";
			Debug.Log(menuScreen);
			menuScreen.SetMoneyText();
		}
	}
}

}
