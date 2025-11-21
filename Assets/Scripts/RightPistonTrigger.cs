using UnityEngine;
using System.Collections;

public class RightPistonTrigger : MonoBehaviour
{
    private bool busy = false;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Fruit")) return;
        if (busy) return;

        FruitLabel fl = other.GetComponent<FruitLabel>();
        if (fl == null) return;

        // Only orange activates RIGHT piston
        if (fl.label != "orange") return;

        busy = true;

        GameObject fruit = other.gameObject;
        StartCoroutine(ProcessRightPiston(fruit));
    }

    IEnumerator ProcessRightPiston(GameObject fruit)
    {
        SortingManager.Instance.belt.StopBelt();
        SortingManager.Instance.spawner.canSpawn = false;

        yield return new WaitForSeconds(0.35f);

        SortingManager.Instance.pistons.SortFruit(fruit, "orange");

        yield return new WaitForSeconds(1.0f);

        SortingManager.Instance.belt.StartBelt();
        SortingManager.Instance.spawner.canSpawn = true;

        busy = false;
    }
}