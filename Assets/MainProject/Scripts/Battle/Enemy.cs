using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sinabro
{
    public class Enemy : MonoBehaviour
    {
        //
        public RuntimeAnimatorController[]  animControl_;
        public Rigidbody2D                  target_;
        private PooledObject                pooledObject_;
        private HpBarControl                hpBar_;
        public Scanner                      fireScanner_;
        public Scanner                      sightScanner_;
        public AIStateBase                  aiState_;
        public List<AIStateBase>            aiStateList_ = new List<AIStateBase>();
        public AIType                       aiType_;

        //
        public Rigidbody2D     rigid_;
        private Collider2D      coll_;
        public SpriteRenderer  sprite_;
        private Animator        anim_;

        private WaitForFixedUpdate wait_;

        //
        public Transform weaponRoot_;
        public WeaponBase myWeapon_ = null;

        //
        [Header("# Enemy Info")]
        public EnemyShipEntity  enemyShipInfo_;
        public double[]         shipStatusDatas_ = new double[(int)ShipStatus.MAX];
        public int              maxHp_;
        public int              fleetIndex_;

        private void Awake()
        {
            rigid_ = GetComponent<Rigidbody2D>();
            coll_ = GetComponent<Collider2D>();
            sprite_ = GetComponent<SpriteRenderer>();
            anim_ = GetComponent<Animator>();
            wait_ = new WaitForFixedUpdate();
        }

        private void FixedUpdate()
        {
            if (GameManager.Instance.isLive_ == false)
                return;

            if (anim_.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
                return;

            //
            aiState_.AIUpdate();
        }

        private void LateUpdate()
        {
            if (GameManager.Instance.isLive_ == false)
                return;

            if (target_ == null)
                return;
        }

        //
        public void CleateUnit(EnemyShipEntity enemyShipInfo, HpBarControl hpBar, int fleetIndex)
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
                aiStateList_[i].Initialize(gameObject, false);
            }
            aiState_ = aiStateList_[0];

            //
            pooledObject_ = GetComponent<PooledObject>();

            //
            target_ = GameManager.Instance.targetPlayer_;

            //
            hpBar_ = hpBar;
            fleetIndex_ = fleetIndex;

            //
            anim_.runtimeAnimatorController = animControl_[0];

            //
            enemyShipInfo_ = enemyShipInfo;
            shipStatusDatas_[(int)ShipStatus.HP] = enemyShipInfo.Hp;
            shipStatusDatas_[(int)ShipStatus.AP] = enemyShipInfo.Ap;
            shipStatusDatas_[(int)ShipStatus.Aim] = enemyShipInfo.Aim;
            shipStatusDatas_[(int)ShipStatus.CoolTime] = enemyShipInfo.FireCool;
            shipStatusDatas_[(int)ShipStatus.FireRange] = enemyShipInfo.FireRange;
            shipStatusDatas_[(int)ShipStatus.SightRange] = enemyShipInfo.SightRange;
            shipStatusDatas_[(int)ShipStatus.DefenseSide] = enemyShipInfo.SideDp;
            shipStatusDatas_[(int)ShipStatus.DefenseTop] = enemyShipInfo.TopDp;
            shipStatusDatas_[(int)ShipStatus.DefenseTorpedo] = enemyShipInfo.TorepedoDp;
            shipStatusDatas_[(int)ShipStatus.MoveSpeed] = enemyShipInfo.MoveSpeed;
            shipStatusDatas_[(int)ShipStatus.Fuel] = enemyShipInfo.Fuel;
            shipStatusDatas_[(int)ShipStatus.Shell] = enemyShipInfo.ShellCnt;
            fireScanner_.scanRange_ = (float)shipStatusDatas_[(int)ShipStatus.FireRange];
            sightScanner_.scanRange_ = (float)shipStatusDatas_[(int)ShipStatus.SightRange];
            maxHp_ = enemyShipInfo.Hp;

            hpBar_.UpdateHp((int)shipStatusDatas_[(int)ShipStatus.HP], maxHp_);

            //
            myWeapon_ = new WeaponOneGuns();
            myWeapon_.CreateWeapon(weaponRoot_, false, (AttackType)enemyShipInfo.AttackType, gameObject, fireScanner_);

            //
            sprite_.sprite = Resources.Load<Sprite>("ShipImg/" + enemyShipInfo.ResourceName);
        }


        //
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Bullet") == false || GameManager.Instance.isLive_ == false)
                return;

            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet != null)
            {
                if (bullet.bPlayer_ == true)
                {
                    shipStatusDatas_[(int)ShipStatus.HP] -= bullet.damage_;
                    hpBar_.UpdateHp((int)shipStatusDatas_[(int)ShipStatus.HP], maxHp_);
                    //StartCoroutine(KnockBack());


                    if (shipStatusDatas_[(int)ShipStatus.HP] > 0)
                    {
                        //anim_.SetTrigger("Hit");
                    }
                    else
                    {
                        //anim_.SetBool("Dead", true);
                    }

                    bullet.DieBullet();
                }
            }


        }

        //IEnumerator KnockBack()
        //{
        //    yield return wait_;

        //    Vector3 playerPos = GameManager.Instance.player_.transform.position;
        //    Vector3 dirVec = transform.position - playerPos;
        //    rigid_.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
        //}

        //
        public void ChangeAIState(AIStateID aiState)
        {
            aiState_ = aiStateList_[(int)aiState];
            aiState_.Initialize(gameObject, true);
        }

        //
        private void Dead()
        {
            pooledObject_.pool.ReturnObject(gameObject);
        }
    }
}