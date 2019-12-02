using UnityEngine;
using System;

public class CircleController : GoldGeneratorController
{
    public event Action OnPurchased;

    private const int InitialCostMultiplier = 10;
    private const float AttackDelay = 1.0f;

    private float timeSinceLastAttack;

    public CircleController(CircleView circleView, ScaleData scaleData, GoldGeneratorDelegate onCircleAttacks)
    {
        initialCost = 100;
        initialGoldPerAttack = 0;
        InitializeController(circleView, scaleData, onCircleAttacks);
    }

    public override void UpdateGoldgenerator(double availableGold)
    {
        if (UpgradeLevel == 0)
            return;

        timeSinceLastAttack += Time.deltaTime;
        if (timeSinceLastAttack >= AttackDelay)
        {
            timeSinceLastAttack = 0;
            GenerateGold();
            var circleView = view as CircleView;
            circleView.PlayAttackAnimation();
        }

        view.SetUpgradeAvailableState(availableGold >= NextUpgradeCost);
    }

    public void IncreaseInitialCost()
    {
        if (UpgradeLevel > 0)
            return;

        initialCost *= InitialCostMultiplier;
        NextUpgradeCost = initialCost;
        UpdateViewText();
    }

    public override void UpgradeGoldGenerator()
    {
        if (GoldController.Instance.AvailableGold >= NextUpgradeCost)
        {
            PayUpgrade();

            if (UpgradeLevel == 0)
                ConfirmPurchase();
            else
                view.PlayUpgradeAnimation();

            IncreaseGoldGeneratorLevel();
            UpdateViewText();
        }
    }

    private void ConfirmPurchase()
    {
        var circleView = view as CircleView;
        circleView.PlayPurchaseAnimation();
        OnPurchased?.Invoke();
    }
}