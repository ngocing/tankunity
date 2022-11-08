using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    public BulletData bulletData;
    private float conquaredDistance = 0;
    private Vector2 startPosition;
    private Rigidbody2D rb;
    public UnityEvent OnHit = new UnityEvent();
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Initialize(BulletData bulletData){
        this.bulletData = bulletData;
        startPosition = transform.position;
        rb.velocity = transform.up * this.bulletData.speed;
    }
    private void Update() {
        conquaredDistance = Vector2.Distance(transform.position, startPosition);
        if(conquaredDistance >= bulletData.maxDistance)
        {
            DisableObject();
        }
    }

    private void DisableObject()
    {
        rb.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Colliderd" + other.name);
        OnHit?.Invoke();
        var damagable = other.GetComponent<Damagable>();
        if(damagable != null){
            damagable.Hit(bulletData.damage);
        }

        DisableObject();
    }
}
