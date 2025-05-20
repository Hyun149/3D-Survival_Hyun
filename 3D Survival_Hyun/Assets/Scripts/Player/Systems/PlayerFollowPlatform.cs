using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerFollowPlatform : MonoBehaviour
{
    private Rigidbody playerRb;
    private MovingPlatform currentPlatform;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            if (currentPlatform == null)
            {
                currentPlatform = collision.gameObject.GetComponent<MovingPlatform>();
            }

            if (currentPlatform != null)
            {
                playerRb.position += currentPlatform.DeltaPosition;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            currentPlatform = null;
        }
    }
}
