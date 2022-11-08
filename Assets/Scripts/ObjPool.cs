using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPool : MonoBehaviour
{
    [SerializeField]
    protected GameObject objToPool;
    [SerializeField]
    protected int poolSize = 10;
    protected Queue<GameObject> objPool;
    public Transform spawnedObjectsParent;
    public bool alwaysDestroy = false;
    private void Awake() {
        objPool = new Queue<GameObject>();
    }
    public void Initialize(GameObject objToPool, int poolSize = 10){
        this.objToPool = objToPool;
        this.poolSize = poolSize;
    }
    public GameObject CreateObject(){
        CreateObjectParentIfNeeded();
        GameObject spawnedObject = null;
        if(objPool.Count < poolSize){
            spawnedObject = Instantiate(objToPool, transform.position, Quaternion.identity);
            spawnedObject.name = transform.root.name + "_" + objToPool.name + "_" + objPool.Count;
            spawnedObject.transform.SetParent(spawnedObjectsParent);
            spawnedObject.AddComponent<DestroyIfDisable>();
        }else{
            spawnedObject = objPool.Dequeue();
            spawnedObject.transform.position = transform.position;
            spawnedObject.transform.rotation = Quaternion.identity;
            spawnedObject.SetActive(true);
        }
        objPool.Enqueue(spawnedObject);
        return spawnedObject;
    }

    private void CreateObjectParentIfNeeded()
    {
        if(spawnedObjectsParent == null){
            string name = "ObjPool_" + objToPool.name;
            var parentObject = GameObject.Find(name);
            if(parentObject != null)
                spawnedObjectsParent = parentObject.transform;
            else{
                spawnedObjectsParent = new GameObject(name).transform;
            }
        }
    }
    private void OnDestroy(){
        foreach(var item in objPool){
            if (item == null)
                continue;
            else if (item.activeSelf == false || alwaysDestroy)
                Destroy(item);
            else
                item.GetComponent<DestroyIfDisable>().SelfDestructionEnabled = true;
        }
    }
}
