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
        public Scanner                      scanner_;


        //
        private Rigidbody2D     rigid_;
        private Collider2D      coll_;
        private SpriteRenderer  spriter_;
        private Animator        anim_;

        private WaitForFixedUpdate wait_;

        //
        public Transform weaponRoot_;
        private WeaponBase myWeapon_ = null;

        //
        [Header("# Enemy Info")]
        public EnemyShipEntity  enemyShipInfo_;
        public double[]         shipStatusDatas_ = new double[(int)ShipStatus.MAX];
        public int              maxHp_;

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

            //target_ = scanner_.nearestRigidbodyTarget_;

            if (target_ == null)
                return;

            if (anim_.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
                return;

            if (myWeapon_ != null)
                myWeapon_.UpdateWeapon();

            if (Vector3.Distance(target_.position, rigid_.position) > enemyShipInfo_.Range - 0.01f)
            {
                Vector2 dirVec = target_.position - rigid_.position;
                Vector2 nextVec = dirVec.normalized * (float)shipStatusDatas_[(int)ShipStatus.MoveSpeed] * Time.fixedDeltaTime;
                rigid_.MovePosition(rigid_.position + nextVec);
                rigid_.velocity = Vector2.zero;
            }

        }

        private void LateUpdate()
        {
            if (GameManager.Instance.isLive_ == false)
                return;

            if (target_ == null)
                return;


            spriter_.flipX = target_.position.x > rigid_.position.x;
        }

        //
        public void CleateUnit(EnemyShipEntity enemyShipInfo, HpBarControl hpBar)
        {
            pooledObject_ = GetComponent<PooledObject>();

            //
            target_ = GameManager.Instance.targetPlayer_;

            //
            hpBar_ = hpBar;

            //
            anim_.runtimeAnimatorController = animControl_[0];

            //
            enemyShipInfo_ = enemyShipInfo;
            shipStatusDatas_[(int)ShipStatus.HP] = enemyShipInfo.Hp;
            shipStatusDatas_[(int)ShipStatus.AP] = enemyShipInfo.Ap;
            shipStatusDatas_[(int)ShipStatus.Aim] = enemyShipInfo.Aim;
            shipStatusDatas_[(int)ShipStatus.CoolTime] = enemyShipInfo.FireCool;
            shipStatusDatas_[(int)ShipStatus.Range] = enemyShipInfo.Range;
            shipStatusDatas_[(int)ShipStatus.DefenseSide] = enemyShipInfo.SideDp;
            shipStatusDatas_[(int)ShipStatus.DefenseTop] = enemyShipInfo.TopDp;
            shipStatusDatas_[(int)ShipStatus.DefenseTorpedo] = enemyShipInfo.TorepedoDp;
            shipStatusDatas_[(int)ShipStatus.MoveSpeed] = enemyShipInfo.MoveSpeed;
            shipStatusDatas_[(int)ShipStatus.Fuel] = enemyShipInfo.Fuel;
            shipStatusDatas_[(int)ShipStatus.Shell] = enemyShipInfo.ShellCnt;
            scanner_.scanRange_ = (float)shipStatusDatas_[(int)ShipStatus.Range];
            maxHp_ = enemyShipInfo.Hp;

            hpBar_.UpdateHp((int)shipStatusDatas_[(int)ShipStatus.HP], maxHp_);

            //
            myWeapon_ = new WeaponOneGuns();
            myWeapon_.CreateWeapon(weaponRoot_, false, (AttackType)enemyShipInfo.AttackType, gameObject);

            //
            spriter_.sprite = Resources.Load<Sprite>("ShipImg/" + enemyShipInfo.ResourceName);
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
        private void Dead()
        {
            pooledObject_.pool.ReturnObject(gameObject);
        }
    }
}