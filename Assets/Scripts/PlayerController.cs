using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    int rightPressedBoolHash = Animator.StringToHash("rightPressed");
    int leftPressedBoolHash = Animator.StringToHash("leftPressed");

    private bool facingRight = true;
    public float moveSpeed = 1.0f;
    public float jumpHeight = 2.0f;
    public float jumpSpeed;
    public float wallJumpHeight;
    public float wallJumpSpeed;
    float tmpSpeed;
    public float movingPlatformDivider;
    float ifInAirSpeed = 1;
    private bool isGrounded;
    public float playerRayCastDistance;

    private bool touchingLeftWall;
    private bool touchingRightWall;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public LayerMask whatIsWall;


    private float moveInput;
    public bool jumpEnabled = true;
    public bool runEnable = true;
    public float leftCheckGroundPosDistance;
    public float rightCheckGroundPosDistance;  

    enum Direction
    {
        left,
        right
    }


    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        runEnable = true;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals ("MovingPlatform"))
        {
            tmpSpeed = moveSpeed;
            MovingPlatformController movPlatCont = col.gameObject.GetComponent<MovingPlatformController>();
            moveSpeed = tmpSpeed / movingPlatformDivider;
            this.transform.parent = col.transform;
            
            
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("MovingPlatform"))
        {
            moveSpeed = tmpSpeed;
            
            this.transform.parent = null;
        }
    }

    private void Update()
    {
        if (isGrounded != true)
        {
            ifInAirSpeed = jumpSpeed;
        }
        else
        {
            ifInAirSpeed = 1;
        }
    }
 

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        
#if (UNITY_EDITOR)

        if (!runEnable)
        {
            if (isGrounded)
                runEnable = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveInput = -1;
          
                if (runEnable == true)
                {
                rigidBody.velocity = new Vector2(moveInput * moveSpeed * ifInAirSpeed, rigidBody.velocity.y);
                animator.SetBool(leftPressedBoolHash, true);
            } 
        }

        if (!Input.GetKey(KeyCode.A))
        {
            animator.SetBool(leftPressedBoolHash, false);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.localScale = new Vector2(-1, 1);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.localScale = new Vector2(1, 1);
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveInput = 1;
         
            if (runEnable == true)

            {
                
                rigidBody.velocity = new Vector2(moveInput * moveSpeed * ifInAirSpeed, rigidBody.velocity.y);           
                animator.SetBool(rightPressedBoolHash, true);
            }        
        }

        if (!Input.GetKey(KeyCode.D))
            {
                animator.SetBool(rightPressedBoolHash, false);
            }


        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, playerRayCastDistance, whatIsWall);
        Debug.Log(hit);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
           JumpUp();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && hit.collider != null)
        {
            runEnable = false;
            rigidBody.velocity = new Vector2(wallJumpSpeed * hit.normal.x, wallJumpHeight);
            transform.localScale = transform.localScale.x == 1 ? new Vector2(-1, 1) : Vector2.one;
            // WallJump();
        }


        
        //if (Input.GetKeyDown(KeyCode.Space) && touchingLeftWall == true && isGrounded != true)
        //{
        //    JumpLeftWall();
        //}

        //if (Input.GetKeyDown(KeyCode.Space) && touchingRightWall == true && isGrounded != true)
        //{
        //    JumpRightWall();
        //}

#endif

    }

    void WallJump()
    {
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
    }





    private void JumpUp()
    {
        rigidBody.velocity = Vector2.up * jumpHeight;
    }

    private void JumpLeftWall()
    {
        runEnable = false;
        //rigidBody.AddForce(Vector2.up * wallJumpHeight, ForceMode2D.Impulse);
        //rigidBody.AddForce(Vector2.right * wallJumpSpeed, ForceMode2D.Impulse);
        //rigidBody.velocity = Vector2.up * wallJumpHeight;
        //rigidBody.velocity = Vector2.right * wallJumpSpeed;
        rigidBody.velocity = new Vector2(wallJumpSpeed, wallJumpHeight);
    }

    private void JumpRightWall()
    {
        runEnable = false;
        rigidBody.velocity = Vector2.up * wallJumpHeight;
       // rigidBody.velocity = Vector2.left * wallJumpSpeed;
    }
























    //private void Jump()
    //{


    //    if (IsPlayerOnGround())
    //    {
    //        runEnable = true;

    //        rigidBody.AddForce(Vector2.up * jumpHeight, ForceMode2D.Force);

    //    } 
    //    if(IsPlayerOnWall(Direction.left))
    //    {
    //        WallJump(Direction.left);

    //    } else if ( IsPlayerOnWall(Direction.right))
    //    {
    //        WallJump(Direction.right);
    //    }

    //}

    //private void WallJump(Direction direction)

    //{
    //    if (direction == Direction.left)
    //    {

    //        runEnable = false;
    //        rigidBody.AddForce(Vector2.up * jumpHeightSide, ForceMode2D.Impulse);
    //        rigidBody.AddForce(Vector2.right * jumpSpeedSide, ForceMode2D.Impulse);
    //        //rigidBody.AddForce(transform.forward * jumpHeight);
    //        // rigidBody.AddForce(new Vector2(jumpSpeedSide , jumpHeightSide), ForceMode2D.Impulse);

    //    }

    //    if (direction == Direction.right)
    //    {

    //        runEnable = false;

    //        rigidBody.AddForce(Vector2.up * jumpHeightSide, ForceMode2D.Impulse);
    //        rigidBody.AddForce(Vector2.left * jumpSpeedSide, ForceMode2D.Impulse);
    //        //rigidBody.AddForce(new Vector2(- jumpSpeedSide, jumpHeightSide), ForceMode2D.Impulse);
    //        //  rigidBody.AddForce(new Vector2(jumpHeight, jumpHeight));
    //        //  rigidBody.AddForce(Vector3.left * jumpHeight);

    //    }
    //}



    //private bool IsPlayerOnGround()
    //{
    //    RaycastHit2D checkGroundMiddle = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);


    //    if (checkGroundMiddle.collider != null)
    //    {

    //        runEnable = true;
    //    }

    //    else
    //    {
    //        return false;
    //    }
    //    return (checkGroundMiddle.collider != null);

    //}

    //private bool IsPlayerOnWall(Direction direction)
    //{
    //    if (direction == Direction.left)
    //    {
    //        RaycastHit2D checkLeftwall = Physics2D.Raycast(transform.position, Vector2.left, wallCheckDistance, wallLayer);
    //        if (checkLeftwall.collider != null)
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }
    //    else if (direction == Direction.right)
    //    {
    //        RaycastHit2D checkRightWall = Physics2D.Raycast(transform.position, Vector2.right, wallCheckDistance, wallLayer);

    //        if (checkRightWall.collider != null)
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

}
