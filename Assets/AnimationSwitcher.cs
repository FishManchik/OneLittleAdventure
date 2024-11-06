using UnityEditor;
using UnityEngine;

public class AnimationSwitcher : PlayerRoot
{
    private Animator _animator;

    void Start()
    {
        EventManager.SitEvent += PlaySitAnimation;
        _animator = GetComponent<Animator>();
    }

    private void PlaySitAnimation()
    {
        //SetAnimation(AnimationState.IsSitting, _animator);
    }
}
