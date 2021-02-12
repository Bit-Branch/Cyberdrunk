using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayerFollow : MonoBehaviour
{
    public Camera MainCamera;
  //  public Canvas canvas;
    private Vector2 screenBounds;

    void Start () {
        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
        
    }

    void LateUpdate(){
     //   canvas.position = new Vector3(player.position.x, player.position.y, canvas.position.z);
        MainCamera.transform.position = new Vector3(transform.position.x, transform.position.y,
        MainCamera.transform.position.z);

    }
}
