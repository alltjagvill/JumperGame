﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushTextToFront : MonoBehaviour
{

    public string layerToPushTo;

    void Start()
    {
        GetComponent<Renderer>().sortingLayerName = layerToPushTo;
        //Debug.Log(GetComponent<Renderer>().sortingLayerName);
    }
}
