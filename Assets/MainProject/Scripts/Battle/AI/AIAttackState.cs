using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sinabro
{
    public class AIAttackState : AIStateBase
    {

        //
        public override void Initialize(GameObject owner, bool bPlayer)
        {
            myTransform_ = owner.transform;
            bPlayer_ = bPlayer;
            if (bPlayer_)
            {
                player_ = owner.GetComponent<Player>();
                player_.rigidBody_.velocity = Vector2.zero;
            }
            else
            {
                enemy_ = owner.GetComponent<Enemy>();
                enemy_.rigid_.velocity = Vector2.zero;
            }
        }

        //
        public override void AIUpdate()
        {
            if (bPlayer_)
            {
                if (player_.fireScanner_.nearestTarget_ == null)
                {
                    player_.ChangeAIState(AIStateID.Chasing);
                }
                else
                {
                    if (player_.myWeapon_ != null)
                        player_.myWeapon_.UpdateWeapon();
                }
            }
            else
            {
                if (enemy_.fireScanner_.nearestTarget_ == null)
                {
                    enemy_.ChangeAIState(AIStateID.Chasing);
                }
                else
                {
                    if (enemy_.myWeapon_ != null)
                        enemy_.myWeapon_.UpdateWeapon();
                }
            }
        }
    }
}