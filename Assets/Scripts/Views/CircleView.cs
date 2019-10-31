using UnityEngine;

public class CircleView : UpgradeView
{
    private static readonly int PurchasedTriggerHash = Animator.StringToHash("Purchased");
    private static readonly int AttacksTriggerHash = Animator.StringToHash("Attacks");

    public void PlayPurchaseAnimation()
    {
        viewAnimator.SetTrigger(PurchasedTriggerHash);
    }

    public void PlayAttackAnimation()
    {
        viewAnimator.SetTrigger(AttacksTriggerHash);
    }
}