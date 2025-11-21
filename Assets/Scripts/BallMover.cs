using UnityEngine;

public class BallMover : MonoBehaviour
{
    private Rigidbody rb;
    private ConveyorBelt belt;
    private bool onBelt = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        belt = FindFirstObjectByType<ConveyorBelt>();
    }

    void FixedUpdate()
    {
        if (onBelt && belt != null && !belt.isRunning)
        {
            rb.linearVelocity = Vector3.zero;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Belt"))
        {
            onBelt = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Belt"))
        {
            onBelt = false;

            // When leaving belt (going into basket), add small sideways motion + spin
            Vector3 sideways = Vector3.Cross(belt.transform.right, Vector3.up).normalized;
            float sideSign = Random.value < 0.5f ? -1f : 1f;

            rb.AddForce(sideways * sideSign * 0.5f, ForceMode.Impulse);
            rb.AddTorque(Random.insideUnitSphere * 1.0f, ForceMode.Impulse);
        }
    }
}
