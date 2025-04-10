using UnityEngine;

public static class ExtensionMethods
{
    public static float Remap(this float from, float fromMin, float fromMax, float toMin, float toMax)
    {
        float fromAbs = from - fromMin;
        float fromMaxAbs = fromMax - fromMin;
        float normal = fromAbs / fromMaxAbs;
        float toMaxAbs = toMax - toMin;
        float toAbs = toMaxAbs * normal;
        float to = toAbs + toMin;

        return to;
    }

    public static void Activate(this GameObject gameObject) => gameObject.SetActive(true);
    public static void Deactivate(this GameObject gameObject) => gameObject.SetActive(false);
}