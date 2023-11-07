using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sinabro
{
    public class HUD : MonoBehaviour
    {
        //
        public enum InfoType { Exp, Level, Kill, Time, Health }
        
        //
        public InfoType type_;

        //
        private Text    myText_;
        private Slider  mySlider_;

        //
        private void Awake()
        {
            myText_ = GetComponent<Text>();
            mySlider_ = GetComponent<Slider>();
        }

        //
        private void LateUpdate()
        {
            if (type_ == InfoType.Exp)
            {
                float currentExp = GameManager.Instance.exp_;
                float currentMaxExp = GameManager.Instance.nextExp_[GameManager.Instance.level_];
                mySlider_.value = currentExp / currentMaxExp;
            }
            else if (type_ == InfoType.Level)
            {
                myText_.text = string.Format("Lv.{0:F0}", GameManager.Instance.level_);
            }
            else if (type_ == InfoType.Kill)
            {
                myText_.text = string.Format("{0:F0}", GameManager.Instance.kill_);
            }
            else if (type_ == InfoType.Time)
            {
                float remainTime = GameManager.Instance.maxGameTime_ - GameManager.Instance.gameTime_;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);

                myText_.text = string.Format("{0:D2}:{1:D2}", min, sec);
            }

        }

    }
}