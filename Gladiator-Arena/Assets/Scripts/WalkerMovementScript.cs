using UnityEngine;

public class WalkerMovementScript : MonoBehaviour
{
    public float speed = 1f;
    private Rigidbody2D _rb;
    private Vector2 _direction;
    private float _arenaRadius;

    private Vector2 _spawnPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector2 toCenter;
        _rb = GetComponent<Rigidbody2D>();
        
        toCenter = (Vector2.zero - _rb.position).normalized;
        float randomOffset = Random.Range(-Mathf.PI/4, Mathf.PI/4);
        _direction = RotateVector(toCenter, randomOffset);
        
        _spawnPoint = _rb.position;
    }

    public void Init(float arenaRadius)
    {
        _arenaRadius = arenaRadius;
    }

    static Vector2 RotateVector(Vector2 v, float angle)
    {
        float cos  = Mathf.Cos(angle);
        float sin  = Mathf.Sin(angle);
        return new Vector2(v.x * cos - v.y * sin,v.x * sin +  v.y * cos);
    }
    
    void FixedUpdate()
    {
        _rb.linearVelocity = _direction * speed;
        
        if(Vector2.Distance(_spawnPoint, _rb.position) > _arenaRadius * 2.2f)
        {
            Destroy(gameObject);
        }
    }
}
