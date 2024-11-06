using Cinemachine;
using UnityEngine;

public class CameraEffectHandler : MonoBehaviour
{
    [SerializeField] private KeyCode[] _keyCodes;
    private CinemachineVirtualCamera _camera;

    void Start()
    {
        _camera = this.gameObject.GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        RotateCamera();
    }
    private void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        _camera.transform.RotateAround(transform.position, Vector3.up, mouseX * 6.5f);

        foreach (var key in _keyCodes)
        {
            if (Input.GetKeyDown(key))
            {
                SetCameraDistance(true);
            }
            else if (Input.GetKeyUp(key))
            {
                SetCameraDistance(false);
            }
        }
    }

    private void SetCameraDistance(bool isPressed)
    {
        _camera.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = isPressed ? 34 : 17;
    }

}
