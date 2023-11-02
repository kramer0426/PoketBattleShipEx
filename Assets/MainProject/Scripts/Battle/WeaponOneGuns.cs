using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sinabro
{
    public class WeaponOneGuns : WeaponBase
    {
        //
        private float timer_ = 0;
        private Player player_ = null;

        //----------------------------------------------
        // CreateWeapon
        //----------------------------------------------
        public override void CreateWeapon(Transform myTransform)
        {
            myTransform_ = myTransform;

            player_ = GameManager.Instance.player_;
            myTransform_.parent = player_.transform;
            myTransform_.localPosition = Vector3.zero;

            //
            id_ = 1;
            damage_ = 4;
            count_ = 3;
            prefabId_ = 2;
            speed_ = 0.3f;

        }

        //----------------------------------------------
        // UpdateWeapon
        //----------------------------------------------
        public override void UpdateWeapon()
        {
            timer_ += Time.deltaTime;
            if (timer_ > speed_)
            {
                timer_ = 0;
                Fire();
            }
        }

        //
        private void Fire()
        {
            if (player_.scanner_.nearestTarget_ == null)
                return;

            Vector3 targetPos = player_.scanner_.nearestTarget_.position;
            Vector3 dir = targetPos - myTransform_.position;
            dir = dir.normalized;

            Transform bullet = GameManager.Instance.poolManager_.GetObject(prefabId_).transform;
            bullet.position = myTransform_.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
            bullet.GetComponent<Bullet>().Init(damage_, count_, dir);
        }
    }
}