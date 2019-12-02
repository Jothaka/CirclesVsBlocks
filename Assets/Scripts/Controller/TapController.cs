using System;

public class TapController : GoldGeneratorController
{
    private BlockView blockView;

    public TapController(UpgradeView tapView, BlockView blockView, ScaleData scaleData, GoldGeneratorDelegate onTapAttack)
    {
        initialCost = scaleData.UpgradeCostMultiplier * Math.Pow(scaleData.UpgradeCostPowed, UpgradeLevel);
        initialGoldPerAttack = 1;
        InitializeController(tapView, scaleData, onTapAttack);
        this.blockView = blockView;
        this.blockView.OnViewInteraction += OnBlockTapped;
    }

    public override void UpdateGoldgenerator(double availableGold)
    {
        view.SetUpgradeAvailableState(availableGold >= NextUpgradeCost);
    }

    public void OnBlockTapped()
    {
        GenerateGold();
        blockView.PlayAttackAnimation();
    }

    protected override void UpdateDetailsText()
    {
        string detailsText = string.Format("{0} g/tap", GoldPerAttack.FormatForUI());
        view.SetDetailsText(detailsText);
    }
}