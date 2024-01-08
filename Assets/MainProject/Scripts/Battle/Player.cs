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
        public Scanner fireScanner_;
        public Scanner sightScanner_;
        public AIStateBase aiState_;
        public List<AIStateBase> aiStateList_ = new List<AIStateBase>();
        public AIType aiType_;

        //
        private PooledObject    pooledObject_;
        public Rigidbody2D      rigidBody_;
        public SpriteRenderer   sprite_;
        private Animator        anim_;
        private HpBarControl    hpBar_;

        //
        public Transform weaponRoot_;
        public WeaponBase myWeapon_ = null;

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
            //
            aiType_ = AIType.Normal;
            aiStateList_.Clear();
            aiStateList_.Add(new AIPatrolState());
            aiStateList_.Add(new AIChaseState());
            aiStateList_.Add(new AIAttackState());
            aiStateList_.Add(new AIDefenseState());
            aiStateList_.Add(new AIDeadState());
            for (int i = 0; i < aiStateList_.Count; ++i)
            {
                aiStateList_[i].Initialize(gameObject, true);
            }
            aiState_ = aiStateList_[0];

            //
            speed_ = 3.0f;
            rigidBody_ = GetComponent<Rigidbody2D>();
            sprite_ = GetComponent<SpriteRenderer>();
            anim_ = GetComponent<Animator>();

            //
            hpBar_ = hpBar;
            pooledObject_ = GetComponent<PooledObject>();
            shipData_ = shipData;
            shipData_.MakeShipAbility();
            shipStatusDatas_ = shipData_.shipStatusDatas_;
            maxHp_ = (int)shipStatusDatas_[(int)ShipStatus.HP];
            speed_ = (float)shipStatusDatas_[(int)ShipStatus.MoveSpeed];
            fireScanner_.scanRange_ = (float)shipStatusDatas_[(int)ShipStatus.FireRange];
            sightScanner_.scanRange_ = (float)shipStatusDatas_[(int)ShipStatus.SightRange];
            hpBar_.UpdateHp((int)shipStatusDatas_[(int)ShipStatus.HP], maxHp_);

            //
            myWeapon_ = new WeaponOneGuns();
            myWeapon_.CreateWeapon(weaponRoot_, true, (AttackType)shipData_.shipInfo_.AttackType, gameObject, fireScanner_);

            //
            sprite_.sprite = Resources.Load<Sprite>("ShipImg/" + shipData_.shipInfo_.ResourceName);
        }

        //
        public void ChangeAIState(AIStateID aiState)
        {
            aiState_ = aiStateList_[(int)aiState];
            aiState_.Initialize(gameObject, true);
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

            //
            aiState_.AIUpdate();
        }

        private void LateUpdate()
        {
            if (GameManager.Instance.isLive_ == false)
                return;

            //anim_.SetFloat("Speed", inputVec_.magnitude);

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


