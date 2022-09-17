using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class BezierCurve : MonoBehaviour
{
    public GameObject be_start, be_end, bet_start, bet_end;

    public Vector3 PosAtTime(float t)
    {
        var a = Vector3.Lerp(be_start.transform.position, bet_start.transform.position, t);
        var b = Vector3.Lerp(bet_start.transform.position, bet_end.transform.position, t);
        var c = Vector3.Lerp(bet_end.transform.position, be_end.transform.position, t);
        var d = Vector3.Lerp(a, b, t);
        var e = Vector3.Lerp(b, c, t);
        return  Vector3.Lerp(d, e, t);
    }
}
