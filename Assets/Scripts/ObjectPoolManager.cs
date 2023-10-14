using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPoolManager : MonoBehaviour
{
    public static List<PooledObjectInfo> ObjectPools = new List<PooledObjectInfo>();

    private GameObject _objectPoolEmptyHolder;

    private static GameObject _particleSystemsEmpty;
    private static GameObject _gameObjectsEmpty;


    public enum PoolType
    {
        Gameobject,
        ParticleSystem,
        None
    }

    public static PoolType PoolingType;

    private void Awake()
    {
        SetupEmpties();
    }

    private void SetupEmpties()
    {
        _objectPoolEmptyHolder = new GameObject("Pooled Objects");

        _particleSystemsEmpty = new GameObject("Particle Systems");
        _particleSystemsEmpty.transform.parent = _objectPoolEmptyHolder.transform;

        _gameObjectsEmpty = new GameObject("Game Objects");
        _gameObjectsEmpty.transform.parent = _objectPoolEmptyHolder.transform;
    }

    public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation, PoolType poolType = PoolType.None)
    {
        PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == objectToSpawn.name);

        if(pool == null)
        {
            pool = new PooledObjectInfo() { LookupString = objectToSpawn.name };
            ObjectPools.Add(pool);
        }

        GameObject spawnableObject = pool.InactiveObjects.FirstOrDefault();

        /*GameObject spawnableObject = null;
        foreach(GameObject obj in pool.InactiveObjects)
        {
            if(obj != null)
            {
                spawnableObject = obj;
                break;
            }
        }*/

        if(spawnableObject == null)
        {
            GameObject parentObject = SetParentObject(poolType);

            spawnableObject = Instantiate(objectToSpawn, spawnPosition, spawnRotation);

            if(parentObject != null)
            {
                spawnableObject.transform.SetParent(parentObject.transform);
            }
        } else
        {
            spawnableObject.transform.position = spawnPosition;
            spawnableObject.transform.rotation = spawnRotation;
            pool.InactiveObjects.Remove(spawnableObject);
            spawnableObject.SetActive(true);
        }

        return spawnableObject;
        

    }

    public static void ReturnObjectPool(GameObject obj)
    {
        string goName = obj.name.Substring(0, obj.name.Length - 7);

        PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == goName);

        if (pool == null)
        {
            Debug.LogWarning("Object tidak di dalam pool: " + obj.name);
        }

        else
        {
            obj.SetActive(false);
            pool.InactiveObjects.Add(obj);
        }
    }

    private static GameObject SetParentObject(PoolType poolType)
    {
        switch(poolType)
        {
            case PoolType.Gameobject:
                return _gameObjectsEmpty;
            case PoolType.ParticleSystem:
                return _particleSystemsEmpty;
            case PoolType.None:
                return null;
            default:
                return null;
        }
    }
}

public class PooledObjectInfo
{
    public string LookupString;
    public List<GameObject> InactiveObjects = new List<GameObject>();
}
