using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovingDots : MonoBehaviour {

  // NavMeshAgent navMeshAgent;
   public float timerForNewPath;

   public float speed;

   bool isCoroutine;

   private void Start() {
   //  navMeshAgent = GetComponent<NavMeshAgent>();  
   }

   private void Update() {
       if(!isCoroutine){
           StartCoroutine(DoSomething());
       }
   }

   Vector3 getNewRandomPosition(){
       float x = Random.Range(-Screen.width,Screen.width);
       float y = Random.Range(-Screen.height,Screen.height);

       Vector3 pos = new Vector3(x,y,0);
       return pos;
   }

   IEnumerator DoSomething(){
       isCoroutine = true;
        yield return new WaitForSeconds(timerForNewPath);

        GetNewPath();
   }

   void GetNewPath(){
     //  GetComponent<Rigidbody2D>().velocity = getNewRandomPosition() * speed;
     float step = speed * Time.deltaTime;
    transform.position = Vector2.MoveTowards(transform.position, getNewRandomPosition(), step);
       //navMeshAgent.SetDestination();
   }
}