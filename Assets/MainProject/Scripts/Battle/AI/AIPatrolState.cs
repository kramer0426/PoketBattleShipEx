using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sinabro
{
    public class AIPatrolState : AIStateBase
    {
        //
        private Vector2 originPos_;
        private Vector2 patrolPos_;
        private bool bToOriginPosition_;

        //
        public override void Initialize(GameObject owner, bool bPlayer)
        {
            myTransform_ = owner.transform;
            bPlayer_ = bPlayer;
            if (bPlayer_)
            {
                player_ = owner.GetComponent<Player>();
                originPos_.x = GameManager.Instance.playerStartPositions_[player_.shipData_.fleetIndex_].position.x;
                originPos_.y = GameManager.Instance.playerStartPositions_[player_.shipData_.fleetIndex_].position.y;

                patrolPos_.x = originPos_.x - Random.Range(8.0f, 12.0f);
                patrolPos_.y = originPos_.y + Random.Range(-2.0f, 2.0f);
            }
            else
            {
                enemy_ = owner.GetComponent<Enemy>();
                originPos_.x = GameManager.Instance.enemyStartPositions_[enemy_.fleetIndex_].position.x;
                originPos_.y = GameManager.Instance.enemyStartPositions_[enemy_.fleetIndex_].position.y;

                patrolPos_.x = originPos_.x + Random.Range(8.0f, 12.0f);
                patrolPos_.y = originPos_.y + Random.Range(-2.0f, 2.0f);
            }

            bToOriginPosition_ = false;
        }

        //
        public override void AIUpdate() 
        {
            if (bPlayer_)
            {
                //
                Vector2 dirVec = patrolPos_ - player_.rigidBody_.position;
                Vector2 moveVec = dirVec.normalized * player_.speed_ * Time.deltaTime;
                player_.rigidBody_.MovePosition(player_.rigidBody_.position + moveVec);

                if (moveVec.x != 0)
                {
                    player_.sprite_.flipX = moveVec.x > 0;
                }

                if (Vector2.Distance(patrolPos_, player_.rigidBody_.position) < 0.05f)
                {
                    player_.rigidBody_.velocity = Vector2.zero;

                    if (bToOriginPosition_)
                    {
                        bToOriginPosition_ = false;

                        patrolPos_.x = originPos_.x - Random.Range(8.0f, 12.0f);
                        patrolPos_.y = originPos_.y + Random.Range(-2.0f, 2.0f);
                    }
                    else
                    {
                        bToOriginPosition_ = true;
                        patrolPos_ = originPos_;
                    }
                }

                //
                if (player_.sightScanner_.nearestTarget_ != null)
                {
                    player_.ChangeAIState(AIStateID.Chasing);
                }
            }
            else
            {
                Vector2 dirVec = patrolPos_ - enemy_.rigid_.position;
                Vector2 moveVec = dirVec.normalized * (float)enemy_.shipStatusDatas_[(int)ShipStatus.MoveSpeed] * Time.deltaTime;
                enemy_.rigid_.MovePosition(enemy_.rigid_.position + moveVec);

                if (moveVec.x != 0)
                {
                    enemy_.sprite_.flipX = moveVec.x > 0;
                }

                if (Vector2.Distance(patrolPos_, enemy_.rigid_.position) < 0.05f)
                {
                    enemy_.rigid_.velocity = Vector2.zero;

                    if (bToOriginPosition_)
                    {
                        bToOriginPosition_ = false;

                        patrolPos_.x = originPos_.x + Random.Range(8.0f, 12.0f);
                        patrolPos_.y = originPos_.y + Random.Range(-2.0f, 2.0f);
                        
                    }
                    else
                    {
                        bToOriginPosition_ = true;
                        patrolPos_ = originPos_;
                    }
                }
            }


        }
    }
}