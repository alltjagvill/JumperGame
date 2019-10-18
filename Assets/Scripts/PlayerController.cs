using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    int rightPressedBoolHash = Animator.StringToHash("rightPressed");
    int leftPressedBoolHash = Animator.StringToHash("leftPressed");
    int jumpBoolHash = Animator.StringToHash("jumping");
    int jetpackBoolHash = Animator.StringToHash("jetPack");

    private bool facingRight = true;
    private bool onPlattform = false;
    private bool haveJetPack = false;

    public bool isGrounded;
    public bool jumpEnabled = true;
    public bool runEnable = true;

    private float moveInput;
    private float tmpSpeed;
    private float ifInAirSpeed = 1;

    public float moveSpeed = 1.0f;
    public float jumpHeight = 2.0f;
    public float jumpSpeed;
    public float wallJumpHeight;
    public float wallJumpSpeed;
    public float jetpackUpSpeed;
    public float jetpackSideSpeed;
    public float checkRadius;
    public float movingPlatformDivider;
    public float playerRayCastDistance;
    public float leftCheckGroundPosDistance;
    public float rightCheckGroundPosDistance;
    public float normalGravity;
    public float jetpackGravity;

    public bool touchingLeftWall = false;
    public bool touchingRightWall = false;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public LayerMask whatIsWall;
    public Camera mainCamera;

    private CameraController mainCameraController;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;

    public delegate void Player();
    public static event Player OnDangerHit;
    enum Direction
    {
        left,
        right
    }


    


    

  
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        mainCameraController = mainCamera.GetComponent<CameraController>();
        runEnable = true;
    }

   

    private void Update()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        if (isGrounded)
        {
            animator.SetBool(jumpBoolHash, false);
        }

        if (isGrounded != true)
        {
            ifInAirSpeed = jumpSpeed;
        }
        else
        {
            ifInAirSpeed = 1;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jumped");

            if (isGrounded && !haveJetPack)
            {

                JumpUp();
            }

            if (!isGrounded && touchingLeftWall)
            {
                runEnable = false;
                transform.localScale = transform.localScale.x == 1 ? new Vector2(-1, 1) : Vector2.one;
                rigidBody.AddForce(new Vector2(wallJumpSpeed, wallJumpHeight), ForceMode2D.Force);
                animator.SetBool(jumpBoolHash, true);

            }

            if (touchingRightWall == true && !isGrounded)
            {
                runEnable = false;
                transform.localScale = transform.localScale.x == 1 ? new Vector2(-1, 1) : Vector2.one;
                rigidBody.AddForce(new Vector2(-wallJumpSpeed, wallJumpHeight), ForceMode2D.Force);
                animator.SetBool(jumpBoolHash, true);
            }
        }

        
    }
 

    private void FixedUpdate()
    {
        
        //    isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        //if (isGrounded)
        //{
        //    animator.SetBool(jumpBoolHash, false);
        //}

        if (!runEnable)
        {
            if (isGrounded)
                runEnable = true;
        }

#if (UNITY_EDITOR || UNITY_STANDALONE)



        if (Input.GetKey(KeyCode.A))
        {
            moveInput = -1;
            transform.localScale = new Vector2(-1, 1);

            if (runEnable == true && !haveJetPack)
                {
                rigidBody.velocity = new Vector2(moveInput * moveSpeed * ifInAirSpeed, rigidBody.velocity.y);
                animator.SetBool(leftPressedBoolHash, true);
            } 

            if (haveJetPack)
            {
                rigidBody.velocity = new Vector2(moveInput * jetpackSideSpeed * ifInAirSpeed, rigidBody.velocity.y);
            }
        }

        if (!Input.GetKey(KeyCode.A))
        {
            animator.SetBool(leftPressedBoolHash, false);
        }

        

        if (Input.GetKey(KeyCode.D))
        {
            moveInput = 1;
            transform.localScale = new Vector2(1, 1);

            if (runEnable == true && !haveJetPack)

            {
                
                rigidBody.velocity = new Vector2(moveInput * moveSpeed * ifInAirSpeed, rigidBody.velocity.y);           
                animator.SetBool(rightPressedBoolHash, true);
            }

            if (haveJetPack)
            {
                rigidBody.velocity = new Vector2(moveInput * jetpackSideSpeed * ifInAirSpeed, rigidBody.velocity.y);
            }
        }

        if (!Input.GetKey(KeyCode.D))
        {
            animator.SetBool(rightPressedBoolHash, false);
        }


        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, playerRayCastDistance, whatIsWall);

        //&& isGrounded == true && (!touchingLeftWall || !touchingRightWall)
        //if (Input.GetKeyDown(KeyCode.Space) )
        //{
        //    // Debug.Log("Jumped");
        //    if (isGrounded && !haveJetPack)
        //    {

        //        //JumpUp();
        //        animator.SetBool(jumpBoolHash, true);
        //    }

        //    if (!isGrounded && touchingLeftWall)
        //    {
        //        //runEnable = false;
        //        //transform.localScale = transform.localScale.x == 1 ? new Vector2(-1, 1) : Vector2.one;
        //        //rigidBody.AddForce(new Vector2(wallJumpSpeed, wallJumpHeight), ForceMode2D.Force);
        //        animator.SetBool(jumpBoolHash, true);

        //    }

        //    if (touchingRightWall == true && !isGrounded)
        //    {
        //        //runEnable = false;
        //        //transform.localScale = transform.localScale.x == 1 ? new Vector2(-1, 1) : Vector2.one;
        //        //rigidBody.AddForce(new Vector2(-wallJumpSpeed, wallJumpHeight), ForceMode2D.Force);
        //        animator.SetBool(jumpBoolHash, true);
        //    }


        //}

        if (Input.GetKey(KeyCode.Space) && haveJetPack)
        {
            rigidBody.velocity = Vector2.up * jetpackUpSpeed;
        }

        //hit.collider != null
        //if (Input.GetKeyDown(KeyCode.Space) && isGrounded == false && (touchingLeftWall || touchingRightWall) )
        //{
        //    //Walljumping
        //    runEnable = false;

        //    //rigidBody.velocity = new Vector2(wallJumpSpeed * hit.normal.x, wallJumpHeight);
           
        //        if (touchingLeftWall == true)
        //        {
        //            transform.localScale = transform.localScale.x == 1 ? new Vector2(-1, 1) : Vector2.one;
        //            rigidBody.AddForce(new Vector2(wallJumpSpeed, wallJumpHeight), ForceMode2D.Force);
        //        }

        //        if (touchingRightWall == true)
        //        {
        //            transform.localScale = transform.localScale.x == 1 ? new Vector2(-1, 1) : Vector2.one;
        //            rigidBody.AddForce(new Vector2(-wallJumpSpeed, wallJumpHeight), ForceMode2D.Force);
        //        }
            
        //    // WallJump();
        //}


        
        

