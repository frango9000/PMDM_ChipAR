using UnityEngine;

public class MaskMover : MonoBehaviour
{
    public void MoveZ(float z)
    {
        var transform1 = transform;
        var localPosition = transform1.localPosition;
        localPosition = new Vector3(localPosition.x, localPosition.y, z);
        transform1.localPosition = localPosition;
    }
}