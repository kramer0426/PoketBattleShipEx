using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sinabro
{
    public class Scanner : MonoBehaviour
    {
        //
        public float            scanRange_;
        public LayerMask        targetLayer_;
        public RaycastHit2D[]   targets_;
        public Transform        nearestTarget_;

        //
        private void FixedUpdate()
        {
            targets_ = Physics2D.CircleCastAll(transform.position, scanRange_, Vector2.zero, 0, targetLayer_);
            nearestTarget_ = GetNearest();
        }

        //
        private Transform GetNearest()
        {
            Transform result = null;
            float diff = 100.0f;
            Vector3 myPos = transform.position;
            Vector3 targetPos;
            for (int i = 0; i < targets_.Length; ++i)
            {
                targetPos = targets_[i].transform.position;
                float curDiff = Vector3.Distance(myPos, targetPos);

                if (curDiff < diff)
                {
                    diff = curDiff;
                    result = targets_[i].transform;
                }
            }

            return result;
        }
    }
}