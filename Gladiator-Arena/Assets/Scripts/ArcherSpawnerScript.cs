using UnityEngine;

public class ArcherSpawnerScript : MonoBehaviour
{
    public SpriteRenderer MapRenderer;
    private float arenaRadius;
    public float interval = 5f;
    private float currentTime = 0f;
    public GameObject archerPrefab;
    public Transform playerTransform;

    void Start()
    {
        arenaRadius = MapRenderer.bounds.extents.x;
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > interval)
        {
            currentTime = 0f;
            float angle = Random.Range(0f, 2f * Mathf.PI);
            Vector2 spawnPosition = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * arenaRadius;
            GameObject archerObj = Instantiate(archerPrefab, spawnPosition, Quaternion.identity);
            archerObj.GetComponent<ArcherScript>().Init(playerTransform);
        }
    }
}
