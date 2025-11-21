using UnityEngine;
using System.Collections;

public class LeftPistonTrigger : MonoBehaviour
{
    private bool busy = false;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball")) return;
        if (busy) return;

        BallLabel fl = other.GetComponent<BallLabel>();
        if (fl == null) return;

        // Only RED ball activates LEFT piston
        if (fl.label != "red") return;

        busy = true;

        GameObject ball = other.gameObject;
        StartCoroutine(ProcessLeftPiston(ball));
    }

    IEnumerator ProcessLeftPiston(GameObject ball)
    {
        SortingManager.Instance.belt.StopBelt();
        SortingManager.Instance.spawner.canSpawn = false;

        yield return new WaitForSeconds(0.35f);

        SortingManager.Instance.pistons.SortBall(ball, "red");

        yield return new WaitForSeconds(1.0f);

        SortingManager.Instance.belt.StartBelt();
        SortingManager.Instance.spawner.canSpawn = true;

        busy = false;
    }
}
