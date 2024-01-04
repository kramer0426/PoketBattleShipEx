using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sinabro
{
    public class Bullet : MonoBehaviour
    {
        //
        public double       damage_;
        public bool         bPlayer_;
        public AttackType   attackType_;

        private Rigidbody2D rigid_;
        private PooledObject pooledObject_;

        private float lifeTime_ = 5.0f;

        private void Awake()
        {
            rigid_ = GetComponent<Rigidbody2D>();
        }

        //
        public void Init(double damage, bool bPlayer, AttackType attackType, Vector3 dir)
        {
            CancelInvoke("DieBullet");

            pooledObject_ = GetComponent<PooledObject>();

            damage_ = damage;
            bPlayer_ = bPlayer;
            attackType_ = attackType;
            rigid_.velocity = dir * 15.0f;


            Invoke("DieBullet", lifeTime_);
        }

        //
        public void DieBullet()
        {
            CancelInvoke("DieBullet");

            //rigid_.velocity = Vector3.zero;
            pooledObject_.pool.ReturnObject(gameObject);
        }


    }
}