using UnityEngine;

public class WizardSpawnerScript : MonoBehaviour
{
    [SerializeField] private float spawnInterval = 15f;
    private float currentTime = 0f;

    [SerializeField] private SpriteRenderer map;
    [SerializeField] private GameObject wizardPrefab;
    [SerializeField] private Transform playerTransform;

    private float xMapSize;
    private float yMapSize;

    void Start()
    { 
        xMapSize = map.bounds.extents.x;
        yMapSize = map.bounds.extents.y;
    }
    private void FixedUpdate()
    {
        currentTime += Time.fixedDeltaTime;
        if (currentTime > spawnInterval)
        {
            float angle = Random.Range(0f, 2f * Mathf.PI);
            float r = Mathf.Sqrt(Random.value);
            Vector2 spawnPosition = new Vector2(Mathf.Cos(angle) * r * xMapSize, Mathf.Sin(angle) * r * yMapSize);
            GameObject wizardObj = Instantiate(wizardPrefab, spawnPosition, transform.rotation);
            wizardObj.GetComponent<WizzardScript>().Init(playerTransform);
            currentTime = 0f;
        }
    }
}
