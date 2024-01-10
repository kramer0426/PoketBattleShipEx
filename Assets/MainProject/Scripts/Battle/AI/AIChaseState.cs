using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sinabro
{
    public class AIChaseState : AIStateBase
    {
        //
        private Transform target_;

        //
        public override void AIUpdate()
        {
            if (bPlayer_)
            {
                target_ = player_.sightScanner_.nearestTarget_;
            }
            else
            {
                target_ = enemy_.sightScanner_.nearestTarget_;
            }

            if (target_ == null)
            {
                if (bPlayer_)
                {
                    player_.ChangeAIState(AIStateID.Patrolling);
                }
                else
                {
                    enemy_.ChangeAIState(AIStateID.Patrolling);
                }
            }
            else
            {
                if (bPlayer_)
                {
                    Vector2 targetPos = target_.position;
                    Vector2 dirVec = targetPos - player_.rigidBody_.position;
                    Vector2 moveVec = dirVec.normalized * player_.speed_ * Time.deltaTime;
                    player_.rigidBody_.MovePosition(player_.rigidBody_.position + moveVec);

                    if (moveVec.x != 0)
                    {
                        player_.sprite_.flipX = moveVec.x > 0;
                    }

                    if (player_.fireScanner_.nearestTarget_ != null)
                    {
                        player_.ChangeAIState(AIStateID.Attacking);
                    }
                }
                else
                {
                    Vector2 targetPos = target_.position;
                    Vector2 dirVec = targetPos - enemy_.rigid_.position;
                    Vector2 moveVec = dirVec.normalized * (float)enemy_.shipStatusDatas_[(int)ShipStatus.MoveSpeed] * Time.deltaTime;
                    enemy_.rigid_.MovePosition(enemy_.rigid_.position + moveVec);

                    if (moveVec.x != 0)
                    {
                        enemy_.sprite_.flipX = moveVec.x > 0;
                    }

                    if (enemy_.fireScanner_.nearestTarget_ != null)
                    {
                        enemy_.ChangeAIState(AIStateID.Attacking);
                    }
                }
            }
        }
    }
}