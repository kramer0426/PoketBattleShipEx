using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sinabro
{
    public class WeaponMeleRotation : WeaponBase
    {
        //
        private float speed_;

        //----------------------------------------------
        // CreateWeapon
        //----------------------------------------------
        public override void CreateWeapon(Transform myTransform, bool bPlayer, AttackType attackType, GameObject owner)
        {
            myTransform_ = myTransform;

            myTransform_.parent = owner.transform;
            myTransform_.localPosition = Vector3.zero;

            //
            id_ = 0;
            damage_ = 4;
            bPlayer_ = bPlayer;
            attackType_ = attackType;
            speed_ = 150.0f;
            int count = 3;

            for (int i = 0; i < count; ++i)
            {
                Bullet bullet = ObjectMgr.Instance.GetBullet(0).GetComponent<Bullet>();
                if (bullet != null)
                {
                    Transform bulletTransform = bullet.transform;
                    bulletTransform.transform.parent = myTransform_;

                    bulletTransform.transform.localPosition = Vector3.zero;
                    bulletTransform.transform.localRotation = Quaternion.identity;

                    Vector3 rotVec = Vector3.forward * 360 * i / count;
                    bulletTransform.Rotate(rotVec);
                    bulletTransform.Translate(bulletTransform.up * 1.5f, Space.World);

                    bullet.Init(damage_, bPlayer_, attackType_, Vector3.zero);
                }
            }
        }

        //----------------------------------------------
        // UpdateWeapon
        //----------------------------------------------
        public override void UpdateWeapon() 
        {
            myTransform_.Rotate(Vector3.back * speed_ * Time.deltaTime);
        }
    }
}