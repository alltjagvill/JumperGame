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

#if (UNITY_EDITOR)
            if (Input.GetKey(KeyCode.Space))
            {
                internalJetpackFuel -= jetpackFuelDeplete * Time.fixedDeltaTime;
                
                fueltank.DepleteBar(internalJetpackFuel/jetpackFuel);
            }
#endif
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
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
}
