using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSomeScene : MonoBehaviour
{

	public Ball player = Ball.GetInstance();
	private enum State {
        Waiting,
        Playing,
        Dead,
    }

    State state;

	bool isPaused = false;

	private void Start() {
		Ball.GetInstance().OnDeath += OnDied;
	}

    private void Awake(){
		
    	transform.Find("Pause").gameObject.SetActive(false);

    	transform.Find("PauseButton").GetComponent<Button>().onClick.AddListener(() => {
    	 transform.Find("Pause").gameObject.SetActive(true);
    	//  player.OnWait += OnWaiting; 
		//  player.PausePlayer();
		//  player.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    	
	  transform.Find("PauseButton").GetComponent<Button>().gameObject.SetActive(false);
	  player.PausePlayer();
    	 Time.timeScale = 0; 
		  });

    	 transform.Find("Pause").Find("Retry").GetComponent<Button>().onClick.AddListener(() => {
        	transform.Find("Pause").gameObject.SetActive(false);
			transform.Find("PauseButton").GetComponent<Button>().gameObject.SetActive(true);
        	player.UnPausePlayer(); 
		Time.timeScale = 1;
    	});

StartCoroutine(ShowLoadingScreen());
		
}

private void OnDied(object sender, System.EventArgs e) {
	transform.Find("PauseButton").GetComponent<Button>().gameObject.SetActive(false);
    }


private void OnWaiting(object sender, System.EventArgs e) {
    state = State.Waiting;
    }

	IEnumerator ShowLoadingScreen(){
		DeathHandler.isInLoadingScreen = true;
		transform.Find("Loading").gameObject.SetActive(true);
		yield return new WaitForSeconds(4);
		transform.Find("Loading").gameObject.SetActive(false);
		DeathHandler.isInLoadingScreen = false;
	}

}
