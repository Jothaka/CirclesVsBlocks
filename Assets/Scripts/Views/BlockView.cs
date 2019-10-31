using UnityEngine;

public class BlockView : MonoBehaviour
{
    private static readonly int AttackedTriggerHash = Animator.StringToHash("Attacked");

    [SerializeField]
    private Animator viewAnimator;

    //Triggered by UI OnClick-Callback
    public void PlayAttackAnimationOnTapped()
    {
        viewAnimator.SetTrigger(AttackedTriggerHash);
    }
}
