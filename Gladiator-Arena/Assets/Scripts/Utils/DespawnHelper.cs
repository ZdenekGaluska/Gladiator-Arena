using UnityEngine;

public static class DespawnHelper
{
    public static bool ShouldDespawn(Vector2 position)
    {
        return !ArenaBoundary.Instance.IsInside(position * 0.8f);
    }
    
    public static bool IsInside(Vector2 position)
    {
        return ArenaBoundary.Instance.IsInside(position);
    }
  
    public static bool IsInside(Vector2 position, float margin)
    {
        Vector2 shiftedPosition = position + position.normalized * margin;
        return ArenaBoundary.Instance.IsInside(shiftedPosition);
    }
}
