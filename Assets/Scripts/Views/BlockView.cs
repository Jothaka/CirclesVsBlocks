using UnityEngine;

public class BlockView : MonoBehaviour
{
    private static readonly int AttackedTriggerHash = Animator.StringToHash("Attacked");

    public event UpgradeView.OnViewInteractionDelegate OnViewInteraction;

    [SerializeField]
    private Animator viewAnimator;

    //Triggered by UI OnClick-Callback
    public void OnUIButtonClicked()
    {
        OnViewInteraction?.Invoke();
    }

    public void PlayAttackAnimation()
    {
        viewAnimator.SetTrigger(AttackedTriggerHash);
    }
}
