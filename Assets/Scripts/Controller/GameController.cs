using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private double availableGold;

    [Header("References")]
    [SerializeField]
    private GoldView goldView;
    [SerializeField]
    private CircleView[] CircleViews;
    [SerializeField]
    private TapView tapView;

    [Header("Settings")]
    [SerializeField]
    private ScaleData scaleData;

    private List<GoldGeneratorController> goldgeneratorControllers;

    private List<CircleController> circleControllers;
    private TapController tapController;

    //TODO: Get scaleData from remote
    void Start()
    {
        goldgeneratorControllers = new List<GoldGeneratorController>();
        circleControllers = new List<CircleController>(CircleViews.Length);
        
        for (int i = 0; i < CircleViews.Length; i++)
        {
            CircleController controller = new CircleController(CircleViews[i], scaleData, OnGoldGenerated);
            controller.OnPurchased += OnCirclePurchased;
            circleControllers.Add(controller);
        }

        goldgeneratorControllers.AddRange(circleControllers);

        tapController = new TapController(tapView, scaleData, OnGoldGenerated);

        goldgeneratorControllers.Add(tapController);
    }

    void Update()
    {
        for (int i = 0; i < goldgeneratorControllers.Count; i++)
            goldgeneratorControllers[i].UpdateGoldgenerator(availableGold);
    }

    //Triggered by UI OnClick-Callback
    public void OnBlockTapped()
    {
        tapController.OnBlockTapped();
    }

    //Triggered by UI OnClick-Callback
    public void OnCircleUpgradeTapped(int circleID)
    {
        UpgradeController(circleControllers[circleID]);
    }

    //Triggered by UI OnClick-Callback
    public void OnTapUpgradeTapped()
    {
        UpgradeController(tapController);
    }

    private void UpgradeController(GoldGeneratorController controller)
    {
        if (availableGold >= controller.NextUpgradeCost)
        {
            availableGold -= controller.NextUpgradeCost;
            goldView.SetGoldText(availableGold.FormatForUI());
            controller.UpgradeGoldGenerator();
        }
    }

    private void OnGoldGenerated(double goldGained)
    {
        availableGold += goldGained;
        goldView.SetGoldText(availableGold.FormatForUI());
    }

    private void OnCirclePurchased()
    {
        for (int i = 0; i < circleControllers.Count; i++)
            circleControllers[i].IncreaseInitialCost();
    }
}