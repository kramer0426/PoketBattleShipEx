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
        private PooledObject    pooledObject_;
        private Rigidbody2D     rigidBody_;
        private SpriteRenderer  sprite_;
        private Animator        anim_;
        private HpBarControl    hpBar_;

        //
        public Transform weaponRoot_;
        private WeaponBase myWeapon_ = null;

        //
        [Header("# Player Info")]
        public MyShipData   shipData_ = null;
        public double[]     shipStatusDatas_ = new double[(int)ShipStatus.MAX];
        public int          maxHp_;

        // 
        void Start()
        {


        }

        //
        public void CreateUnit(MyShipData shipData, HpBarControl hpBar)
        {
            speed_ = 3.0f;
            rigidBody_ = GetComponent<Rigidbody2D>();
            sprite_ = GetComponent<SpriteRenderer>();
            anim_ = GetComponent<Animator>();
            scanner_ = GetComponent<Scanner>();

            //
            hpBar_ = hpBar;
            pooledObject_ = GetComponent<PooledObject>();
            shipData_ = shipData;
            shipData_.MakeShipAbility();
            shipStatusDatas_ = shipData_.shipStatusDatas_;
            maxHp_ = (int)shipStatusDatas_[(int)ShipStatus.HP];
            speed_ = (float)shipStatusDatas_[(int)ShipStatus.MoveSpeed];
            scanner_.scanRange_ = (float)shipStatusDatas_[(int)ShipStatus.Range];
            hpBar_.UpdateHp((int)shipStatusDatas_[(int)ShipStatus.HP], maxHp_);

            //
            myWeapon_ = new WeaponOneGuns();
            myWeapon_.CreateWeapon(weaponRoot_, true, (AttackType)shipData_.shipInfo_.AttackType, gameObject);

            //
            sprite_.sprite = Resources.Load<Sprite>("ShipImg/" + shipData_.shipInfo_.ResourceName);
        }


        //
        void OnMove(InputValue value)
        {
            if (GameManager.Instance.isLive_ == false)
                return;

            inputVec_ = value.Get<Vector2>();
        }

        //
        private void Dead()
        {
            pooledObject_.pool.ReturnObject(gameObject);
        }

        //
        private void FixedUpdate()
        {
            if (GameManager.Instance.isLive_ == false)
                return;

            if (myWeapon_ != null)
                myWeapon_.UpdateWeapon();

            Vector2 moveVec = inputVec_ * speed_ * Time.deltaTime;
            rigidBody_.MovePosition(rigidBody_.position + moveVec);
        }

        private void LateUpdate()
        {
            if (GameManager.Instance.isLive_ == false)
                return;

            //anim_.SetFloat("Speed", inputVec_.magnitude);

            if (inputVec_.x != 0)
            {
                sprite_.flipX = inputVec_.x < 0;
            }
        }

        //
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Bullet") == false || GameManager.Instance.isLive_ == false)
                return;

            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet != null)
            {
                if (bullet.bPlayer_ == false)
                {
                    shipStatusDatas_[(int)ShipStatus.HP] -= bullet.damage_;
                    hpBar_.UpdateHp((int)shipStatusDatas_[(int)ShipStatus.HP], maxHp_);

                    if (shipStatusDatas_[(int)ShipStatus.HP] > 0)
                    {
                        //anim_.SetTrigger("Hit");
                    }
                    else
                    {
                        // to do : die
                    }

                    bullet.DieBullet();
                }
            }
        }
    }
}


