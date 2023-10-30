using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sinabro
{
    public class Gear : MonoBehaviour
    {
        //
        public ItemData.ItemType type_;
        public float rate_;

        //
        public void Init(ItemData data)
        {
            //
            name = "Gear" + data.itemId_;
            transform.parent = GameManager.Instance.player_.transform;
            transform.localPosition = Vector3.zero;

            //
            type_ = data.itemType_;
            rate_ = data.damages_[0];

            //
            ApplyGear();
        }

        //
        public void LevelUp(float rate)
        {
            rate_ = rate;

            //
            ApplyGear();
        }

        //
        private void ApplyGear()
        {
            if (type_ == ItemData.ItemType.Glove)
            {
                RateUp();
            }
            else if (type_ == ItemData.ItemType.shoe)
            {
                SpeedUp();
            }
        }

        //
        private void RateUp()
        {
            Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();
            foreach (Weapon weapon in weapons)
            {
                if (weapon.id_ == 0)
                {
                    weapon.speed_ = 150 + (150 * rate_);
                }
                else
                {
                    weapon.speed_ = 0.5f * (1.0f - rate_);
                }
            }
        }

        //
        private void SpeedUp()
        {
            float speed = 3;
            GameManager.Instance.player_.speed_ = speed + speed * rate_;
        }
    }
}