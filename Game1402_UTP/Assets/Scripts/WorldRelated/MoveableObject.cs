using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    [SerializeField]
    float pushForce = 1;


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody _rb = hit.collider.attachedRigidbody;

        //if no rigidbody, return
        if (_rb == null || _rb.isKinematic)
            return;

        //if push objects below -0.3f, return
        if (hit.moveDirection.y < -0.3f)
            return;

        //calculate push direction from move direction,
        //we only push objects to the x and z axis never y axis
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        //apply the push
        _rb.velocity = pushDir * pushForce;
    }
}
