using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CowAnimator : MonoBehaviour
{
    [SerializeField] private Cow _cow;

    private int _jump = Animator.StringToHash("Jump");
    private int _fly = Animator.StringToHash("Fly");
    private Animator _animator;

    private void Awake() => _animator = GetComponent<Animator>();

    private void OnEnable()
    {
        _cow.Jumped += OnJumped;
        _cow.Flying += OnFlying;
    }

    private void OnDisable()
    {
        _cow.Jumped -= OnJumped;
        _cow.Flying -= OnFlying;
    }

    private void OnJumped() => _animator.SetTrigger(_jump);

    private void OnFlying()
    {
        _animator.SetBool(_fly, true);
    }
}