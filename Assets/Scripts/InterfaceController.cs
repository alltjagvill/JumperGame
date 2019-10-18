using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceController : MonoBehaviour
{
    public Camera mainCamera;
    private GameObject jetpackFuelItem;
    [SerializeField] float hide;
    [SerializeField] float unHide;

    //private SpriteRenderer fuelSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        jetpackFuelItem = GameObject.Find("InterfaceJetpack");
        //fuelSpriteRenderer = jetpackFuelItem.GetComponent<SpriteRenderer>;
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowMainCamera();
    }

    public void FollowMainCamera()
    {
        transform.position = mainCamera.transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    //public void ActivateFuelTank()
    //{

    //    jetpackFuelItem.GetComponent<SpriteRenderer>().enabled = true;
    //    for (int i = 0; i < jetpackFuelItem.transform.childCount; i++)
    //    {
    //        var child = jetpackFuelItem.transform.GetChild(i).gameObject;
    //        if (child != null)
    //        {
    //            child.GetComponent<SpriteRenderer>().enabled = true;
    //        }
    //    }
    //    //jetpackFuelItem.transform.position = new Vector3(jetpackFuelItem.transform.position.x, jetpackFuelItem.transform.position.y, unHide);
    //}

    //public void DeactivateFueltank()
    //{

    //    for (int i = 0; i < jetpackFuelItem.transform.childCount; i++)
    //    {
    //        var child = jetpackFuelItem.transform.GetChild(i).gameObject;
    //        if (child != null)
    //        {
    //            child.GetComponent<SpriteRenderer>().enabled = false;
    //        }
    //    }
    //    //jetpackFuelItem.transform.position = new Vector3(jetpackFuelItem.transform.position.x, jetpackFuelItem.transform.position.y, hide);
    //}
}
