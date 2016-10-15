using UnityEngine;

[ExecuteInEditMode]
public class FaceTarget : MonoBehaviour
{
    public Transform target;

    void LateUpdate()
    {
        if (target != null)
            transform.rotation = target.rotation;
    }
}