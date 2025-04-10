using UnityEngine;

public class Vector3Cross : MonoBehaviour
{
    [SerializeField] private SimpleVector _vectorOne;
    [SerializeField] private SimpleVector _vectorTwo;

    [SerializeField] private SimpleVector _resultVector;

    private void Update()
    {
        _resultVector.SetVector(Vector3.Cross(_vectorOne.Vector, _vectorTwo.Vector));        
    }
}