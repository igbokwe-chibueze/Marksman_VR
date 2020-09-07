using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [Tooltip("Lifetime of the floating text.")]
    public float destroyTime = 2f;

    [Tooltip("Offset on the Y-axis to ensure this gameobjects spawns ontop of its parent.")]
    public Vector3 offSet = new Vector3 (0.5f, 2, 0);

    [Tooltip("Randomise Z-axis of this object position")]
    public Vector3 randomizedIntensity = new Vector3 (0.5f, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);
        transform.localPosition += offSet;
        transform.localPosition += new Vector3(Random.Range(-randomizedIntensity.x, randomizedIntensity.x), Random.Range(-randomizedIntensity.x, randomizedIntensity.y), Random.Range(-randomizedIntensity.x, randomizedIntensity.z));
    }
}
