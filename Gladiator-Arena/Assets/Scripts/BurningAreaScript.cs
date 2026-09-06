using System.Collections;
using UnityEngine;
public class BurningAreaScript : MonoBehaviour
{
    [SerializeField] private float MinThrowDuration = 2f;
    [SerializeField] private float throwSpeed = 5f; 
    [SerializeField] private float explosionGrowDuration = 0.5f;
    [SerializeField] private float AreaSize = 2.5f;
    
    [SerializeField] private Transform warningVisualTransform;
    [SerializeField] private Transform fireballTransform;
    [SerializeField] private GameObject activeVisual;
    
    [SerializeField] private float arcFactor;
    [SerializeField] private float minArcHeight = 0.3f;

    [SerializeField] private float minFireballScale = 0.5f;
    [SerializeField] private float maxFireballScale = 1.2f;

    [SerializeField] private float burningTime = 5f;
    
    void Init(Vector2 from, Vector2 to)
    {
        activeVisual.SetActive(false);
        warningVisualTransform.gameObject.SetActive(true);
        fireballTransform.gameObject.SetActive(true);
        
        StartCoroutine(ThrowRoutine(from, to));
    }
    
    
    private IEnumerator ThrowRoutine(Vector2 from, Vector2 to)
    {
        float distance = Vector2.Distance(from, to);
        float duration = Mathf.Max(MinThrowDuration, distance / throwSpeed);
        
        float elapsedTime = 0f;
        float dx = Mathf.Abs(to.x - from.x);
        float arcHeight = Mathf.Max(minArcHeight, Mathf.Sqrt(dx) * arcFactor);
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            
            Vector2 groundPos = Vector2.Lerp(from, to, t);
            float height = arcHeight * 4f * t * (1f - t);
            fireballTransform.position = groundPos + new Vector2(0, height);

            float heightNormalized = height / arcHeight;
            float scale = Mathf.Lerp(minFireballScale, maxFireballScale, heightNormalized);
            fireballTransform.localScale = Vector3.one * scale;
            
            elapsedTime += Time.deltaTime;
            warningVisualTransform.localScale = t * AreaSize * Vector3.one;
            yield return null;
        }
        warningVisualTransform.gameObject.SetActive(false);
        fireballTransform.gameObject.SetActive(false);
        activeVisual.SetActive(true);
        activeVisual.transform.localScale = Vector3.zero;
        float explosionElapsed = 0f;    
        while (explosionElapsed < explosionGrowDuration)
        {
            float explosionT = explosionElapsed / explosionGrowDuration;
            activeVisual.transform.localScale = explosionT * AreaSize * Vector3.one;
            explosionElapsed += Time.deltaTime;
            yield return null;
        }
        activeVisual.transform.localScale = AreaSize * Vector3.one;
        
        yield return new WaitForSeconds(burningTime);
        Destroy(gameObject);
    }
    public void ShootFireball(Vector2 from, Vector2 to)
    {
        warningVisualTransform.transform.position = to;
        activeVisual.transform.position = to;
        Init(from, to);
    }
}