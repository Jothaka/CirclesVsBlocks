using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GoldView goldView;
    [SerializeField]
    private CircleView[] CircleViews;
    [SerializeField]
    private UpgradeView tapView;
    [SerializeField]
    private BlockView blockView;

    [Header("Settings")]
    [SerializeField]
    private ScaleData scaleData;

    private List<GoldGeneratorController> goldgeneratorControllers;

    private List<CircleController> circleControllers;
    private TapController tapController;

    private GoldController goldController;

    //TODO: Get scaleData from remote
    void Start()
    {
        goldController = GoldController.Instance;
        goldController.SetGoldView(goldView);
        goldgeneratorControllers = new List<GoldGeneratorController>();
        circleControllers = new List<CircleController>(CircleViews.Length);

        for (int i = 0; i < CircleViews.Length; i++)
        {
            CircleController controller = new CircleController(CircleViews[i], scaleData, goldController.ChangeGoldValue);
            controller.OnPurchased += OnCirclePurchased;
            circleControllers.Add(controller);
        }

        goldgeneratorControllers.AddRange(circleControllers);

        tapController = new TapController(tapView, blockView, scaleData, goldController.ChangeGoldValue);

        goldgeneratorControllers.Add(tapController);
    }

    void Update()
    {
        for (int i = 0; i < goldgeneratorControllers.Count; i++)
            goldgeneratorControllers[i].UpdateGoldgenerator(goldController.AvailableGold);
    }

    //Increase Initial Costs of all non-purchased Circles when a new circle is purchased
    private void OnCirclePurchased()
    {
        for (int i = 0; i < circleControllers.Count; i++)
            circleControllers[i].IncreaseInitialCost();
    }
}