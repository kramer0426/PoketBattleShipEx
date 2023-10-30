using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sinabro
{
    [System.Serializable]
    public class SpwanData
    {
        public int      spriteType_;
        public float    spawnTime_;
        public double   health_;
        public float    speed_;
    }


    public class Spawner : MonoBehaviour
    {
        //
        public Transform[] spawnPoint_;
        public SpwanData[] spawnDatas_;

        //
        private int level_;
        private float timer_;

        private void Awake()
        {
            spawnPoint_ = GetComponentsInChildren<Transform>();

            timer_ = 0.0f;
        }

        //
        void Update()
        {
            if (GameManager.Instance.isLive_ == false)
                return;

            timer_ += Time.deltaTime;
            level_ = Mathf.Min(Mathf.FloorToInt(GameManager.Instance.gameTime_ / 10.0f), spawnDatas_.Length - 1);

            if (timer_ > spawnDatas_[level_].spawnTime_)
            {
                timer_ = 0;
                Spwan();
            }
        }

        //
        private void Spwan()
        {
            GameObject enemy = GameManager.Instance.poolManager_.GetObject(0);
            if (enemy != null)
            {
                enemy.transform.position = spawnPoint_[Random.Range(1, spawnPoint_.Length)].position;
                Enemy enemyInst = enemy.GetComponent<Enemy>();
                if (enemyInst != null)
                {
                    enemyInst.Init(spawnDatas_[level_]);
                }
            }
        }
    }
}