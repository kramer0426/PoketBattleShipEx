using UnityEngine;

namespace Sinabro
{
    public class AutoKillPooled : MonoBehaviour
    {
        public float            lifeTime_ = 2.0f;

        private PooledObject    pooledObject_;
        private float           accTime_;

        //
        private void OnEnable()
        {
            accTime_ = 0.0f;
        }

        //
        private void Start()
        {
            pooledObject_ = GetComponent<PooledObject>();
        }

        //
        private void FixedUpdate()
        {
            if (lifeTime_ < 0)
                return;

            accTime_ += Time.deltaTime;
            if (accTime_ >= lifeTime_)
            {
                pooledObject_.pool.ReturnObject(gameObject);
            }
        }
    }
}
