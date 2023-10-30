using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sinabro
{
    public class GameManager : MonoBehaviour
    {
        //
        [Header("# Game Object")]
        public static GameManager Instance;
        public PoolManager poolManager_;
        public Player player_;
        public LevelUp levelUpUI_;

        //
        [Header("# Game Control")]
        public bool isLive_;
        public float gameTime_;
        public float maxGameTime_ = 20.0f;

        //
        [Header("# Player Info")]
        public int health_;
        public int maxHealth_ = 100;
        public int level_;
        public int kill_;
        public int exp_;
        public int[] nextExp_ = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 };


        private void Awake()
        {
            Instance = this;

            gameTime_ = 0;
        }

        private void Start()
        {
            health_ = maxHealth_;

            //
            levelUpUI_.Select(0);

            isLive_ = true;
        }

        //
        private void Update()
        {
            if (isLive_ == false)
                return;

            gameTime_ += Time.deltaTime;
            if (gameTime_ > maxGameTime_)
            {
                gameTime_ = maxGameTime_;
            }
        }

        //
        public void GetExp()
        {
            exp_++;
            if (exp_ == nextExp_[level_])
            {
                level_++;
                exp_ = 0;
                levelUpUI_.Show();
            }
        }

        //
        public void Stop()
        {
            isLive_ = false;
            Time.timeScale = 0;
        }

        //
        public void Resume()
        {
            isLive_ = true;
            Time.timeScale = 1;
        }
    }
}