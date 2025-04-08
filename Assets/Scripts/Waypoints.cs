using UnityEngine;

public class Waypoints : MonoBehaviour
{
    [SerializeField] private Transform[] _points;

    private int _currentPoint = 0;

    public Transform NextPoint() {
        _currentPoint++;
        if(_currentPoint >= _points.Length)
            _currentPoint = 0;

        return GetCurrentPoint();
    }

    public Transform GetCurrentPoint() => _points[_currentPoint]; 
}
