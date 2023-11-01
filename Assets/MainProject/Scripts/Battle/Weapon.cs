using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sinabro
{
    public class Weapon : MonoBehaviour
    {
        //
        public int      id_;
        public int      prefabId_;
        public double   damage_;
        public int      count_;
        public float    speed_;


        //
        private float timer_ = 0;
        private Player player_;

        private void Awake()
        {
            player_ = GameManager.Instance.player_;
        }

        // Update is called once per frame
        private void Update()
        {
            if (GameManager.Instance.isLive_ == false)
                return;

            if (id_ == 0)
            {
                transform.Rotate(Vector3.back * speed_ * Time.deltaTime);
            }
            else if (id_ == 1)
            {
                timer_ += Time.deltaTime;
                if (timer_ > speed_)
                {
                    timer_ = 0;
                    Fire();
                }
            }
        }

        //
        public void Init()
        {
            //
            name = "Weapon";
            transform.parent = player_.transform;
            transform.localPosition = Vector3.zero;

            //
            id_ = 0;
            damage_ = 4;
            count_ = 3;
            prefabId_ = 1;


            //
            if (id_ == 0)
            {
                speed_ = 150.0f;
                Create();
            }
            else if (id_ == 1)
            {
                speed_ = 0.3f;
            }
        }

        //
        public void LevelUp(double damage, int count)
        {
            damage_ = damage;
            count_ += count;

            if (id_ == 0)
            {
                Create();
            }
            else if (id_ == 1)
            {

            }


            //
            player_.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
        }

        //
        private void Create()
        {
            for (int i = 0; i < count_; ++i)
            {
                Transform bullet;
                if (i < transform.childCount)
                {
                    bullet = transform.GetChild(i);
                }
                else
                {
                    bullet = GameManager.Instance.poolManager_.GetObject(prefabId_).transform;
                    bullet.parent = transform;
                }

                bullet.localPosition = Vector3.zero;
                bullet.localRotation = Quaternion.identity;

                Vector3 rotVec = Vector3.forward * 360 * i / count_;
                bullet.Rotate(rotVec);
                bullet.Translate(bullet.up * 1.5f, Space.World);

                bullet.GetComponent<Bullet>().Init(damage_, -100, Vector3.zero);
            }
        }

        //
        private void Fire()
        {
            if (player_.scanner_.nearestTarget_ == null)
                return;

            Vector3 targetPos = player_.scanner_.nearestTarget_.position;
            Vector3 dir = targetPos - transform.position;
            dir = dir.normalized;

            Transform bullet = GameManager.Instance.poolManager_.GetObject(prefabId_).transform;
            bullet.position = transform.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
            bullet.GetComponent<Bullet>().Init(damage_, count_, dir);
        }
    }
}