using UnityEngine;

public class ArenaBoundaryScript : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private float _xMapSize;
    private float _yMapSize;
    public float offset = 0.5f;
    
    public static ArenaBoundaryScript Instance;

    void Awake()
    {
        Instance = this;
        _xMapSize = spriteRenderer.bounds.extents.x;
        _yMapSize = spriteRenderer.bounds.extents.y;
    }
    
    public bool IsInside(Vector2 position)
    {
        return (position.x * position.x) / ((_xMapSize - offset) * (_xMapSize - offset))
            + (position.y * position.y) / ((_yMapSize - offset) * (_yMapSize - offset)) <= 1f;
    }

    public Vector2 ClampToArena(Vector2 position)
    {
        if (IsInside(position)) return position;

        float angle = Mathf.Atan2(position.y / _yMapSize, position.x / _xMapSize);
        return new Vector2(Mathf.Cos(angle) * (_xMapSize - offset), Mathf.Sin(angle) * (_yMapSize - offset));
    }
}
