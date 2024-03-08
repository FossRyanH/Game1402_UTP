using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    [SerializeField]
    float pushForce = 1;


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        #region approach 1
        // no rigidbody  
        if (body == null || body.isKinematic)
            return;

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3f)
            return;

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // Apply the push
        body.velocity = pushDir * pushForce;
        #endregion


        #region approach 2
        /*
        if (body != null)
        {
            // move to one direction
            Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
            forceDirection.y = 0; //do not move on y axis
            forceDirection.Normalize();

            body.AddForceAtPosition(forceDirection * pushForce, transform.position, ForceMode.Impulse);
        }
        */
        #endregion
    }
}
