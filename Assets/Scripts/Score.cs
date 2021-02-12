using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{

	private Text score;
    // Start is called before the first frame update
    void Start()
    {
        score = transform.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
    	int currentScore = Level.GetInstance().GetBlocksPassed();
    	if (currentScore <= 9) 
    	{
    		score.text = "0" + currentScore.ToString();
    	}else {
    		score.text = currentScore.ToString();
    	}
        
    }
}
