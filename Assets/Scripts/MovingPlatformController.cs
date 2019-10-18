using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;
    public float speed;
    Vector2 startVec2Pos;
    Vector2 endVec2Pos;
    private bool goingToEndPoint = true;
    
    void Start()
    {
        startVec2Pos = startPos.position;
        endVec2Pos = endPos.position;
        transform.position = startPos.position;
       
        
    }
    void Update()
    {
        //transform.position = Vector2.MoveTowards(transform.position, endVec2Pos, speed * Time.deltaTime);
        if (goingToEndPoint == true )
        {
            transform.position = Vector2.MoveTowards(transform.position, endVec2Pos, speed * Time.deltaTime);
           

            if (transform.position == endPos.position)
            {
                goingToEndPoint = false;
            }
        }

        if (goingToEndPoint == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, startVec2Pos, speed * Time.deltaTime);
            if (transform.position == startPos.position)
            {
                goingToEndPoint = true;
            }
        }
    }

    
}
