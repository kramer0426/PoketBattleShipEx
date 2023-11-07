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

        private void Awake()
        {
            rigid_ = GetComponent<Rigidbody2D>();
        }

        //
        public void Init(double damage, bool bPlayer, AttackType attackType, Vector3 dir)
        {
            pooledObject_ = GetComponent<PooledObject>();

            damage_ = damage;
            bPlayer_ = bPlayer;
            attackType_ = attackType;
            rigid_.velocity = dir * 15.0f;
            
        }

        //
        public void DieBullet()
        {
            //pooledObject_.pool.ReturnObject(gameObject);
        }

        //
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Area") == false)
                return;

            pooledObject_.pool.ReturnObject(gameObject);
        }
    }
}