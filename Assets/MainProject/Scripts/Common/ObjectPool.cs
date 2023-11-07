using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Assertions;

namespace Sinabro
{
    public class ObjectPool : MonoBehaviour
    {
        public GameObject   prefab_;
        public int          initialSize_;

        private readonly Stack<GameObject> instances_ = new Stack<GameObject>();

        //
        private void Awake()
        {
            Assert.IsNotNull(prefab_);
        }

        //------------------------------------------------------------------------------------------
        // Initialize
        //------------------------------------------------------------------------------------------
        public void Initialize()
        {
            for (var i = 0; i < initialSize_; i++)
            {
                GameObject obj = CreateInstance();
                obj.SetActive(false);
                instances_.Push(obj);
            }
        }

        //------------------------------------------------------------------------------------------
        // GetObject
        //------------------------------------------------------------------------------------------
        public GameObject GetObject()
        {
            GameObject obj = instances_.Count > 0 ? instances_.Pop() : CreateInstance();
            obj.SetActive(true);
            return obj;
        }

        //------------------------------------------------------------------------------------------
        // ReturnObject
        //------------------------------------------------------------------------------------------
        public void ReturnObject(GameObject obj)
        {
            PooledObject pooledObject = obj.GetComponent<PooledObject>();
            Assert.IsNotNull(pooledObject);
            Assert.IsTrue(pooledObject.pool == this);

            obj.transform.SetParent(transform);
            obj.SetActive(false);

            if (!instances_.Contains(obj))
            {
                instances_.Push(obj);
            }
        }

        //------------------------------------------------------------------------------------------
        // Reset
        //------------------------------------------------------------------------------------------
        public void Reset()
        {
            List<GameObject> objectsToReturn = new List<GameObject>();
            foreach (PooledObject instance in transform.GetComponentsInChildren<PooledObject>())
            {
                if (instance.gameObject.activeSelf)
                {
                    objectsToReturn.Add(instance.gameObject);
                }
            }
            foreach (GameObject instance in objectsToReturn)
            {
                ReturnObject(instance);
            }
        }

        //------------------------------------------------------------------------------------------
        // Reset
        //------------------------------------------------------------------------------------------
        private GameObject CreateInstance()
        {
            GameObject obj = Instantiate(prefab_);
            PooledObject pooledObject = obj.AddComponent<PooledObject>();
            pooledObject.pool = this;
            obj.transform.SetParent(transform);
            return obj;
        }
    }

    //------------------------------------------------------------------------------------------
    // Utility class to identify the pool of a pooled object.
    //------------------------------------------------------------------------------------------
    public class PooledObject : MonoBehaviour
    {
        public ObjectPool pool;
    }
}
