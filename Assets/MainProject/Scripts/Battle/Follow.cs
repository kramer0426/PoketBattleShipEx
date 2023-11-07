using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sinabro
{
    public class Follow : MonoBehaviour
    {
        //
        private RectTransform rect_;
        private Camera mainCamera_;


        //
        private void Awake()
        {
            rect_ = GetComponent<RectTransform>();
            mainCamera_ = Camera.main;
        }

        //
        private void FixedUpdate()
        {
            if (GameManager.Instance.focusPlayer_ != null)
                rect_.position = mainCamera_.WorldToScreenPoint(GameManager.Instance.focusPlayer_.position);
        }
    }
}