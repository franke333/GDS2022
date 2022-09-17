using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BezierCurve))]
public class DrawBezierExample : Editor
{
    private void OnSceneViewGUI(SceneView sv)
    {
        BezierCurve be = target as BezierCurve;

        be.be_start.transform.position = Handles.PositionHandle(be.be_start.transform.position, Quaternion.identity);
        be.be_end.transform.position = Handles.PositionHandle(be.be_end.transform.position, Quaternion.identity);
        be.bet_start.transform.position = Handles.PositionHandle(be.bet_start.transform.position, Quaternion.identity);
        be.bet_end.transform.position = Handles.PositionHandle(be.bet_end.transform.position, Quaternion.identity);

        Handles.DrawBezier(be.be_start.transform.position, be.be_end.transform.position, be.bet_start.transform.position, be.bet_end.transform.position, Color.red, null, 4f);
    }

    void OnEnable()
    {
        Debug.Log("OnEnable");
        SceneView.onSceneGUIDelegate += OnSceneViewGUI;
    }

    void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneView.onSceneGUIDelegate -= OnSceneViewGUI;
    }
}