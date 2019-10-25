using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPackController : MonoBehaviour
{
    public PlayerController playerController;

    [SerializeField] private FuelBarController fueltank;

    [SerializeField] private InterfaceController interfaceController;
   
    
    public float jetpackFuel;
    public float jetpackFuelDeplete;
    [SerializeField]
    private float internalJetpackFuel;

    private bool attachedToPlayer = false;
    public bool touchJetpack = false;

    void Start()
    {
        internalJetpackFuel = jetpackFuel;
    }
    void FixedUpdate()
    {
        if (internalJetpackFuel <= 0)
        {
            DeatachFromPlayer();
        }
        if (attachedToPlayer)
        {
            if (touchJetpack)
            {
                DepleteFuel();
            }
#if (UNITY_STANDALONE)
            if (Input.GetKey(KeyCode.Space))
            {
                DepleteFuel();
            }
#endif
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Jetpack"))
        {
            internalJetpackFuel = jetpackFuel;
            fueltank.SetFullTank();
            attachedToPlayer = true;
            playerController.JetpackAquired();
            //interfaceController.ActivateFuelTank();
            Debug.Log("JetPack: Touched player!");
        }
    }

    void DeatachFromPlayer()

    {

        //gameObject.SetActive(true);
        playerController.JetpackRemoved();
        internalJetpackFuel = jetpackFuel;
        attachedToPlayer = false;
        //interfaceController.DeactivateFueltank();
    }

    private void DepleteFuel()
    {
        internalJetpackFuel -= jetpackFuelDeplete * Time.fixedDeltaTime;

        fueltank.DepleteBar(internalJetpackFuel / jetpackFuel);
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
