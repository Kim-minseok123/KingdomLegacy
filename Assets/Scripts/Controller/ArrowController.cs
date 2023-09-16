using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowController : MonoBehaviour
{
    GameObject _body;
    GameObject _head;
    public int resolution = 20;
    bool _isDraw = false;
    List<GameObject> _arrow = new();
    void Start()
    {
        _head = Managers.Resource.Load<GameObject>("Prefabs/Arrow/HeadArrow");
        _body = Managers.Resource.Load<GameObject>("Prefabs/Arrow/BodyArrow");
    }
    public void DrawArrow(Vector3 startPos, Vector3 endPos)
    {
        if (!_isDraw)
        {
            StartDraw();
        }
        Vector3 controlPoint = startPos + (endPos - startPos) / 2 + Vector3.up * (endPos - startPos).magnitude / 2;
        Vector3[] bezierPoints = GetBezierPoints(startPos, controlPoint, endPos, resolution);

        for (int i = 0; i < bezierPoints.Length - 2; i++)
        {

            if (i == bezierPoints.Length - 3)
            {
                _arrow[i].transform.position = bezierPoints[i+1];
                Utils.LookAt2D(_arrow[i].transform, bezierPoints[i + 2]);
            }
            else {
                _arrow[i].transform.position = bezierPoints[i];
                Utils.LookAt2D(_arrow[i].transform, bezierPoints[i + 1]);
            }
        }
    }
    void StartDraw()
    {
        for (int i = 0; i < resolution - 3; i++)
        {
            _arrow.Add(Managers.Resource.Instantiate(_body, transform));
        }
        _arrow.Add(Managers.Resource.Instantiate(_head, transform));
        _isDraw = true;
    }
    public void StopDrawArrow()
    {
        foreach (GameObject obj in _arrow)
        {
            Managers.Resource.Destroy(obj);
        }
        _arrow.Clear();
        _isDraw = false;
    }
    private Vector3[] GetBezierPoints(Vector3 start, Vector3 control, Vector3 end, int resolution)
    {
        Vector3[] points = new Vector3[resolution];
        for (int i = 0; i < resolution; i++)
        {
            float t = i / (float)(resolution - 1);
            points[i] = Utils.GetCurvePoint(start, control, end, t);
        }
        return points;
    }

}
