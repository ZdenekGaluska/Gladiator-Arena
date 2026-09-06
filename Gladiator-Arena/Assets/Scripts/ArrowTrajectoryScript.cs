using System.Collections;
using UnityEngine;

public class ArrowTrajectoryScript : MonoBehaviour
{
    private LineRenderer _lR;
    private Transform[]  _points;
    public float _lineLength = 20f;
    void Start()
    {
        _lR = GetComponent<LineRenderer>();
        _lR.positionCount = 2;
        _lR.enabled = false;
    }


    public void DrawShootingTrajectory(Vector2 archerPosition, Vector2 PlayerPosition)
    {
        _lR.enabled = true;
        _lR.SetPosition(0, archerPosition);
        _lR.SetPosition(1,  archerPosition + (PlayerPosition - archerPosition).normalized * _lineLength);
    }

    public void EraseShootingTrajectory()
    {
      _lR.enabled = false;  
    }
}
