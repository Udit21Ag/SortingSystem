using UnityEngine;
using System.Collections;

public class LeftPistonTrigger : MonoBehaviour
{
    private bool busy = false;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Fruit")) return;
        if (busy) return;

        FruitLabel fl = other.GetComponent<FruitLabel>();
        if (fl == null) return;

        // Only apple activates LEFT piston
        if (fl.label != "apple") return;

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