using UnityEngine;
using DG.Tweening;
using Cinemachine;
using System;

public class AutomaticInputHandler : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    private CharacterMovement characterMovement;
    private float runStartTime;
    private Animator animator;
    private CinemachineVirtualCamera virtualCamera;

    public static event Action<Collider, bool> PreFightEffectEvent;
    public static event Action FightCameraEffectEvent;
    public static event Action SitCameraEffectEvent;
    private const string IsNerding = "IsNerding";

    private void Start()
    {
        animator = GetComponent<Animator>();
        characterMovement = GetComponent<CharacterMovement>();
        virtualCamera = FindAnyObjectByType<CinemachineVirtualCamera>();
    }

    void Update()
    {
        CheckForCollisions();
        //RotateCamera();
    }

    private void CheckForCollisions()
    {
        var hits = Physics.OverlapSphere(transform.position, 3.5f, layerMask);

        foreach (var hit in hits)
        {
            if (hit != null)
                HandleFightCollision(hit);
            else
                PreFightEffectEvent.Invoke(hit, false);
        }
    }

    private void HandleFightCollision(Collider hit)
    {
        if (characterMovement.IsRunning())
        {
            if (Time.time - runStartTime >= 1.5f)
            {
                PreFightEffectEvent.Invoke(hit, true);
                CheckForKey(hit);
            }
        }
    }

    private void CheckForKey(Collider hit)
    {
        if (Input.GetKey(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.E))
        {
            animator.SetBool(IsNerding, true);

            Fight(hit);
            FightCameraEffectEvent?.Invoke();
            runStartTime = Time.time;
        }
    }

    private void Fight(Collider hit)
    {
        transform.LookAt(hit.transform, transform.position);

        transform.DOMove(new Vector3(hit.transform.position.x - 1, transform.position.y + 1.5f, hit.transform.position.z - 1), 0.5f)
            .OnComplete(() => transform.DOMove(hit.transform.position - Vector3.forward * 3, 0.5f)).OnComplete(() => animator.SetBool(IsNerding, false));
    }

    private void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        virtualCamera.transform.RotateAround(transform.position, Vector3.up, mouseX * 6.5f);

        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Q) || Input.GetMouseButtonUp(1) || Input.GetKeyUp(KeyCode.Q))
        {
            bool isPressed = Input.GetMouseButton(1) || Input.GetKey(KeyCode.Q);
            virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = isPressed ? 45 : 30;
        }
    }
}
