using DG.Tweening;
using UnityEngine;
using System;

public class PlayerAttacker : PlayerRoot
{
    [SerializeField] private LayerMask _layerMask;

    private RaycastHit _enemy;
    private float _runStartTime;
    private Animator _animator;

    public static event Action FightCameraEffectEvent;

    void Start()
    {
        EventManager.TriedToInteract += Fight;
        _animator = GetComponent<Animator>();
    }

    private bool IsAbleToFight()
    {
        _enemy = Hit(Vector3.back, _layerMask);

        if (!_enemy.collider.gameObject)
            return false;

        if (isRunning & Time.time - _runStartTime >= 1.5f)
            return true;
        else
            return false;
    }

    private void Fight()
    {
        if (IsAbleToFight() == true)
        {
            _enemy.transform.LookAt(_enemy.transform, transform.position);
            isMovementLocked = true;
            FightCameraEffectEvent.Invoke();

            SetAnimation(AnimationState.IsNerding, _animator);

            transform.DOMove(new Vector3(_enemy.transform.position.x - 1, transform.position.y, _enemy.transform.position.z - 1), 0.5f)
                .OnComplete(() => transform.DOMove(_enemy.transform.position - Vector3.forward * 3, 0.5f)).OnComplete(() => isMovementLocked = false);
        }
    }

}
