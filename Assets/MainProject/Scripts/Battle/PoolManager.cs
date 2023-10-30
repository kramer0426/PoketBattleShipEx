using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sinabro
{
    public class PoolManager : MonoBehaviour
    {
        //
        public GameObject[] prefabs_;

        //
        private List<GameObject>[] pools_;

        private void Awake()
        {
            pools_ = new List<GameObject>[prefabs_.Length];
            for (int i = 0; i < pools_.Length; ++i)
            {
                pools_[i] = new List<GameObject>();
            }
        }

        //
        public GameObject GetObject(int index)
        {
            GameObject obj = null;

            for (int i = 0; i < pools_[index].Count; ++i)
            {
                if (pools_[index][i].activeSelf == false)
                {
                    obj = pools_[index][i];
                    obj.SetActive(true);
                    break;
                }
            }

            if (obj == null)
            {
                obj = Instantiate(prefabs_[index], transform);
                pools_[index].Add(obj);
            }

            return obj;
        }
    }
}