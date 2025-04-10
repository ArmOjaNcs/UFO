using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private Transform _transform;

    private void OnDrawGizmos()
    {
        if (_transform == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, _transform.position);
        Gizmos.DrawSphere(_transform.position, 0.2f);
    }
}