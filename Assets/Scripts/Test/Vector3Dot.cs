using UnityEngine;

public class Vector3Dot : MonoBehaviour
{
    [SerializeField] private SimpleVector _vectorOne;
    [SerializeField] private SimpleVector _vectorTwo;

    private void Update()
    {
        gameObject.name = "Dot = " + Vector3.Dot(_vectorOne.Vector, _vectorTwo.Vector).ToString("F2");
    }
}