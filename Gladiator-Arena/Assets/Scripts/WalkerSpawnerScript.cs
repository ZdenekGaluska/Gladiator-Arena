using UnityEngine;

public class WalkerSpawnerScript : MonoBehaviour
{
    public SpriteRenderer MapRenderer;
    private float arenaRadius;
    public float Interval = 0.2f;
    private float currentTime = 0f;
    public GameObject walkerPrefab;
    void Start()
    {
        arenaRadius = MapRenderer.bounds.extents.x;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > Interval)
        {
            currentTime = 0f;
            float angle = Random.Range(0, 2 * Mathf.PI);
            Vector2 spawnPosition = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * arenaRadius;
            GameObject walkerObj =  Instantiate(walkerPrefab, spawnPosition, Quaternion.identity);
            WalkerMovementScript walker = walkerObj.GetComponent<WalkerMovementScript>();      
            walker.Init(arenaRadius);
        }
    }
}
