using UnityEngine;

public class FruitMover : MonoBehaviour
{
    private Rigidbody rb;
    private ConveyorBelt belt;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // New API for Unity 2022+ (no warning)
        belt = FindFirstObjectByType<ConveyorBelt>();
    }

    void FixedUpdate()
    {
        if (belt == null) return;

        // Freeze fruit if belt stops
        if (!belt.isRunning)
        {
            rb.linearVelocity = Vector3.zero;
        }
    }
}