#endif

    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("MovingPlatform"))
        {

            Vector3 delta = transform.position - col.transform.position;

            if (delta.y > 0.6f)
            {
                onPlattform = true;
                tmpSpeed = moveSpeed;
                MovingPlatformController movPlatCont = col.gameObject.GetComponent<MovingPlatformController>();
                moveSpeed = tmpSpeed / movingPlatformDivider;
                this.transform.parent = col.transform;
            }

        }

        if (col.gameObject.tag.Equals("LeftWall"))
        {
            touchingLeftWall = true;
            
        }

        if (col.gameObject.tag.Equals("RightWall"))
        {
            touchingRightWall = true;
        }
                     
    }
    private void JumpUp()
    {
        rigidBody.velocity = Vector2.up * jumpHeight;
        animator.SetBool(jumpBoolHash, true);
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("MovingPlatform") && onPlattform)
        {
            onPlattform = false;
            moveSpeed = tmpSpeed;

            this.transform.parent = null;
        }

        if (col.gameObject.tag.Equals("LeftWall"))
        {
            touchingLeftWall = false;
        }

        if (col.gameObject.tag.Equals("RightWall"))
        {
            touchingRightWall = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Danger"))
        {
            PlayerDied();
        }

    }

    public void JetpackAquired()
    {
        haveJetPack = true;
        rigidBody.gravityScale = jetpackGravity;
        animator.SetBool(jetpackBoolHash, true);

    }

    public void JetpackRemoved()
    {
        haveJetPack = false;
        rigidBody.gravityScale = normalGravity;
        animator.SetBool(jetpackBoolHash, false);
    }
   
    private void PlayerDied()
    {
        Debug.Log("Im hit!");
        Destroy(this);
        transform.Rotate(0, 0, 90);

        if (OnDangerHit != null)
        {
            OnDangerHit();
        }
    }
}
