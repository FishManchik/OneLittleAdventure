using UnityEngine;

public class InputHandler : PlayerRoot
{
    [SerializeField] private LayerMask _layer;
    private RaycastHit _hit;

    void Update()
    {
        _hit = Hit(Vector3.down, _layer);

        HandleClick();
        SetDirection();

        if (_direction != Vector3.zero)
        {
            SetRotation();
        }
    }
    private void SetDirection()
    {
        _direction = Camera.main.transform.TransformDirection(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"))).normalized;
        _direction = Vector3.ProjectOnPlane(_direction, _hit.normal).normalized;
    }


    private void SetRotation()
    {
        _rotationAngle = Quaternion.LookRotation(_direction, _hit.normal);
    }

    private void HandleClick()
    {
        isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        if (Input.GetKeyUp(KeyCode.E))
        {
            EventManager.OnTriedToInteract();
        }

        /*
                switch (true)
        {
            case true when Input.GetKey(KeyCode.LeftControl):
                EventManager.OnSit();
                break;

            case true when Input.GetKey(KeyCode.E):
                EventManager.OnTriedToInteract();
                break;
        }
        */
    }
}