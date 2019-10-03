using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInput : MonoBehaviour
{
    public delegate void ButtonPress();

    public static ButtonPress PressLeft;
    public static ButtonPress PressRight;
    public static ButtonPress Jump;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
            {
            if (PressLeft != null)
            {
                PressLeft();
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (PressRight != null)
            {
                PressRight();
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (Jump != null)
            {
                Jump();
            }
        }
    }
}
