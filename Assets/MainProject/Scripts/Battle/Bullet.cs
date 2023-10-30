using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sinabro
{
    public class Bullet : MonoBehaviour
    {
        //
        public double   damage_;
        public int      per_;

        private Rigidbody2D rigid_;


        private void Awake()
        {
            rigid_ = GetComponent<Rigidbody2D>();
        }

        //
        public void Init(double damage, int per, Vector3 dir)
        {
            damage_ = damage;
            per_ = per;

            if (per >= 0)
            {
                rigid_.velocity = dir * 15.0f;
            }
        }

        //
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy") == false || per_ == -100)
                return;

            per_--;

            if (per_ < 0)
            {
                rigid_.velocity = Vector2.zero;
                gameObject.SetActive(false);
            }
        }

        //
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Area") == false || per_ == -100)
                return;

            gameObject.SetActive(false);
        }
    }
}