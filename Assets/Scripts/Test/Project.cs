using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class Project : MonoBehaviour
{
    [SerializeField] private SimpleVector _project;
    [SerializeField] private SimpleVector _plane;

    [ProPlayButton]
    public void ProjectOnPlane()
    {
        _project.SetVector(Vector3.ProjectOnPlane(_project.Vector, _plane.Vector));
    }
}
