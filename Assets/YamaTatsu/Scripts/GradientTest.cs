using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GradientTest : MonoBehaviour {

    public Gradient gradient;

    [Range(0, 1)]
    public float time = 0;

    void OnValidate()
    {
        this.GetComponent<Graphic>().color = gradient.Evaluate(time);
    }
}
