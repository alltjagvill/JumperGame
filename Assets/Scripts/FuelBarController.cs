using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelBarController : MonoBehaviour
{
    private Transform bar;
    private void Start()
    {
        bar = transform.Find("Bar");
        bar.localScale = new Vector3(1f, 0f);
    }

   public void SetFullTank()
    {
        bar.localScale = new Vector3(1f, 1f);
    }

   public void DepleteBar(float sizeNormalized)
    {
        bar.localScale = new Vector3(1f, sizeNormalized);
    }

   
}
