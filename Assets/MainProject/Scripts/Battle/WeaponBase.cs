using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sinabro
{
    public class WeaponBase
    {
        // 
        public Transform myTransform_;
        public int id_;
        public double damage_;
        public bool bPlayer_;
        public AttackType attackType_;
        public float coolTime_;

        //----------------------------------------------
        // CreateWeapon
        //----------------------------------------------
        public virtual void CreateWeapon(Transform myTransform, bool bPlayer, AttackType attackType, GameObject owner) { }

        //----------------------------------------------
        // UpdateWeapon
        //----------------------------------------------
        public virtual void UpdateWeapon() { }

    }
}