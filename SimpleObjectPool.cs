// this script is used to reuse object instead of instantiating and destroying
// allows to recycle objects and avoid allocations which will lead to garbage collection.
// it will be used for answer button.
using UnityEngine;
using System.Collections.Generic;

// A very simple object pooling class
public class SimpleObjectPool : MonoBehaviour
{
    // the prefab that this object pool returns instances of
    public GameObject prefab;
    // collection of currently inactive instances of the prefab
    private Stack<GameObject> inactiveInstances = new Stack<GameObject>();

    // Returns an instance of the prefab
    public GameObject GetObject()
    {
        GameObject spawnedGameObject;


        // if there is an inactive instance of the prefab ready to return, return that
        if (inactiveInstances.Count > 0)
        {
            // remove the instance from the collection of inactive instances
            spawnedGameObject = inactiveInstances.Pop();
        }
        // otherwise, create a new instance
        else
        {
            spawnedGameObject = (GameObject)GameObject.Instantiate(prefab);

            // add the PooledObject component to the prefab so we know it came from this pool
            PooledObject pooledObject = spawnedGameObject.AddComponent<PooledObject>();
            pooledObject.pool = this;
        }

        // enable the instance
        spawnedGameObject.transform.SetParent(null);
        spawnedGameObject.SetActive(true);

        // return a reference to the instance
        return spawnedGameObject;
    }

    // return an instance of the prefab to the pool
    public void ReturnObject(GameObject toReturn)
    {
        PooledObject pooledObject = toReturn.GetComponent<PooledObject>();

        if (pooledObject != null && pooledObject.pool == this) // if the instance came from this pool, return it to the pool
        {
            // make the instance a child of this and disable it.
            toReturn.transform.SetParent(transform);
            toReturn.SetActive(false);

            // add the instance to the colection of inactive instances.
            inactiveInstances.Push(toReturn);
        }
        else // otherwise just destroy it.
        {
            Debug.LogWarning(toReturn.name + "was returned to a pool it wasn't spawned from! Destroying.");
            Destroy(toReturn);
        }
    }
}

// a component that simply identifies the pool that a GameObject came from
public class PooledObject : MonoBehaviour
{
    public SimpleObjectPool Pool;
    internal SimpleObjectPool pool;
}

