using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class Boundary{
    public float xMin,xMax,yMin,yMax;
}
public class Ball : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Transform transform;
    private float speed = 7.5f;
    
   // private TrailRenderer trailRenderer;
    private SpriteRenderer spriteRenderer;
    public Sprite defaultSkin;
    public GameObject explosion;
    private int flag = 1;

    private State state;

    public bool onPauseMenu = false;


    public event EventHandler OnDeath;
    public event EventHandler OnStartPlaying;
    public event EventHandler OnWait;
    private static Ball instance;

    public Quaternion calibratedQuaternion;

    public Boundary boundary;

    public float tilt;

    public static Ball GetInstance(){
        return instance;
    }

    private enum State {
        Waiting,
        Playing,
        Dead
    }

    private void Awake(){
        Debug.Log("jnpausemenu - " + onPauseMenu);
        Debug.Log("last skin is : " + SaveManager.GetLastSkin());
        instance = this;
        transform = GetComponent<Transform>();
     //   trailRenderer = GetComponent<TrailRenderer>();  
        spriteRenderer = GetComponent<SpriteRenderer>();
        Sprite skin = SaveManager.GetLastSkin();
     //   Debug.Log("Ball Awake lastskin is" + skin);
        if (skin == null) 
        {
            spriteRenderer.sprite = defaultSkin;
        }else {
            spriteRenderer.sprite = skin;
        }
        
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.bodyType = RigidbodyType2D.Static;
        state = State.Waiting;
    }


    void Start()
    {
        
    }

    public Transform GetTransform(){
        return transform;
    }

    public SpriteRenderer GetSpriteRenderer(){
        return spriteRenderer;
    }

    // public TrailRenderer GetTrailRenderer(){
    //     return trailRenderer;
    // }

    public void SetNewSprite(Sprite sprite){
        spriteRenderer.sprite = sprite;
    }

    public Rigidbody2D GetRigidBody2D(){
        return rigidBody;
    }

    public int GetFlag(){
        return flag;
    }

    public void ChangeFlag(){
        flag *= -1;
    }



    // public void SetTrailColors(Color start, Color end){
    //     trailRenderer.startColor = start;
    //    trailRenderer.endColor = end;
    // }


    void Update()
    {
        switch (state) {
        default:
        case State.Waiting:
            if ((Input.GetMouseButtonDown(0) ||
            Input.touchCount > 0) && !DeathHandler.isInLoadingScreen) {
                // Start playing
                state = State.Playing;
                rigidBody.bodyType = RigidbodyType2D.Dynamic;
                MovePlayer();
                if (OnStartPlaying != null) OnStartPlaying(this, EventArgs.Empty);
            }else{
                rigidBody.bodyType = RigidbodyType2D.Static;
            }
            break;
        case State.Playing:
            MovePlayer();
            break;
        case State.Dead:
            break;
        }


        

    }

    private void MovePlayer(){
        if(!onPauseMenu && !DeathHandler.isInLoadingScreen){
        if (Input.GetMouseButtonDown (0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            ChangeFlag();
            if (Level.blockMoveSpeed < Level.MAX_SPEED) 
            {
                StartCoroutine(speedTime());
            }
        
        }
        if (flag < 0)
            {
                rigidBody.velocity = (Vector2.left + (Vector2.left*0.5f) + Vector2.up) *  speed;

            }
            else
            {
              //  SetTrailColors(new Color(0f, 172f, 255f), new Color(44f, 229f, 40f));
                rigidBody.velocity = (Vector2.right + (Vector2.right*0.5f) + Vector2.up) *  speed;
            }
    //     Vector3 rawAcceleration = Input.acceleration;
    //     Vector3 acceleration = FixAcceleration(rawAcceleration);

    //    // rigidBody.rotation = Quaternion.Euler(0f,0f,(rigidBody.velocity.x * -tilt));

    //     rigidBody.velocity = (new Vector3(0.0f,acceleration.y,0.0f) + Vector3.up) * speed;
    //     rigidBody.position = new Vector3(Mathf.Clamp(rigidBody.position.x,boundary.xMin,boundary.xMax),Mathf.Clamp(rigidBody.position.y,boundary.yMin,boundary.yMax),0.0f);
        }
        
    }

    public void CalibrateAccelerometer(){
        Vector3 accSnapshot = Input.acceleration;
        Quaternion rotateQuaternion = 
        Quaternion.FromToRotation(new Vector3(0.0f,0.0f,-1f),accSnapshot);
        calibratedQuaternion = Quaternion.Inverse(rotateQuaternion);
    }

    public Vector3 FixAcceleration(Vector3 acceleration){
        Vector3 fixedAcc = calibratedQuaternion * acceleration;
        return fixedAcc;
    }

    IEnumerator speedTime ()
     {
        Level.blockMoveSpeed = Level.MAX_SPEED;
        yield return new WaitForSeconds(0.2f);
        revertSpeed();
     }

     void revertSpeed ()
     {
        Level.blockMoveSpeed = Level.USUAL_SPEED;
     }

    private IEnumerator SetUpDirection(){
             //   trailRenderer.startColor = new Color(178f,0f,0f);
              //  trailRenderer.endColor = new Color(229f, 227f, 40f);
                rigidBody.AddForce((Vector2.up + Vector2.right) * 1.5f,ForceMode2D.Impulse);
                yield return new WaitForSeconds (1);
    }

    private IEnumerator SetDownDirection(){
      //  spriteRenderer.sprite = blue;
           //     trailRenderer.startColor = new Color(0f, 172f, 255f);
           //     trailRenderer.endColor = new Color(44f, 229f, 40f);
                rigidBody.AddForce((Vector2.down + Vector2.right) *  1.5f,ForceMode2D.Impulse);
                yield return new WaitForSeconds (1);
    }

    public bool isWaiting(){
        return state == State.Waiting ? true : false;
    }

    public void PausePlayer(){
        onPauseMenu = true;
    }

    public void UnPausePlayer(){
        onPauseMenu = false;
    }

    public void changePlayerStateToWaiting(){
        state = State.Waiting;
    }

    public void changePlayerStateToPlaying(){
        state = State.Playing;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Instantiate(explosion,transform.position,Quaternion.identity);
        Level.blockMoveSpeed = Level.USUAL_SPEED;
        Destroy(this.gameObject);
        DeathHandler.deathCount++;
        DeathHandler.adShowed = false;
        DeathHandler.currentDeathCauser = col.name;
        Debug.Log("Ball + " + DeathHandler.deathCount);
        SoundManager.PlaySound(SoundManager.Sound.Lose);
        Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        if (OnDeath != null) 
        {
            OnDeath(this,EventArgs.Empty);
        }
        
    }
}
