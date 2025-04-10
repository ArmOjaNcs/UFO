using UnityEngine;

public class CowCatcher : MonoBehaviour
{
    [SerializeField] private float _catchDistance;
    [SerializeField] private float _catchRadius;
    [SerializeField] private float _catchTime;
    [SerializeField] private GameObject _effect;
    [SerializeField] private LayerMask _layerMask;

    private Transform _transform;
    private Transform _catchedCow;
    private PlayerInput _playerInput;
    private Vector3 _startCowPosition;
    private Vector3 _startCowScale;
    private bool _isCatchActionActive;
    private float _catchTimer = -1;

    private void Awake() => _transform = transform;

    private void Update()
    {
        if(_catchTimer > 0)
        {
            _catchTimer -= Time.deltaTime / _catchTime;
            
            if(_catchTimer < 0 || Mathf.Approximately(_catchTimer, 0))
            {
                _catchedCow.gameObject.Deactivate();
                _catchedCow = null;
                OnCatchReleased();
            }

            if (_catchedCow != null)
                UpdateCowTransform();
        }
    }

    private void FixedUpdate()
    {
        if (_isCatchActionActive == false)
            return;

        if (_catchedCow != null)
            return;

        Collider[] colliders = Physics.OverlapSphere(_transform.position + _transform.forward * _catchDistance, 
            _catchRadius, _layerMask, QueryTriggerInteraction.Ignore);

        foreach(Collider collider in colliders)
        {
            Cow cow = collider.GetComponentInParent<Cow>();

            if (cow != null)
            {
                cow.Get—aught();
                _catchedCow = cow.transform;
                _catchedCow.SetParent(_transform);
                _startCowPosition = _catchedCow.localPosition;
                _startCowScale = _catchedCow.localScale;
                _catchTimer = 1;
                return;
            }
        }
    }

    private void OnDisable()
    {
        if (_playerInput != null)
        {
            _playerInput.CatchPressed -= OnCatchPressed;
            _playerInput.CatchReleased -= OnCatchReleased;
        }
    }

    public void SetInput(PlayerInput playerInput)
    {
        _playerInput = playerInput;
        _playerInput.CatchPressed += OnCatchPressed;
        _playerInput.CatchReleased += OnCatchReleased;
    }

    private void OnCatchPressed()
    {
        _effect.Activate();
        _isCatchActionActive = true;
    }

    private void OnCatchReleased()
    {
        if (_catchedCow != null)
            return;

        _effect.Deactivate();
        _isCatchActionActive = false;
    }

    private void UpdateCowTransform()
    {
        float t = Mathf.SmoothStep(0, 1, _catchTimer);

        _catchedCow.localPosition = Vector3.Lerp(Vector3.zero, _startCowPosition, t);
        _catchedCow.localScale = Vector3.Lerp(Vector3.zero, _startCowScale, t);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + transform.forward * _catchDistance, _catchRadius);
    }
}