using Assets.Scripts.Objects;
using UnityEngine;
using Assets.Scripts.Stage;
using Assets.ScriptableObjects;
using System.Collections;
using Assets.Scripts.Battle;

namespace Assets.Scripts.Player
{
    public class PlayerMain : MonoBehaviour
    {
        [SerializeField] private PlayerParameter _playerParameter;
        private static HP singletonHP;
        public static int CurrentHP => singletonHP.CurrentHP;
        private static Energy singletonEnergy;
        public static int CurrentAvailableEnergy => singletonEnergy.CurrentAvailableEnergy;
        public static float CurrentEnergy => singletonEnergy.CurrentEnergy;
        private PlayerAttack playerAttack;
        private bool isReady = false;
        public bool IsReady => isReady;
        private bool isInvincible = false;


        private void Awake()
        {
            ObjectFacade.AddPlayer(this);
            singletonHP = HP.Initialize(_playerParameter.MaxHP);
            singletonEnergy = Energy.Initialize(_playerParameter.MaxEnergy);
            playerAttack = GetComponent<PlayerAttack>();
            GetComponent<ObjectMove>().Initialize(_playerParameter, transform.position);
            GetComponent<PlayerMove>().Initialize(_playerParameter);
            playerAttack.Initialize(_playerParameter);
            GetComponent<PlayerDash>().Initialize(_playerParameter);
            GetComponent<PlayerAnimation>().Initialize(_playerParameter);
            StartCoroutine(ChargeEnergy());
        }

        private IEnumerator ChargeEnergy()
        {
            while(!IsDead())
            {
                yield return new WaitUntil(() => !singletonEnergy.IsFull());
                for(int i = 0; i < 10; i++)
                {
                    yield return new WaitForSeconds(_playerParameter.ChargingEnergyTime * 0.1f);
                    singletonEnergy = singletonEnergy.Charge(0.1f);
                }
            }
        }

        public void SetReady()
        {
            isReady = true;
        }

        public static bool IsDead()
        {
            return singletonHP.IsZero();
        }

        public static bool CanUseEnergy(int consumption)
        {
            return singletonEnergy.CanUse(consumption);
        }

        public bool IsDamaging()
        {
            return playerAttack.IsDamaging;
        }

        public void DamageTo(IEnemyMain obj)
        {
            obj.TakeDamage(_playerParameter.AttackPower);
        }

        public void TakeDamage(int damage)
        {
            if(isInvincible) return;
            singletonHP = singletonHP.TakeDamage(damage);
            StartCoroutine(BecomeInvincible());
        }

        private IEnumerator BecomeInvincible()
        {
            isInvincible = true;
            yield return new WaitForSeconds(_playerParameter.InvincibleTime);
            isInvincible = false;
        }

        public static void ConsumeEnergy(int consumption)
        {
            singletonEnergy = singletonEnergy.Consume(consumption);
        }

        public (Vector3 minImPos3, Vector3 maxImPos3) GetImPos3s()
        {
            Vector3 minRePos3 = transform.position - new Vector3(transform.localScale.x / 4f, 0f, 0f);
            Vector3 maxRePos3 = transform.position + new Vector3(transform.localScale.x / 4f, transform.localScale.y, transform.localScale.y / StageFacade._TileHeight);
            return (ObjectMove.ConvertToImPos3FromRePos3(minRePos3), ObjectMove.ConvertToImPos3FromRePos3(maxRePos3));
        }

        public void DestroyDeadObject()
        {
            ObjectFacade.RemovePlayer();
            BattleFacade.DeathCount++;
            StartCoroutine(WaitAndDestroy());
        }

        private IEnumerator WaitAndDestroy()
        {
            yield return new WaitForSeconds(_playerParameter.DeadTime);
            Destroy(gameObject);
        }

        public void DestroyAliveObject()
        {
            ObjectFacade.RemovePlayer();
            Destroy(gameObject);
        }
    }
}
