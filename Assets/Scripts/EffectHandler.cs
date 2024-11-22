using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class EffectHandler : MonoBehaviour
{
    [SerializeField] private GameObject effect;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Transform target;

    void Start()
    {
        virtualCamera = FindAnyObjectByType<CinemachineVirtualCamera>();

        //AutomaticInputHandler.SitCameraEffectEvent += SitCameraEffect;
        PlayerAttacker.FightCameraEffectEvent += FightCameraEffect;
    }

    void Update()
    {
    }

    private void FightCameraEffect()
    {
        virtualCamera.transform.DOShakeRotation(1, 10, 15, 90f);
        virtualCamera.transform.DORotate(new Vector3(27,0,0), 0.5f);
    }


    private void SitCameraEffect()
    {
        virtualCamera.transform.DOShakeRotation(10, 0.5f, 1, 90f);
        //virtualCamera.transform.DORotate(new Vector3(18, 0, 0), 0.5f);
    }
}