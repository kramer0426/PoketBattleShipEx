using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sinabro
{
    //
    public enum AIStateID
    {
        Patrolling = 0,
        Chasing,
        Attacking,
        Defensing,
        Dead
    }

    //
    public enum AIType
    {
        Normal = 0,
        Offensive,
        Defensive
    }

    //
    public class AIStateBase
    {
        //
        public Transform myTransform_;
        public Player player_;
        public Enemy enemy_;
        public bool bPlayer_;

        //
        public virtual void Initialize(GameObject owner, bool bPlayer) 
        {
            myTransform_ = owner.transform;
            bPlayer_ = bPlayer;
            if (bPlayer_)
            {
                player_ = owner.GetComponent<Player>();
            }
            else
            {
                enemy_ = owner.GetComponent<Enemy>();
            }
        }

        //
        public virtual void AIUpdate() { }
    }
}