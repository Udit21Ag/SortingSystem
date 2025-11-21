using UnityEngine;

public class ConveyorBeltController : MonoBehaviour
{
    public ConveyorBelt belt;

    void Start()
    {
        if (belt == null) belt = GetComponent<ConveyorBelt>();
    }

    public void StartBelt()
    {
        belt.isRunning = true;
    }

    public void StopBelt()
    {
        belt.isRunning = false;
    }
}