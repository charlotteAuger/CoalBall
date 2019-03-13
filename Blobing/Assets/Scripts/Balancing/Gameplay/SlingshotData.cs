using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SlingshotData", menuName = "Balancing/SlingshotData", order = 1)]
public class SlingshotData : ScriptableObject {

    public float maxForce;
    public float ballLienarDrag;
    public float maxDistance;
    public float pullingLerpCoeff;
    public float returnLerpCoeff;
}
