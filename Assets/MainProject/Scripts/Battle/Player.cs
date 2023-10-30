using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Sinabro
{
    public class Player : MonoBehaviour
    {
        //
        public Vector2 inputVec_;
        public float speed_;

        //
        public Scanner scanner_;

        //
        private Rigidbody2D rigidBody_;
        private SpriteRenderer sprite_;
        private Animator anim_;


        // 
        void Start()
        {
            speed_ = 3.0f;
            rigidBody_ = GetComponent<Rigidbody2D>();
            sprite_ = GetComponent<SpriteRenderer>();
            anim_ = GetComponent<Animator>();
            scanner_ = GetComponent<Scanner>();
        }

        //
        void OnMove(InputValue value)
        {
            if (GameManager.Instance.isLive_ == false)
                return;

            inputVec_ = value.Get<Vector2>();
        }

        //
        private void FixedUpdate()
        {
            if (GameManager.Instance.isLive_ == false)
                return;

            Vector2 moveVec = inputVec_ * speed_ * Time.deltaTime;
            rigidBody_.MovePosition(rigidBody_.position + moveVec);
        }

        private void LateUpdate()
        {
            if (GameManager.Instance.isLive_ == false)
                return;

            anim_.SetFloat("Speed", inputVec_.magnitude);

            if (inputVec_.x != 0)
            {
                sprite_.flipX = inputVec_.x < 0;
            }
        }
    }
}


