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

    
    private bool onPlattform = false;
    public bool haveJetPack = false;

    public bool isGrounded;
    public bool jumpEnabled = true;
    public bool runEnable = true;

    public bool touchJump = false;
    public bool touchWalkLeft = false;
    public bool touchWalkRight = false;
    public bool touchJetpack = false;

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
    public GameManager gameManager;

    private CameraController mainCameraController;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;

    public delegate void Player();
    public static event Player OnDangerHit;
    public static event Player OnExitHit;
   
  
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

        if(touchWalkLeft)
        {
            TouchWalk(-1);
        }
        if (touchWalkRight)
        {
            TouchWalk(1);
        }

        if (touchJetpack)
        {
            TouchJetpack();
        }

#if (UNITY_STANDALONE)
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

#endif

    }
 

    private void FixedUpdate()
    {
        
       

        if (!runEnable)
        {
            if (isGrounded)
                runEnable = true;
        }

#if (UNITY_STANDALONE)



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


       

        if (Input.GetKey(KeyCode.Space) && haveJetPack)
        {
            rigidBody.velocity = Vector2.up * jetpackUpSpeed;
        }
      
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

        if (col.gameObject.tag.Equals("Exit"))
        {
            LevelComplete();
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
   
    private void LevelComplete()
    {
        
        if (OnExitHit != null)
        {
            OnExitHit();
        }
    }
    private void PlayerDied()
    {
        
        Destroy(this);
        transform.Rotate(0, 0, 90);

        if (OnDangerHit != null)
        {
            OnDangerHit();
        }
    }

    public void TouchJumping()
    {
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



    public void TouchWalk(float moveInput)
    {
        //moveInput = -1;

        if (moveInput == -1)
        {
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

        if (moveInput ==  1)

        {
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
    }

    private void TouchJetpack()
    {
        if (haveJetPack)
        {
            rigidBody.velocity = Vector2.up * jetpackUpSpeed;
        }
    }

    public void TouchJumpSetTrue()
    {
        //touchJump = true;
        Debug.Log("TOUCH JUMP");
        animator.SetBool(jumpBoolHash, true);
    }
    //public void TouchJumpSetFalse()
    //{
    //    touchJump = false;
    //}

    public void TouchWalkLeftSetTrue()
    {
        touchWalkLeft = true;
    }
    public void TouchWalkLeftSetFalse()
    {
        touchWalkLeft = false;
        animator.SetBool(leftPressedBoolHash, false);
    }

    public void TouchWalkRightSetTrue()
    {
        touchWalkRight = true;
    }
    public void TouchWalkRighttSetFalse()
    {
        touchWalkRight = false;
        animator.SetBool(rightPressedBoolHash, false);
    }

    public void TouchJetpackSetTrue()
    {
        touchJetpack = true;
    }

    public void TouchJetpackSetFalse()
    {
        touchJetpack = false;
    }
}
