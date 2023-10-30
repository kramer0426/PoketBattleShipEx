using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sinabro
{
    public class LevelUp : MonoBehaviour
    {
        //
        private RectTransform rect_;
        private Item[] items_;

        //
        private void Awake()
        {
            rect_ = GetComponent<RectTransform>();
            items_ = GetComponentsInChildren<Item>(true);
        }

        //
        public void Show()
        {
            Next();
            rect_.localScale = Vector3.one;
            GameManager.Instance.Stop();
        }

        //
        public void Hide()
        {
            rect_.localScale = Vector3.zero;
            GameManager.Instance.Resume();
        }

        //
        public void Select(int index)
        {
            items_[index].OnClick();
        }

        //
        private void Next()
        {
            //
            for (int i = 0; i < items_.Length; ++i)
            {
                items_[i].gameObject.SetActive(false);
            }

            //
            int[] ran = new int[3];
            while (true)
            {
                ran[0] = Random.Range(0, items_.Length);
                ran[1] = Random.Range(0, items_.Length);
                ran[2] = Random.Range(0, items_.Length);

                if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2])
                    break;
            }

            for (int i = 0; i < ran.Length; ++i)
            {
                Item ranItem = items_[ran[i]];
                if (ranItem.level_ == ranItem.data_.damages_.Length)
                {
                    items_[4].gameObject.SetActive(true);
                }
                else
                {
                    ranItem.gameObject.SetActive(true);
                }

            }
        }
    }
}