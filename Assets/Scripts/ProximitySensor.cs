using UnityEngine;

public class ProximitySensor : MonoBehaviour
{
    // Expose state
    [HideInInspector]
    public bool isObjectPresent = false;

    public string sensorName = "Sensor1";

    // Optional: a Unity UI text or other UI can be updated here

    private void OnTriggerEnter(Collider other)
    {
        // You can detect by tag or name; we assume the "Fruit" object exists
        if (other.gameObject.CompareTag("Ball") || other.gameObject.name == "Ball")
        {
            isObjectPresent = true;
            Debug.Log($"{sensorName}: Object Entered (True)");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ball") || other.gameObject.name == "Ball")
        {
            isObjectPresent = false;
            Debug.Log($"{sensorName}: Object Exited (False)");
        }
    }
}