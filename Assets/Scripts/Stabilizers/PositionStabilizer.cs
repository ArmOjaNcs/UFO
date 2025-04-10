using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PositionStabilizer : MonoBehaviour
{
    [SerializeField] private float _stabilizerForce;
    [SerializeField] private float _dampingForce;

    private Rigidbody _rigidbody;
    private float _stabilizedForceZ;

    private float PositionZ => _rigidbody.position.z;
    private float VelocityZ => _rigidbody.linearVelocity.z;

    private void Awake() => _rigidbody = GetComponent<Rigidbody>();

    private void FixedUpdate()
    {
        _stabilizedForceZ = -PositionZ * _stabilizerForce - VelocityZ * _dampingForce;
        _rigidbody.AddForce(Vector3.forward * _stabilizedForceZ, ForceMode.Force);
    }
}