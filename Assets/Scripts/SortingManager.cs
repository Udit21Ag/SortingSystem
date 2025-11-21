using UnityEngine;

public class SortingManager : MonoBehaviour
{
    public static SortingManager Instance;

    public ConveyorBeltController belt;
    public FruitSpawner spawner;
    public PistonController pistons;

    [HideInInspector]
    public string lastFruitLabel = "banana";

    [HideInInspector]
    public GameObject lastFruit = null;

    void Awake()
    {
        Instance = this;
    }

    // This function is called by the classifier sensor
    // BEFORE the fruit reaches any piston
    public void OnFruitDetected(GameObject fruit)
    {
        lastFruit = fruit;

        // HARD-CODED CLASSIFICATION FOR NOW
        string fname = fruit.name.ToLower();

        if (fname.Contains("apple"))
            lastFruitLabel = "apple";

        else if (fname.Contains("orange"))
            lastFruitLabel = "orange";

        else
            lastFruitLabel = "banana";

        Debug.Log("SortingManager stored label = " + lastFruitLabel);
    }

    // SortingManager no longer controls belt stopping or pistons.
    // That is handled by:
    // - LeftPistonTrigger.cs
    // - RightPistonTrigger.cs

    void Update()
    {
        // Nothing needed here for now
    }
}