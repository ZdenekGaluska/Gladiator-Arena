using UnityEngine;

public static class MathHelper
{
    public static Vector2 RotateVector(Vector2 v, float angle)
    {
        float cos = Mathf.Cos(angle);
        float sin = Mathf.Sin(angle);
        return new Vector2(v.x * cos - v.y * sin, v.x * sin + v.y * cos);
    }
}
