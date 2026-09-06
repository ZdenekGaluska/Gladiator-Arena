using System;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public float arrowSpeed = 6f;
    private Rigidbody2D _rb;
    

    public void ShootArrow(Vector2 dir)
    {
        _rb = GetComponent<Rigidbody2D>();
        dir = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        _rb.linearVelocity = dir * arrowSpeed;
    }


    private void FixedUpdate()
    {
        if (DespawnHelper.ShouldDespawn(transform.position))
        {
            Destroy(gameObject);
        }
    }
}
