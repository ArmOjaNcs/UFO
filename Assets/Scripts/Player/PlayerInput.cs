using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);

    private Vector2 _moveDirection;

    private bool IsCatchPressed => Input.GetKeyDown(KeyCode.Mouse0);
    private bool IsCatchReleased => Input.GetKeyUp(KeyCode.Mouse0);

    public event Action CatchPressed;
    public event Action CatchReleased;
    public event Action<float> HorizontalDirectionChanged;
    public event Action<float> VerticalDirectionChanged;
    

    private void Update()
    {
        _moveDirection = Vector2.right * Input.GetAxis(Horizontal) + Vector2.up * Input.GetAxis(Vertical);
        HorizontalDirectionChanged?.Invoke(_moveDirection.x);
        VerticalDirectionChanged?.Invoke(_moveDirection.y);

        if(IsCatchPressed)
            CatchPressed?.Invoke();

        if(IsCatchReleased)
            CatchReleased?.Invoke();
    }
}