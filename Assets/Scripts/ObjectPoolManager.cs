using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPoolManager : MonoBehaviour
{
    public static List<PooledObjectInfo> ObjectPools = new List<PooledObjectInfo>();

    public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation)
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
            spawnableObject = Instantiate(objectToSpawn, spawnPosition, spawnRotation);
        } else
        {
            spawnableObject.transform.position = spawnPosition;
            spawnableObject.transform.rotation = spawnRotation;
            pool.InactiveObjects.Remove(spawnableObject);
            spawnableObject.SetActive(true);
        }

        return spawnableObject;

        public static void ReturnObjectPool(GameObject obj)
        {
            string goName = obj.name.Substring(0, obj.name.Length - 7);

            PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == goName);

            if(pool == null)
            {
                Debug.LogWarning("Object tidak di dalam pool: " + obj.name);
            }

            else
            {
                obj.SetActive(false);
                pool.InactiveObjects.Add(obj);
            }
        }

    }
}

public class PooledObjectInfo
{
    public string LookupString;
    public List<GameObject> InactiveObjects = new List<GameObject>();
}
