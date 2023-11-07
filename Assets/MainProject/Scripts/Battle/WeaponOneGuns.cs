using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sinabro
{
    public class WeaponOneGuns : WeaponBase
    {
        //
        private float timer_ = 0;
        private Scanner scanner_;

        //----------------------------------------------
        // CreateWeapon
        //----------------------------------------------
        public override void CreateWeapon(Transform myTransform, bool bPlayer, AttackType attackType, GameObject owner)
        {
            myTransform_ = myTransform;
            myTransform_.parent = owner.transform;
            myTransform_.localPosition = Vector3.zero;

            scanner_ = owner.GetComponent<Scanner>();

            //
            id_ = 1;
            damage_ = 4;
            bPlayer_ = bPlayer;
            attackType_ = attackType;
            coolTime_ = 1.0f;

        }

        //----------------------------------------------
        // UpdateWeapon
        //----------------------------------------------
        public override void UpdateWeapon()
        {
            timer_ += Time.deltaTime;
            if (timer_ > coolTime_)
            {
                timer_ = 0;
                Fire();
            }
        }

        //
        private void Fire()
        {
            if (scanner_ == null)
                return;

            if (scanner_.nearestTarget_ == null)
                return;

            Vector3 targetPos = scanner_.nearestTarget_.position;
            Vector3 dir = targetPos - myTransform_.position;
            dir = dir.normalized;

            GameObject bulletObj = ObjectMgr.Instance.GetBullet();
            Bullet bullet = bulletObj.GetComponent<Bullet>();
            if (bullet != null)
            {
                Transform bulletTransform = bullet.transform;
                bulletTransform.position = myTransform_.position;
                bulletTransform.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
                bullet.Init(damage_, bPlayer_, attackType_, dir);
            }
        }
    }
}