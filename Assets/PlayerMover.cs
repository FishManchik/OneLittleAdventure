using UnityEngine;

public class PlayerMover : PlayerRoot
{
    [SerializeField] private LayerMask _layer;
    private Animator _animator;

    private void Start()
    {
        _playerTransform = this.transform;
        _animator = GetComponent<Animator>();

        //SetAnimation(AnimationState.IsSitting, _animator);
    }

    void Update()
    {
        if (!isMovementLocked)
            ResetAnimation(_animator);

        SetSpeed();

        if (_direction != Vector3.zero)
        {
            if (!isMovementLocked)
            {
                Move();
                Rotate();
            }
        }
    }

    private void Move()
    {
        transform.Translate(_direction * _moveSpeed * Time.deltaTime, Space.World);
        SetAnimation(isRunning ? AnimationState.IsRunning : AnimationState.IsWalking, _animator);
    }

    private void Rotate()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, SetTargetRotation(), Time.deltaTime * 15f);
    }

    private Quaternion SetTargetRotation()
    {
        return Quaternion.LookRotation(_direction, Hit(Vector3.down, _layer).normal);
    }

    private float SetSpeed()
    {
        float targetSpeed = isRunning ? 6f : 2f;
        return _moveSpeed = Mathf.MoveTowards(_moveSpeed, targetSpeed, Time.deltaTime * 10f);
    }
}