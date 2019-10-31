public static class Extensions
{
    private const int ScientificFormatBorder = 999999;

    public static string FormatForUI(this double valueToFormat)
    {
        if (valueToFormat > ScientificFormatBorder)
            return valueToFormat.ToString("0.00E+0");
        else
            return valueToFormat.ToString("N1");
    }
}