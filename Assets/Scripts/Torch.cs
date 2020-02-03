using UnityEngine;
using Vuforia;

public class Torch : MonoBehaviour
{
    public void SetTorch(bool enable)
    {
        CameraDevice.Instance.SetFlashTorchMode(enable);
    }
}