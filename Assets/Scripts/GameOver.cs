using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private Text score;
    private Text deathMessage;



    private void Awake(){
    	score = transform.Find("Highscore").GetComponent<Text>();
        deathMessage = transform.Find("Title").GetComponent<Text>();

    	transform.Find("Back").GetComponent<Button>().onClick.AddListener(() => { 
    		UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    		SoundManager.PlayButtonClickSound(); /*Loader.Load(Loader.Scene.GameScene);*/ });
  //      transform.Find("retryBtn").GetComponent<Button_UI>().AddButtonSounds();

        
        transform.Find("Retry").GetComponent<Button>().onClick.AddListener(() => { /*Loader.Load(Loader.Scene.MainMenu);*/
        	UnityEngine.SceneManagement.SceneManager.LoadScene("SomeScene");
        	SoundManager.PlayButtonClickSound(); });
    //    transform.Find("mainMenuBtn").GetComponent<Button_UI>().AddButtonSounds();
    }

    private void Start() {
        Ball.GetInstance().OnDeath += OnDied;
        gameObject.SetActive(false);
    }

    private void Update() {
      /*  if (Input.GetKeyDown(KeyCode.Space)) {
            // Retry
            Loader.Load(Loader.Scene.GameScene);
        }*/
    }

    private void OnDied(object sender, System.EventArgs e) {
    	if (SaveManager.SetNewHighscore(Level.GetInstance().GetBlocksPassed())) 
    	{
    		//transform.Find("NewHighscore").gameObject.SetActive(true);
    	}else {
    		//transform.Find("NewHighscore").gameObject.SetActive(false);
    	}
		score.text = "Highscore: " + SaveManager.GetHighscore().ToString();
        deathMessage.text = GetCauserDeathText(DeathHandler.currentDeathCauser);
       /* if (Level.GetInstance().GetPipesPassedCount() >= Score.GetHighscore()) {
            // New Highscore!
            highscoreText.text = "NEW HIGHSCORE";
        } else {
            highscoreText.text = "HIGHSCORE: " + Score.GetHighscore();
        }*/
        

        gameObject.SetActive(true);
    }

    private string GetTrashCanDeathText(){
        return SetUpDeathText("Из князи в грязь","Кладоискатель","Бутылку в копилку");
    }

    private string GetGopniksDeathText(){
        return SetUpDeathText("Огонька не нашлось","Один в поле не воин");
    }

    private string GetHatchDeathText(){
        return SetUpDeathText("Привет Сплинтеру","Вниз по социальной лестнице","Бюджетный аквапарк");
    }

    private string GetLampDeathText(){
        return SetUpDeathText("Забыл заземлиться","Смотри куда прёшь","Рукопожатие Зевса");
    }

    private string GetShopDeathText(){
        return SetUpDeathText("Время заправиться","Вот это скидки!","Пиво само себя не купит");
    }

    private string SetUpDeathText(params string[] deathMessages){
        Debug.Log(deathMessages.Length);
        return (string)deathMessages[(new System.Random().Next(0,deathMessages.Length-1))];
    }

    private string GetCauserDeathText(string nameOfCauser){
        if(nameOfCauser.Equals("trashCanOne(Clone)") || nameOfCauser.Equals("trashCanTwo(Clone)")){
            return GetTrashCanDeathText();
        }
        if(nameOfCauser.Equals("gopniks(Clone)")){
            return GetGopniksDeathText();
        }
        if(nameOfCauser.Equals("hatch(Clone)")){
            return GetHatchDeathText();
        }
        if(nameOfCauser.Equals("Stand1(Clone)") || nameOfCauser.Equals("Stand2(Clone)") || nameOfCauser.Equals("Stand3(Clone)")){
            return GetShopDeathText();
        }
        if(nameOfCauser.Equals("Lamp(Clone)")){
            return GetLampDeathText();
        }
        return "";
    }
    
}
