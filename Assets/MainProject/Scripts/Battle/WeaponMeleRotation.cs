using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sinabro
{
    public class WeaponMeleRotation : WeaponBase
    {
        //----------------------------------------------
        // CreateWeapon
        //----------------------------------------------
        public override void CreateWeapon(Transform myTransform)
        {
            myTransform_ = myTransform;

            myTransform_.parent = GameManager.Instance.player_.transform;
            myTransform_.localPosition = Vector3.zero;

            //
            id_ = 0;
            damage_ = 4;
            count_ = 3;
            prefabId_ = 1;
            speed_ = 150.0f;

            for (int i = 0; i < count_; ++i)
            {
                Transform bullet;
                bullet = GameManager.Instance.poolManager_.GetObject(prefabId_).transform;
                bullet.parent = myTransform_;
                
                bullet.localPosition = Vector3.zero;
                bullet.localRotation = Quaternion.identity;

                Vector3 rotVec = Vector3.forward * 360 * i / count_;
                bullet.Rotate(rotVec);
                bullet.Translate(bullet.up * 1.5f, Space.World);

                bullet.GetComponent<Bullet>().Init(damage_, -100, Vector3.zero);
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