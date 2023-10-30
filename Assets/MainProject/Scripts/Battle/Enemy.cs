using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sinabro
{
    public class Enemy : MonoBehaviour
    {
        //
        public float                        speed_;
        public double                       health_;
        public double                       maxHealth_;
        public RuntimeAnimatorController[]  animControl_;
        public Rigidbody2D                  target_;

        //
        private bool            isLive_ = false;

        private Rigidbody2D     rigid_;
        private Collider2D      coll_;
        private SpriteRenderer  spriter_;
        private Animator        anim_;

        private WaitForFixedUpdate wait_;

        private void Awake()
        {
            rigid_ = GetComponent<Rigidbody2D>();
            coll_ = GetComponent<Collider2D>();
            spriter_ = GetComponent<SpriteRenderer>();
            anim_ = GetComponent<Animator>();
            wait_ = new WaitForFixedUpdate();
        }

        private void FixedUpdate()
        {
            if (GameManager.Instance.isLive_ == false)
                return;

            if (target_ == null)
                return;

            if (isLive_ == false || anim_.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
                return;

            Vector2 dirVec = target_.position - rigid_.position;
            Vector2 nextVec = dirVec.normalized * speed_ * Time.fixedDeltaTime;
            rigid_.MovePosition(rigid_.position + nextVec);
            rigid_.velocity = Vector2.zero;
        }

        private void LateUpdate()
        {
            if (GameManager.Instance.isLive_ == false)
                return;

            if (target_ == null)
                return;

            if (isLive_ == false)
                return;

            spriter_.flipX = target_.position.x < rigid_.position.x;
        }

        //
        private void OnEnable()
        {
            target_ = GameManager.Instance.player_.GetComponent<Rigidbody2D>();
            isLive_ = true;
            coll_.enabled = true;
            rigid_.simulated = true;
            spriter_.sortingOrder = 2;
            anim_.SetBool("Dead", false);
            health_ = maxHealth_;
        }

        //
        public void Init(SpwanData data)
        {
            anim_.runtimeAnimatorController = animControl_[data.spriteType_];
            speed_ = data.speed_;
            maxHealth_ = data.health_;
            health_ = data.health_;
        }

        //
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Bullet") == false || isLive_ == false)
                return;

            health_ -= collision.GetComponent<Bullet>().damage_;
            StartCoroutine(KnockBack());

            if (health_ > 0)
            {
                anim_.SetTrigger("Hit");
            }
            else
            {
                isLive_ = false;
                coll_.enabled = false;
                rigid_.simulated = false;
                spriter_.sortingOrder = 1;
                anim_.SetBool("Dead", true);
                GameManager.Instance.kill_++;
                GameManager.Instance.GetExp();
            }
        }

        IEnumerator KnockBack()
        {
            yield return wait_;

            Vector3 playerPos = GameManager.Instance.player_.transform.position;
            Vector3 dirVec = transform.position - playerPos;
            rigid_.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
        }

        private void Dead()
        {
            this.gameObject.SetActive(false);
        }
    }
}