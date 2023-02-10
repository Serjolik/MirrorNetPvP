using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        var otherBrain = collision.gameObject.GetComponentInParent<PlayerBrain>();
        var myBrain = GetComponentInParent<PlayerBrain>();

        if (otherBrain == null || myBrain == null)
        {
            return;

        }

        if (otherBrain.isInDash && !myBrain.affected)
        {
            myBrain.CmdBump();
            return;
        }

        if (!otherBrain.affected && myBrain.isInDash)
        {
            myBrain.CmdOtherBumped();
            return;
        }

    }

}
