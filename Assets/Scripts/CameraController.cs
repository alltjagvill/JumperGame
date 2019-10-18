using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    void Update()
    {
        transform.position = new Vector3(0, player.position.y + offset.y, offset.z);
    }

    //public void MoveToCameraPoint()
    //{
    //    Debug.Log("Camera Moved!");
    //    transform.position = new Vector3(0, player.position.y + offset.y, offset.z);
    //}
}
