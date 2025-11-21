using UnityEngine;

public class SortingManager : MonoBehaviour
{
    public static SortingManager Instance;

    public ConveyorBeltController belt;
    public BallSpawner spawner;
    public PistonController pistons;

    [HideInInspector] public string lastBallLabel = "green";
    [HideInInspector] public GameObject lastBall  = null;

    void Awake()
    {
        Instance = this;
    }

    // Called by ColorClassifierSensor when a ball passes under it
    public void OnBallDetected(GameObject ball, string label)
    {
        lastBall      = ball;
        lastBallLabel = label;
    }
}
