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
    public float jumpSpeedUp;
    public float jumpSpeedSide;
    public float jumpHeightSide;
    private float moveInput;
    public float groundCheckDistance = 0.01f;
    public float wallCheckDistance = 0.01f;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public bool jumpEnabled = true;
    private bool runEnable = true;
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
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
 

    private void FixedUpdate()
    {
#if (UNITY_EDITOR)
        
        //if (!runEnable)
        //{
        //    if (IsPlayerOnGround())
        //        runEnable = true;
        //}

        if (Input.GetKey(KeyCode.A))
        {
            moveInput = -1;
          
                if (runEnable == true)
                {
                
                rigidBody.velocity = new Vector2(moveInput * moveSpeed, rigidBody.velocity.y);
                
                //RunLeft();
                animator.SetBool(leftPressedBoolHash, true);
                } 
        }

        if (!Input.GetKey(KeyCode.A))
        {
            animator.SetBool(leftPressedBoolHash, false);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            spriteRenderer.flipX = true;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            spriteRenderer.flipX = false;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveInput = 1;
         
            if (runEnable == true)

            {
                
                rigidBody.velocity = new Vector2(moveInput * moveSpeed, rigidBody.velocity.y);
                
                //RunRight();
                animator.SetBool(rightPressedBoolHash, true);
            }        
        }

        if (!Input.GetKey(KeyCode.D))
            {
                animator.SetBool(rightPressedBoolHash, false);
            }

        

        if (Input.GetKeyDown(KeyCode.Space))
        {
           // Jump();
        }
#endif

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
