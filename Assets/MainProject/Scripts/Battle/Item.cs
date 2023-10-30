using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sinabro
{
    public class Item : MonoBehaviour
    {
        public ItemData data_;
        public int level_;
        public Weapon weapon_;
        public Gear gear_;

        private Image icon_;
        private Text levelText_;
        private Text nameText_;
        private Text descText_;

        private void Awake()
        {
            icon_ = GetComponentsInChildren<Image>()[1];
            icon_.sprite = data_.itemIcon_;

            Text[] texts = GetComponentsInChildren<Text>();
            levelText_ = texts[0];
            nameText_ = texts[1];
            descText_ = texts[2];

            nameText_.text = data_.itemName_;
        }

        private void OnEnable()
        {
            levelText_.text = "Lv." + (level_ + 1);

            if (data_.itemType_ == ItemData.ItemType.Melee || data_.itemType_ == ItemData.ItemType.Range)
            {
                descText_.text = string.Format(data_.itemDesc_, data_.damages_[level_] * 100, data_.counts_[level_]);
            }
            else if (data_.itemType_ == ItemData.ItemType.Glove || data_.itemType_ == ItemData.ItemType.shoe)
            {
                descText_.text = string.Format(data_.itemDesc_, data_.damages_[level_] * 100);
            }
            else
            {
                descText_.text = string.Format(data_.itemDesc_);
            }

        }



        public void OnClick()
        {
            if (data_.itemType_ == ItemData.ItemType.Melee || data_.itemType_ == ItemData.ItemType.Range)
            {
                if (level_ == 0)
                {
                    GameObject newWeapon = new GameObject();
                    weapon_ = newWeapon.AddComponent<Weapon>();
                    weapon_.Init(data_);
                }
                else
                {
                    float nextDamage = data_.baseDamage_;
                    int nextCount = 0;

                    nextDamage += data_.baseDamage_ * data_.damages_[level_];
                    nextCount += data_.counts_[level_];

                    weapon_.LevelUp(nextDamage, nextCount);
                }

                level_++;
            }
            else if (data_.itemType_ == ItemData.ItemType.Glove || data_.itemType_ == ItemData.ItemType.shoe)
            {
                if (level_ == 0)
                {
                    GameObject newGear = new GameObject();
                    gear_ = newGear.AddComponent<Gear>();
                    gear_.Init(data_);
                }
                else
                {
                    float nextRate = data_.damages_[level_];
                    gear_.LevelUp(nextRate);
                }

                level_++;
            }
            else if (data_.itemType_ == ItemData.ItemType.Heal)
            {
                GameManager.Instance.health_ = GameManager.Instance.maxHealth_;
            }



            if (level_ == data_.damages_.Length)
            {
                GetComponent<Button>().interactable = false;
            }
        }
    }
}