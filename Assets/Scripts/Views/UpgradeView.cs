using UnityEngine;
using UnityEngine.UI;

public class UpgradeView : MonoBehaviour
{
    private static readonly int upgradeTriggerHash = Animator.StringToHash("Upgrade");
    private static readonly int availableUpgradeBooleanHash = Animator.StringToHash("UpgradeAvailable");

    public delegate void OnViewInteractionDelegate();
    public event OnViewInteractionDelegate OnViewInteraction;

    [SerializeField]
    protected Animator viewAnimator;
    [SerializeField]
    protected Text upgradeText;
    [SerializeField]
    protected Text detailsText;

    //Triggered by UI OnClick-Callback
    public void OnUIButtonClicked()
    {
        OnViewInteraction?.Invoke();
    }

    public void PlayUpgradeAnimation()
    {
        viewAnimator.SetTrigger(upgradeTriggerHash);
    }

    public void SetUpgradeAvailableState(bool isUpgradeable)
    {
        viewAnimator.SetBool(availableUpgradeBooleanHash, isUpgradeable);
    }

    public void SetUpgradeText(string upgradeText)
    {
        this.upgradeText.text = upgradeText;
    }

    public void SetDetailsText(string detailsText)
    {
        this.detailsText.text = detailsText;
    }
}