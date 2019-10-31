using UnityEngine;
using System.Collections;
using System;

public abstract class GoldGeneratorController
{
    public delegate void GoldGeneratedDelegate(double goldGenerated);
    public event GoldGeneratedDelegate OnGoldGenerated;

    protected UpgradeView view;
    protected ScaleData scaleData;

    protected double initialCost;
    protected double initialGoldPerAttack;

    public int UpgradeLevel { get; protected set; }
    public double NextUpgradeCost { get; protected set; }
    public double GoldPerAttack { get; protected set; }

    protected void InitializeController(UpgradeView view, ScaleData scaleData, GoldGeneratedDelegate onGoldGenerated)
    {
        this.view = view;
        this.scaleData = scaleData;
        OnGoldGenerated += onGoldGenerated;
        NextUpgradeCost = initialCost;
        GoldPerAttack = initialGoldPerAttack;

        UpdateViewText();
    }

    public virtual void UpgradeGoldGenerator()
    {
        view.PlayUpgradeAnimation();
        IncreaseGoldGeneratorLevel();
        UpdateViewText();
    }

    public abstract void UpdateGoldgenerator(double availableGold);

    protected void InvokeOnGoldGenerated()
    {
        OnGoldGenerated(GoldPerAttack);
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