using UnityEngine;

public class CrowdThrowScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer MapRenderer;
    private float _arenaSizeX;
    private float _arenaSizeY;
    [SerializeField] private float interval = 12f;
    private float _currentTime = 0f;
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform playerTransform;

    [SerializeField] private float maxOffsetAcc = 2f;
    
    private Vector2 spawnPosition;

    void Start()
    {
        _arenaSizeX = MapRenderer.bounds.extents.x + 2f;
        _arenaSizeY = MapRenderer.bounds.extents.y + 2f;
        
    }


    void FixedUpdate()
    {
     _currentTime += Time.deltaTime;
     if (_currentTime > interval)
     {
         _currentTime = 0f;

         float angle = Random.Range(0f, Mathf.PI * 2f);
         spawnPosition = new Vector2(Mathf.Cos(angle) * _arenaSizeX, Mathf.Sin(angle)  * _arenaSizeY);
         
         float angleAcc = Random.Range(0f, Mathf.PI * 2f);
         float offset =  Random.Range(0f, maxOffsetAcc);
         Vector2 target = (Vector2)playerTransform.position + new Vector2(Mathf.Cos(angleAcc) * offset, Mathf.Sin(angleAcc) * offset);
             
        GameObject fireballObj =  Instantiate(fireballPrefab, spawnPosition, Quaternion.identity);
        BurningAreaScript fireballScript = fireballObj.GetComponent<BurningAreaScript>();
        fireballScript.ShootFireball(spawnPosition, target);
     }
    }
}
