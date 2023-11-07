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
        public Transform[] playerStartPositions_;
        public Transform[] enemyStartPositions_;
        public Transform focusPlayer_;

        //
        [Header("# Game Control")]
        public bool isLive_;
        public float gameTime_;
        public float maxGameTime_ = 20.0f;

        //
        [Header("# Player Info")]
        public int level_;
        public int kill_;
        public int exp_;
        public int[] nextExp_ = { 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 };

        //
        [Header("# UI")]
        public HpBarControl[] playerHpBars_;
        public HpBarControl[] enemyHpBars_;

        private void Awake()
        {
            Instance = this;

            gameTime_ = 0;
        }

        private void Start()
        {
            // test code
            for (int i = 0; i < DataMgr.Instance.g_shipDataExcel.Sheet1.Count; ++i)
            {
                MyShipData shipData = new MyShipData();
                shipData.shipInfo_ = DataMgr.Instance.g_shipDataExcel.Sheet1[i];
                DataMgr.Instance.myInfo_g.myShipList_.Add(shipData);
            }

            //
            isLive_ = true;

            //
            for (int i = 0; i < playerHpBars_.Length; ++i)
            {
                playerHpBars_[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < enemyHpBars_.Length; ++i)
            {
                enemyHpBars_[i].gameObject.SetActive(false);
            }

            //
            GameObject playerInst = ObjectMgr.Instance.GetPlayer();
            if (playerInst != null)
            {
                playerInst.transform.position = playerStartPositions_[0].position;

                focusPlayer_ = playerInst.transform;

                MyShipData shipData = DataMgr.Instance.myInfo_g.GetMyShip(2);
                if (shipData != null)
                {
                    playerHpBars_[0].gameObject.SetActive(true);
                    playerHpBars_[0].SetUI(playerInst.transform);

                    playerInst.GetComponent<Player>().CreateUnit(shipData, playerHpBars_[0]);
                }
            }

            //
            GameObject enemy = ObjectMgr.Instance.GetEnemy();
            if (enemy != null)
            {
                enemy.transform.position = enemyStartPositions_[0].position;
                Enemy enemyInst = enemy.GetComponent<Enemy>();
                if (enemyInst != null)
                {
                    EnemyShipEntity enemyInfo = DataMgr.Instance.GetEnemyShipInfo(1);
                    if (enemyInfo != null)
                    {
                        enemyHpBars_[0].gameObject.SetActive(true);
                        enemyHpBars_[0].SetUI(enemyInst.transform);

                        enemyInst.CleateUnit(enemyInfo, enemyHpBars_[0]);
                    }
                }
            }
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