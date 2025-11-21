using UnityEngine;

public class ColorClassifierSensor : MonoBehaviour
{
    // Reference colours (tweak in Inspector to match your materials)
    public Color redRef    = Color.red;
    public Color yellowRef = Color.yellow;
    public Color greenRef  = Color.green;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball")) return;

        // Get renderer colour
        Renderer rend = other.GetComponent<Renderer>();
        if (rend == null)
            rend = other.GetComponentInChildren<Renderer>();
        if (rend == null) return;

        Color c = rend.material.color;
        string colorLabel = ClassifyColor(c);

        // Store on the object
        BallLabel fl = other.GetComponent<BallLabel>();
        if (fl != null)
        {
            fl.label = colorLabel;
        }

        // Inform the central sorting manager (virtual PLC)
        SortingManager.Instance.OnBallDetected(other.gameObject, colorLabel);
    }

    // Pick the closest of 3 reference colours
    private string ClassifyColor(Color c)
    {
        float distRed    = ColorDistance(c, redRef);
        float distYellow = ColorDistance(c, yellowRef);
        float distGreen  = ColorDistance(c, greenRef);

        if (distRed <= distYellow && distRed <= distGreen)
            return "red";
        else if (distYellow <= distRed && distYellow <= distGreen)
            return "yellow";
        else
            return "green";
    }

    private float ColorDistance(Color a, Color b)
    {
        float dr = a.r - b.r;
        float dg = a.g - b.g;
        float db = a.b - b.b;
        return dr * dr + dg * dg + db * db; // squared distance is enough
    }
}
