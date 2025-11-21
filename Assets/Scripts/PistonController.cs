using UnityEngine;
using System.Collections;

public class PistonController : MonoBehaviour
{
    public Transform leftPiston;
    public Transform rightPiston;

    public float extendDistance = 2.0f;
    public float extendSpeed = 6f;
    public float retractDelay = 0.4f;

    private Vector3 leftStartPos;
    private Vector3 rightStartPos;

    void Start()
    {
        leftStartPos = leftPiston.localPosition;
        rightStartPos = rightPiston.localPosition;
    }

    public void SortBall(GameObject ball, string label)
    {
        if (label == "red")
            StartCoroutine(ActivatePiston(leftPiston, leftStartPos));
        else if (label == "yellow")
            StartCoroutine(ActivatePiston(rightPiston, rightStartPos));
        else
        {
            // "green" → no piston, fruit continues straight
        }
    }


    IEnumerator ActivatePiston(Transform piston, Vector3 startPos)
    {
        // EXTEND in piston’s own forward direction
        Vector3 targetPos = startPos + piston.forward * extendDistance;

        // Extend animation
        while (Vector3.Distance(piston.localPosition, targetPos) > 0.01f)
        {
            piston.localPosition = Vector3.Lerp(
                piston.localPosition, targetPos, extendSpeed * Time.deltaTime
            );
            yield return null;
        }

        yield return new WaitForSeconds(retractDelay);

        // Retract animation
        while (Vector3.Distance(piston.localPosition, startPos) > 0.01f)
        {
            piston.localPosition = Vector3.Lerp(
                piston.localPosition, startPos, extendSpeed * Time.deltaTime
            );
            yield return null;
        }
    }
}