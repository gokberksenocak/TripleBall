using UnityEngine;

public class Lines : MonoBehaviour
{
    [SerializeField] private LineRenderer _line;
    [SerializeField] private Transform firstpoint;
    [SerializeField] private Transform lastpoint;
    void Start()
    {
        _line.positionCount = 2;
    }
    void FixedUpdate()
    {
        _line.SetPosition(0, firstpoint.position);
        _line.SetPosition(1, lastpoint.position);
    }
}