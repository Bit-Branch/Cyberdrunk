using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{

    public GameObject trashCanOne;
     public GameObject trashCanTwo;
     public GameObject gopniks;
     public GameObject hatch;
   //  public GameObject stand1;
  //   public GameObject stand2;
  //   public GameObject   stand3;
   //  public GameObject   lamp;

    public Ball player;

    public static float blockMoveSpeed = 12f;
    public const float USUAL_SPEED = 12f;
    public const float MAX_SPEED = 12f * 3f;
    public float gapSize;
    public float pipeSpawnTimerMax;


    private const float WIDTH = 1.73f * 2f;
    private const float CAMERA_SIZE = 16f;
    private const float PIPE_SPAWN_Y_POSITION = +20f;
    private float lastBlockPosition = 100f;
    private State state;
    private float blockSpawnTimer;
    private int blocksPassed;
    List<Block> blocks;
    private static Level instance;

    public static Level GetInstance() {
        return instance;
    }

    private enum State {
        Waiting,
        Playing,
        Dead,
    }

    private enum Blocks{
        trashCanOne,
        trashCanTwo,
        gopniks,
        hatch,
        stand1,
        stand2,
        stand3,
        lamp
    }



    void Awake()
    {
        instance = this;
        blocks = new List<Block>();
        state = State.Waiting;
        player = Ball.GetInstance();
        blockMoveSpeed = 12f;
       }

    void Start()
    {
        Ball.GetInstance().OnDeath += OnDied;
        Ball.GetInstance().OnStartPlaying += OnStartPlaying;
        Ball.GetInstance().OnWait += OnWaiting;
        blockMoveSpeed = 12f;

    }
   


    void Update()
    {
        
        if (state == State.Playing && !DeathHandler.isInLoadingScreen) {
            
            HandleBlockMovement();
            HandleBlockSpawning();
        }
        
    }

   
    private void OnStartPlaying(object sender, System.EventArgs e) {
        state = State.Playing;
    }

    private void OnDied(object sender, System.EventArgs e) {
        state = State.Dead;
        SaveManager.SetNewHighscore(blocksPassed);
        Debug.Log("OnDied blocksPassed" + blocksPassed);
        SaveManager.AddScoreAfterDeath(blocksPassed);
        blocksPassed = 0;
    }

    private void OnWaiting(object sender, System.EventArgs e) {
        state = State.Waiting;
    }


private GameObject getByTypeName(Blocks state){
    switch(state){
        case Blocks.gopniks:{
            return gopniks;
        }
        case Blocks.hatch:{
            return hatch;
        }
        // case Blocks.lamp:{
        //     return lamp;
        // }
      /*  case Blocks.stand1:{
            return stand1;
        }
        case Blocks.stand2:{
            return stand2;
        }
        case Blocks.stand3:{
            return stand3;
        }*/
        case Blocks.trashCanOne:{
            return trashCanOne;
        }
        case Blocks.trashCanTwo:{
            return trashCanTwo;
        }
        default:{
            return trashCanOne;
        }
    }
}


    private void HandleBlockSpawning() {
        blockSpawnTimer -= Time.deltaTime;
        if (blockSpawnTimer < 0) {
            blockSpawnTimer += pipeSpawnTimerMax;
            float heightForSingle = CAMERA_SIZE * 2.1f;
            float heightForDouble = CAMERA_SIZE * 1.35f;

            Array values = Enum.GetValues(typeof(Blocks));
            System.Random random = new System.Random();
            bool forBottom = random.Next() > (Int32.MaxValue / 2);
            bool isDouble = random.Next() > (Int32.MaxValue / 2);
            
            Blocks randomState = (Blocks)values.GetValue(random.Next(values.Length));
                if (isDouble)
                {
                    CreateDoubleBlocks(getByTypeName(randomState), getByTypeName((Blocks)values.GetValue(random.Next(values.Length))),WIDTH, heightForDouble, gapSize, PIPE_SPAWN_Y_POSITION,false);
                }
                else{
                    CreateBlock(getByTypeName(randomState),WIDTH, heightForSingle, PIPE_SPAWN_Y_POSITION,forBottom,false);
                }
            
        }
    }

    void HandleBlockMovement()
    {
        for (int i = 0; i < blocks.Count; i++) 
        {
            Block block = blocks[i];
           // block.position += new Vector3(-1, 0, 0) * blockMoveSpeed * Time.deltaTime;
            block.Move();

            if (block.GetYPosition() < Ball.GetInstance().GetTransform().position.y && !block.IsPassed()) 
            {
                    block.Pass();
                    blocksPassed++;
            }
            if (block.GetYPosition() + Screen.height/6 < Ball.GetInstance().GetTransform().position.y) {  
                block.DestroySelf();
                blocks.Remove(block);     
                Debug.Log("destroyed " + block);
                i--;
            }
        }



            
    }

    private void CreateDoubleBlocks(GameObject topBlock, GameObject bottomBlock,float width, float height, float gapSize, float yPosition,bool isMoving)
    {
        // CreateBlock(bottomBlock,width, height, xPosition, true,false);
        // CreateBlock(topBlock,width, height, -1 * xPosition,false,false);
        GameObject blockTransform = Instantiate(topBlock);
        float pos = lastBlockPosition + yPosition;
        
        
        SpriteRenderer blockSpriteRenderer = blockTransform.transform.GetComponent<SpriteRenderer>();
        float offset = topBlock.transform.GetComponent<SpriteRenderer>().size.x;
        blockTransform.transform.position = new Vector3(+CAMERA_SIZE/3.5f + offset,pos);
        blockSpriteRenderer.size = new Vector2(width, height);
        blockSpriteRenderer.flipX = true;
     //   BoxCollider2D blockBoxCollider = blockTransform.transform.GetComponent<BoxCollider2D>();
      //  blockBoxCollider.size = new Vector2(width/2, height);
     //   blockBoxCollider.offset = new Vector3(0,0);


        Block block; 
        if (isMoving) 
        {
         //   block = new MovingBlock(blockTransform,forBottom);
            block = new Block(blockTransform,false);
        }
        else {
            block = new Block(blockTransform,false);
        }

        blocks.Add(block);

        blockTransform = Instantiate(bottomBlock);
        
        
        blockSpriteRenderer = blockTransform.transform.GetComponent<SpriteRenderer>();
        offset = topBlock.transform.GetComponent<SpriteRenderer>().size.x;
        blockTransform.transform.position = new Vector3(-CAMERA_SIZE/3.5f - offset,pos);
        blockSpriteRenderer.size = new Vector2(width, height);
        
       // blockBoxCollider = blockTransform.transform.GetComponent<BoxCollider2D>();
       // blockBoxCollider.size = new Vector2(width/2, height);
       // blockBoxCollider.offset = new Vector3(0,0);

        if (isMoving) 
        {
         //   block = new MovingBlock(blockTransform,forBottom);
            block = new Block(blockTransform,true);
        }
        else {
            block = new Block(blockTransform,true);
        }

        blocks.Add(block);

        lastBlockPosition+= 10f;
     //   lastBlockPosition = blockTransform.transform.position.x;
    }

    // private void CreateDoubleMovingBlocks(Transform topBlock, Transform bottomBlock, float gapY, float gapSize, float xPosition){
    //     CreateBlock(bottomBlock, gapY - gapSize * .5f, xPosition, true,true);
    //     CreateBlock(topBlock, CAMERA_SIZE * 2f - gapY - gapSize * .5f, -1 * xPosition,false,true);
    // }

    private void CreateBlock(GameObject initialBlock,float width,float height, float yPosition, bool forBottom,bool isMoving)
    {
    
        GameObject blockTransform = Instantiate(initialBlock);
        float blockXPosition;
       
        if (forBottom)
        {
            blockXPosition = -CAMERA_SIZE/2 + initialBlock.transform.GetComponent<SpriteRenderer>().size.x;
        }
        else
        {
            blockXPosition = +CAMERA_SIZE/2 - initialBlock.transform.GetComponent<SpriteRenderer>().size.x;
        }
        blockTransform.transform.position = new Vector3(blockXPosition, lastBlockPosition + yPosition);
        lastBlockPosition = blockTransform.transform.position.y;

        SpriteRenderer blockSpriteRenderer = blockTransform.transform.GetComponent<SpriteRenderer>();
        blockSpriteRenderer.size = new Vector2(width, height);
        if(!forBottom){
            blockSpriteRenderer.flipX = true;
        }
     //   BoxCollider2D blockBoxCollider = blockTransform.transform.GetComponent<BoxCollider2D>();
      //  blockBoxCollider.size = new Vector2(width, height);



        Block block; 
        if (isMoving) 
        {
         //   block = new MovingBlock(blockTransform,forBottom);
            block = new Block(blockTransform,forBottom);
        }
        else {
            block = new Block(blockTransform,forBottom);
        }

        blocks.Add(block);
    }

    public int GetBlocksPassed(){
        return blocksPassed;
    }

    private class Block 
    {
        private GameObject block;
        private bool isBottom;
        private bool isPassed = false;

        public Block(GameObject block, bool isBottom) {
            this.block = block;
            this.isBottom = isBottom;
        }

        public virtual void Move() {
            this.block.transform.position += new Vector3(0, -1, 0) * blockMoveSpeed * Time.deltaTime;
        }

        public bool IsBottom() {
            return isBottom;
        }

        public float GetXPosition(){
            return this.block.transform.position.x;
        }

        public float GetYPosition(){
            return this.block.transform.position.y;
        }

        public void DestroySelf() {
            Destroy(this.block);
        }

        public bool IsPassed(){
            return isPassed;
        }

        public void Pass(){
            this.isPassed = true;
        }

    }
    
    

    // private class MovingBlock : Block
    // {
    //     private Transform block;
    //     private bool isBottom;
    //     private float magnetude = 5f;
    //     private float offset = 0;
    //     private Vector3 startPosition;
    //     private float frequency = 2f;

    // public MovingBlock(Transform block, bool isBottom) :base(block,isBottom) {
    //       //  this.block = block;
    //       //  this.isBottom = isBottom;
    //         startPosition = this.block.position;
    //     }
    //   public override void Move(){
    //    // startPosition += new Vector3(-1, 0, 0) ;
    //   //  block.position += new Vector3(-1, 0, 0) * blockMoveSpeed * Time.deltaTime;
    //     this.block.position += (startPosition  + this.block.transform.up * Mathf.Sin(Time.time * frequency + offset) * magnetude);
    //   }
    // }

}
