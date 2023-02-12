using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject Target;

    private void Update()
    {
        transform.position = new(Target.transform.position.x, Target.transform.position.y, transform.position.z);
    }
}
