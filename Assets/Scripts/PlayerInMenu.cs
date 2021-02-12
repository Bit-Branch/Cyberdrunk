using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInMenu : MonoBehaviour
{
    Vector3 spawnPosition;
    public Ball player;
    public float speed;

    private bool flyUp = true;

 
 void Start () {
    spawnPosition = new Vector3(0,0,0);
    InvokeRepeating("ChangePosition", 0, 3);
    player = Ball.GetInstance();
    player.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
   
 }

 private void Update() {
    if(player.isWaiting()){
        player.changePlayerStateToPlaying();
    }
 }
 
 void ChangePosition () {
 	if (!flyUp) 
 	{
 		SetUpDirection();
      flyUp = true;
 		
 	}
 	else {
 		SetDownDirection();
 		flyUp = false;
 	}
    
 }

 


    private void SetUpDirection(){
       SetTrailColors(player.gameObject.GetComponent<TrailRenderer>(),new Color(178f,0f,0f), new Color(229f, 227f, 40f));
    //   player.gameObject.GetComponent<Rigidbody2D>().velocity = (Vector2.up + Vector2.right) *  speed;
    }

     public void SetTrailColors(TrailRenderer renderer, Color start, Color end){
      renderer.startColor = start;
      renderer.endColor = end;
    }

    private void SetDownDirection(){
       SetTrailColors(player.gameObject.GetComponent<TrailRenderer>(),new Color(0f, 172f, 255f), new Color(44f, 229f, 40f));
       //  player.gameObject.GetComponent<Rigidbody2D>().velocity = (Vector2.down + Vector2.right) *  speed;
    }
}
