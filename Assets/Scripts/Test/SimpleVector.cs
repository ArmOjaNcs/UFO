using UnityEngine;

public class SimpleVector : MonoBehaviour
{
    [SerializeField] private Color _color;

    public Vector3 Vector => transform.forward * transform.localScale.magnitude;

    public void SetVector(Vector3 value)
    {
        Quaternion quaternion = Quaternion.identity;
        quaternion.SetLookRotation(value.normalized);
        transform.rotation = quaternion;
        transform.localScale = Vector3.one.normalized * value.magnitude;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _color;
        Gizmos.DrawRay(transform.position, Vector);
        Gizmos.DrawSphere(transform.position + Vector, 0.1f);
    }
}