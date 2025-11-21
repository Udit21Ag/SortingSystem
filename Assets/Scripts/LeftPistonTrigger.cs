using UnityEngine;
using System.Collections;

public class LeftPistonTrigger : MonoBehaviour
{
    private bool busy = false;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Fruit")) return;
        if (busy) return;

        // Get classification
        string label = SortingManager.Instance.lastFruitLabel;

        // Only apple belongs to LEFT piston
        if (label != "apple") return;

        busy = true;

        GameObject fruit = other.gameObject;

        StartCoroutine(ProcessLeftPiston(fruit));
    }

    IEnumerator ProcessLeftPiston(GameObject fruit)
    {
        SortingManager.Instance.belt.StopBelt();
        SortingManager.Instance.spawner.canSpawn = false;

        yield return new WaitForSeconds(0.35f);

        SortingManager.Instance.pistons.SortFruit(fruit, "apple");

        yield return new WaitForSeconds(1.0f);

        SortingManager.Instance.belt.StartBelt();
        SortingManager.Instance.spawner.canSpawn = true;

        busy = false;
    }
}