using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
	private Transform transform;

public void Awake(){
	transform = GetComponent<Transform>();
}

public void SetTransformPosition(float x,float y){
	transform.position= new Vector3(x,y);
}
	public void PlayDeathAnim(){
		Destroy(gameObject);
	}

}