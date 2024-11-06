using TMPro;
using UnityEngine;

public abstract class PlayerRoot : MonoBehaviour
{
    public static Vector3 _direction { get; set; }
    public static float _moveSpeed { get; set; }
    public static bool isRunning { get; set; }
    public static bool isDraggingObject { get; set; }
    public static bool isMovementLocked { get; set; }
    public static Quaternion _rotationAngle { get; set; }
    public static Transform _playerTransform { get; set; }
    public static string _currentAnim { get; set; }

    public enum AnimationState
    {
        IsWalking,
        IsRunning,
        IsSitting,
        IsNerding
    }

    public static RaycastHit Hit(Vector3 _searchDirection, LayerMask layerMask)
    {
        if (Physics.Raycast(_playerTransform.position, _searchDirection, out RaycastHit _hit, 10f, layerMask))
        {
            return _hit;
        }

        return _hit;
    }

    public static void SetAnimation(AnimationState _newAnimation, Animator _animator)
    {
        _animator.SetBool(_currentAnim, false);
        _animator.SetBool(_newAnimation.ToString(), true);
        _currentAnim = _newAnimation.ToString();
    }

    public static void ResetAnimation(Animator _animator)
    {
        _animator.SetBool(_currentAnim, false);
    }
}
