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
        public int prefabId_;
        public double damage_;
        public int count_;
        public float speed_;

        //----------------------------------------------
        // CreateWeapon
        //----------------------------------------------
        public virtual void CreateWeapon(Transform myTransform) { }

        //----------------------------------------------
        // UpdateWeapon
        //----------------------------------------------
        public virtual void UpdateWeapon() { }

    }
}