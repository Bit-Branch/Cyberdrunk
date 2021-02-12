using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   //  public Transform player;
   // // public float cameraDistance = 30f;
   //  // Start is called before the first frame update
   //  void Start()
   //  {
   //   //   GetComponent<Camera>().orthographicSize = (Screen.height / 2) / cameraDistance;
   //  }

   //  // Update is called once per frame
   //  void Update()
   //  {
   //      transform.position = new Vector3(
   //          transform.position.x,
   //          Mathf.Clamp(player.position.y, (-1* Screen.height)/4, (Screen.height/4)),
   //          transform.position.z);
   //   //   transform.position = new Vector3(player.position.x + 10, player.position.y, transform.position.z);
   //  }
    public Camera MainCamera;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;


    // Use this for initialization
    void Start () {
        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
       // objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2
        MainCamera.gameObject.GetComponent<AudioSource>().clip = SoundManager.GetRandomBackgroundMusic();
        MainCamera.gameObject.GetComponent<AudioSource>().loop = true;
        MainCamera.gameObject.GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void LateUpdate(){
        Vector3 viewPos = transform.position;

        MainCamera.transform.position = new Vector3(0, transform.position.y + screenBounds.y/3,
          MainCamera.transform.position.z);
          viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x *-1 + objectHeight, screenBounds.x - objectHeight);
       // viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
        transform.position = viewPos;
    }
}
