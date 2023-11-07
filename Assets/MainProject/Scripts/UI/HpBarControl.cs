using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sinabro
{
    public class HpBarControl : MonoBehaviour
    {
        //
        private RectTransform   rect_;
        public Slider           mySlider_;
        private Camera          mainCamera_;
        private Transform       target_;

        //
        private void Awake()
        {
            rect_ = GetComponent<RectTransform>();
            mainCamera_ = Camera.main;
        }

        //
        public void SetUI(Transform traget)
        {
            target_ = traget;
        }

        //
        public void UpdateHp(float hp, float maxHp)
        {
            mySlider_.value = hp / maxHp;
        }

        //
        private void FixedUpdate()
        {
            rect_.position = mainCamera_.WorldToScreenPoint(target_.position);
        }
    }
}