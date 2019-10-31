using System;

public class TapController : GoldGeneratorController
{
    public TapController(UpgradeView tapView, ScaleData scaleData, GoldGeneratedDelegate onTapAttack)
    {
        initialCost = scaleData.UpgradeCostMultiplier * Math.Pow(scaleData.UpgradeCostPowed, UpgradeLevel);
        initialGoldPerAttack = 1;
        InitializeController(tapView, scaleData, onTapAttack);
    }

    public override void UpdateGoldgenerator(double availableGold)
    {
        view.SetUpgradeAvailableState(availableGold >= NextUpgradeCost);
    }

    public void OnBlockTapped()
    {
        InvokeOnGoldGenerated();
    }

    protected override void UpdateDetailsText()
    {
        string detailsText = string.Format("{0} g/tap", GoldPerAttack.FormatForUI());
        view.SetDetailsText(detailsText);
    }
}