using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class QuaternionRotation : MonoBehaviour
{
    [SerializeField] private float _angle;
    [SerializeField] private Transform _axis;

    [ProPlayButton]
    public void Rotate()
    {
        Quaternion quaternion = Quaternion.AngleAxis(_angle, _axis.forward);
        transform.rotation = quaternion * transform.rotation;
    }

    private void Update()
    {
        Quaternion quaternion = Quaternion.AngleAxis(_angle * Time.deltaTime, _axis.forward);
        quaternion = Quaternion.Slerp(quaternion, Quaternion.Euler(Vector3.zero), 0.5f);
        transform.rotation = quaternion * transform.rotation;
    }

    private void OnDrawGizmos()
    {
        if (_axis == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, _axis.forward * 10);
        Gizmos.DrawRay(transform.position, -_axis.forward * 10);
    }
}
