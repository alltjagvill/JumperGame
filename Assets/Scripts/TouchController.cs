using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    private PlayerController player;
    private JetPackController jetpack;
    // public PlayerController playerController;



    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        jetpack = FindObjectOfType<JetPackController>();
    }

    void Update()
    {
        
    }
    public void LeftArrow()
        {
        player.TouchWalkLeftSetTrue();
    }

    public void UnpressLeftArrow()
    {
        player.TouchWalkLeftSetFalse();
    }

    public void RightArrow()
        {
        player.TouchWalkRightSetTrue();
    }

    public void UnpressRightArrow()
    {
        player.TouchWalkRighttSetFalse();
    }

    public void Jetpack()
    {
        player.TouchJetpackSetTrue();
        jetpack.TouchJetpackSetTrue();
    }

    public void UnpressJetpack()
    {
        player.TouchJetpackSetFalse();
        jetpack.TouchJetpackSetFalse();
    }

    public void JumpButton()
    {
        player.TouchJumpSetTrue();
        player.TouchJumping();
        
    }

}
