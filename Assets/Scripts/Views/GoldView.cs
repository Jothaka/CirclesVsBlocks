using UnityEngine;
using UnityEngine.UI;

public class GoldView : MonoBehaviour
{
    [SerializeField]
    private Text GoldText;

    public void SetGoldText(string goldString)
    {
        GoldText.text = string.Format("Current Gold: {0}", goldString);
    }
}