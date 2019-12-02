public class GoldController 
{
    public static GoldController Instance
    {
        get
        {
            if (instance == null)
                instance = new GoldController();
            return instance;
        }
    }

    private static GoldController instance;

    private GoldView goldView;

    public double AvailableGold { get; private set; }

    public void ChangeGoldValue(double changeValue)
    {
        AvailableGold += changeValue;
        goldView.SetGoldText(AvailableGold.FormatForUI());
    }

    public void SetGoldView(GoldView view)
    {
        goldView = view;
        goldView.SetGoldText(AvailableGold.FormatForUI());
    }
}