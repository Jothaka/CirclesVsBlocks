using System;

public abstract class GoldGeneratorController
{
    //used for generating Gold as well as for paying upgrades
    public delegate void GoldGeneratorDelegate(double goldChangeAmount);
    public event GoldGeneratorDelegate OnGoldChanged;

    protected UpgradeView view;
    protected ScaleData scaleData;

    protected double initialCost;
    protected double initialGoldPerAttack;

    public int UpgradeLevel { get; protected set; }
    public double NextUpgradeCost { get; protected set; }
    public double GoldPerAttack { get; protected set; }

    protected void InitializeController(UpgradeView view, ScaleData scaleData, GoldGeneratorDelegate onGoldChanged)
    {
        this.view = view;
        this.scaleData = scaleData;
        OnGoldChanged += onGoldChanged;
        NextUpgradeCost = initialCost;
        GoldPerAttack = initialGoldPerAttack;

        UpdateViewText();
        view.OnViewInteraction += UpgradeGoldGenerator;
    }

    public virtual void UpgradeGoldGenerator()
    {
        if (GoldController.Instance.AvailableGold >= NextUpgradeCost)
        {
            PayUpgrade();
            view.PlayUpgradeAnimation();
            IncreaseGoldGeneratorLevel();
            UpdateViewText();
        }
    }

    public abstract void UpdateGoldgenerator(double availableGold);

    protected void GenerateGold()
    {
        OnGoldChanged(GoldPerAttack);
    }

    protected void PayUpgrade()
    {
        OnGoldChanged(-NextUpgradeCost);
    }

    protected void IncreaseGoldGeneratorLevel()
    {
        UpgradeLevel++;
        NextUpgradeCost = scaleData.UpgradeCostMultiplier * Math.Pow(scaleData.UpgradeCostPowed, UpgradeLevel);
        GoldPerAttack = scaleData.GoldPerTapMultiplier * Math.Pow(UpgradeLevel, scaleData.GoldPerTapPow);
    }

    protected void UpdateViewText()
    {
        UpdateUpgradeText();
        UpdateDetailsText();
    }

    protected void UpdateUpgradeText()
    {
        string upgradeText = NextUpgradeCost.FormatForUI() + " g";

        view.SetUpgradeText(upgradeText);
    }

    protected virtual void UpdateDetailsText()
    {
        string detailsText = string.Format("{0} g/s", GoldPerAttack.FormatForUI());
        view.SetDetailsText(detailsText);
    }
}