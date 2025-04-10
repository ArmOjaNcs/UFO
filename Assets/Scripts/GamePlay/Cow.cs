using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class Cow : MonoBehaviour
{
    private const string UFO = nameof(UFO);

    [SerializeField] private float _jumpPower;
    [SerializeField] private GameObject _deadCowPrefab;
    [SerializeField] private float _minJumpTime;
    [SerializeField] private float _maxJumpTime;

    private Rigidbody _rigidbody;
    private Transform _transform;
    private float _jumpTimer;
    private bool _isCatched;

    public event Action Jumped;
    public event Action Flying;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = transform;
        _jumpTimer = _minJumpTime;
    }

    private void Update()
    {
        if (_isCatched == false)
            WaitTimeAndJump();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isCatched)
            return;

        if (collision.gameObject.CompareTag(UFO))
        {
            Instantiate(_deadCowPrefab, _transform.position, _transform.rotation);
            gameObject.Deactivate();
        }
    }

    public void GetÑaught()
    {
        _isCatched = true;
        _rigidbody.isKinematic = true;
        Flying?.Invoke();
    }

    private void Jump()
    {
        _rigidbody.linearVelocity = (Vector3.up + _transform.forward) * _jumpPower;
        Jumped?.Invoke();
    }

    private void WaitTimeAndJump()
    {
        if (_jumpTimer > 0)
        {
            _jumpTimer -= Time.deltaTime;

            if (_jumpTimer < 0)
            {
                Jump();
                _jumpTimer = Random.Range(_minJumpTime, _maxJumpTime);
            }
        }
    }
}