using UnityEngine;

public class AnimationsHandler : MonoBehaviour
{
    private Animator animator;

    public enum AnimationState
    {
        IsWalking,
        IsRunning,
        IsSitting
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {

    }

    private void SetBool(string s, bool b) => animator.SetBool(s, b);

    private void WalkingAnim(bool isWalking) => SetBool(AnimationState.IsWalking.ToString(), isWalking);
}
