using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrankMaskSpawner : MonoBehaviour
{
    private Vector2 lastPosition;
    public float trankDistance =.2f;
    public GameObject trankPrefab;
    public int objectPoolSize = 50;
    private ObjPool objectPool;
    private void Awake() {
        objectPool = GetComponent<ObjPool>();
    }
    private void Start() {
        lastPosition = transform.position;
        objectPool.Initialize(trankPrefab, objectPoolSize);
    }
    private void Update() {
        var distanceDriven = Vector2.Distance(transform.position, lastPosition);
        if (distanceDriven >= trankDistance) {
            lastPosition = transform.position;
            var tracks = objectPool.CreateObject();
            tracks.transform.position = transform.position;
            tracks.transform.rotation = transform.rotation;
        }
    }
}
