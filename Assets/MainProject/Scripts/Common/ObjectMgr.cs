using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Assertions;

namespace Sinabro
{
    public class ObjectMgr : MonoBehaviour
    {
        //
        private static ObjectMgr instance = null;

        //
        public ObjectPool playerPools_;
        public ObjectPool enemyPools_;
        public ObjectPool[] bulletPools_;

        //-----------------------------------------------
        // Instance
        //-----------------------------------------------
        public static ObjectMgr Instance
        {
            get { return instance; }
        }

        //
        private void Awake()
        {
            //
            instance = this;

            //
            playerPools_.Initialize();
            enemyPools_.Initialize();
            bulletPools_[0].Initialize();
            bulletPools_[1].Initialize();
        }

        //
        public GameObject GetPlayer()
        {
            return playerPools_.GetObject();
        }

        //
        public GameObject GetEnemy()
        {
            return enemyPools_.GetObject();
        }

        //
        public GameObject GetBullet(int index)
        {
            return bulletPools_[index].GetObject();
        }

    }
}
