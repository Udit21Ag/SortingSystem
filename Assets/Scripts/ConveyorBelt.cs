using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float beltSpeed = 2f;
    public bool isRunning = true;

    void OnCollisionStay(Collision collision)
    {
        if (!isRunning) return;

        Rigidbody rb = collision.rigidbody;
        if (rb == null) return;

        // Move ball along the belt direction
        rb.linearVelocity = transform.right * beltSpeed;

        // OPTIONAL: tiny downward force to keep contact with belt
        // rb.AddForce(Vector3.down * 10f, ForceMode.Force);
    }
}
