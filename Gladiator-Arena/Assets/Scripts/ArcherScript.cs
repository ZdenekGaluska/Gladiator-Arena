using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArcherScript : MonoBehaviour
{
    public GameObject arrowPrefab;
    private Rigidbody2D rb;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private ArrowTrajectoryScript arrowTrajectoryScript;

    [SerializeField] private float minWalkDuration = 4f;
    [SerializeField] private float maxWalkDuration = 6f;

    [SerializeField] private float aimDuration = 1f;
    [SerializeField] private Vector2 shootDirection;
    
    [SerializeField] private float archerSpeed = 2f;
    [SerializeField] private float minDrift = 0.01f;
    [SerializeField] private float maxDrift = 0.025f;
    
    [SerializeField] private float minDriftStart = 0f;
    [SerializeField] private float maxDriftStart = 30f;
    
    [SerializeField] private float drift;
    [SerializeField] private bool driftSign;
    [SerializeField] private float driftStart;
    [SerializeField] private Vector2 currentDirection;

    public void Init(Transform player)
    {
        playerTransform = player;
    }
    
    void Start()
    {
        drift = Random.Range(minDrift, maxDrift);
        driftSign = Random.value < 0.5f ? true : false;
        driftStart = Random.Range(minDriftStart, maxDriftStart);
        currentDirection = (Vector2.zero - (Vector2)transform.position).normalized;
        
        if (driftSign)
        {
            drift = -drift;
            currentDirection = RotateVector(currentDirection, -driftStart * Mathf.Deg2Rad);
        }
        else
        {
            currentDirection = RotateVector(currentDirection, driftStart * Mathf.Deg2Rad);
        }

        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ArcherRutine());
    }

    private void FixedUpdate()
    {
        if (DespawnHelper.ShouldDespawn(transform.position ))
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator ArcherRutine()
    {
        while (true)
        {
            float walkDuration = Random.Range(minWalkDuration, maxWalkDuration);
            yield return StartCoroutine(Walk(walkDuration));
            rb.linearVelocity = Vector2.zero;
            
            if (!DespawnHelper.ShouldDespawn(transform.position * 1.25f))
            {
                Aim();
                yield return new WaitForSeconds(aimDuration);
                Shoot();
            }
        }
    }
    
    static Vector2 RotateVector(Vector2 v, float angle)
    {
        float cos = Mathf.Cos(angle);
        float sin = Mathf.Sin(angle);
        return new Vector2(v.x * cos - v.y * sin, v.x * sin + v.y * cos);
    }
    
    private IEnumerator Walk(float walkDuration)
    {
        float currentTime = 0f;
        while (currentTime <= walkDuration)
        {
            
            rb.linearVelocity = currentDirection * archerSpeed;
            currentTime += Time.deltaTime;
            currentDirection = RotateVector(currentDirection, drift * Time.deltaTime);
            yield return null;
        }
    }
    
    private void Aim()
    {
        arrowTrajectoryScript.DrawShootingTrajectory(transform.position, playerTransform.position);
        shootDirection = playerTransform.position - transform.position;
    }
    
    private void Shoot()
    {
        GameObject arrowObj = Instantiate(arrowPrefab, transform.position, transform.rotation);
        ArrowScript arrowScript = arrowObj.GetComponent<ArrowScript>();
        arrowScript.ShootArrow(shootDirection);        
        arrowTrajectoryScript.EraseShootingTrajectory();
    }
    
}

