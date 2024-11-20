using Assets.Scripts.Objects;
using UnityEngine;
using Assets.Scripts.Stage;
using Assets.ScriptableObjects;
using System.Collections;

namespace Assets.Scripts.Player
{
    public class Player : MonoBehaviour, IObject
    {
        [SerializeField] private PlayerParameter playerParameter;
        private HP hP;
        private Energy energy;
        private PlayerAttack playerAttack;
        private bool isReady = false;
        public bool IsReady => isReady;

        private void Awake()
        {
            ObjectFacade.AddPlayer(this);
            hP = HP.Initialize(playerParameter.MaxHP);
            energy = Energy.Initialize(playerParameter.MaxEnergy);
            playerAttack = GetComponent<PlayerAttack>();
            GetComponent<ObjectMove>().Initialize(playerParameter, transform.position);
            GetComponent<PlayerMove>().Initialize(playerParameter);
            playerAttack.Initialize(playerParameter);
            GetComponent<PlayerDash>().Initialize(playerParameter);
            StartCoroutine(ChargeEnergy());
        }

        private IEnumerator ChargeEnergy()
        {
            while(!IsDead())
            {
                yield return new WaitUntil(() => !energy.IsFull());
                yield return new WaitForSeconds(playerParameter.ChargingEnergyTime);
                energy = energy.Charge(1);
            }
        }

        public void SetReady()
        {
            isReady = true;
        }

        public bool IsDead()
        {
            return hP.IsZero();
        }

        public bool CanUseEnergy(int consumption)
        {
            return energy.CanUse(consumption);
        }

        public bool IsDamaging()
        {
            return playerAttack.IsDamaging;
        }

        public void DamageTo(IObject obj)
        {
            obj.TakeDamage(playerParameter.AttackPower);
        }

        public void TakeDamage(float damage)
        {
            hP = hP.TakeDamage(damage);
        }

        public void ConsumeEnergy(int consumption)
        {
            energy = energy.Consume(consumption);
        }

        public (Vector3 minImPos3, Vector3 maxImPos3) GetImPos3s()
        {
            Vector3 minRePos3 = transform.position - new Vector3(transform.localScale.x / 4f, 0f, 0f);
            Vector3 maxRePos3 = transform.position + new Vector3(transform.localScale.x / 4f, transform.localScale.y, transform.localScale.y / StageFacade._tileHeight);
            return (ObjectMove.ConvertToImPos3FromRePos3(minRePos3), ObjectMove.ConvertToImPos3FromRePos3(maxRePos3));
        }

        public Vector3 GetRePos3()
        {
            return transform.position;
        }

        public void DestroyObject()
        {
            ObjectFacade.RemovePlayer();
            Destroy(gameObject);
        }
    }
}
