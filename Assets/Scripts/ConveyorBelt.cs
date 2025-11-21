using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float beltSpeed = 2f;
    public bool isRunning = true;

    void OnCollisionStay(Collision collision)
    {
        if (!isRunning) return;

        Rigidbody rb = collision.rigidbody;

        if (rb != null)
        {
            // Move fruit along the X-axis
            rb.linearVelocity = transform.right * beltSpeed;
        }
    }
}