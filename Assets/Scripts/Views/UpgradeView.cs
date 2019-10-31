using UnityEngine;
using UnityEngine.UI;

public abstract class UpgradeView : MonoBehaviour
{
    private static readonly int upgradeTriggerHash = Animator.StringToHash("Upgrade");
    private static readonly int availableUpgradeBooleanHash = Animator.StringToHash("UpgradeAvailable");

    [SerializeField]
    protected Animator viewAnimator;
    [SerializeField]
    protected Text upgradeText;
    [SerializeField]
    protected Text detailsText;

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