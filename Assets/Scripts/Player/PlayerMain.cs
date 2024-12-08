using Assets.Scripts.Objects;
using Assets.ScriptableObjects;
using Cysharp.Threading.Tasks;
using System;

namespace Assets.Scripts.Player
{
    public class PlayerMain : ObjectMainBase
    {
        private PlayerParameter _playerParameter;
        private static HP singletonHP;
        public static int CurrentHP => singletonHP.CurrentHP;
        private static Energy singletonEnergy;
        public static int CurrentAvailableEnergy => singletonEnergy.CurrentAvailableEnergy;//identity
        public static float CurrentEnergy => singletonEnergy.CurrentEnergy;
        private PlayerAttack playerAttack;

        public void InitializePlayer(PlayerParameter playerParameter)
        {
            _playerParameter = playerParameter;
            Initialize(_playerParameter);
            singletonHP = hP;
            singletonEnergy = Energy.Initialize(_playerParameter.MaxEnergy);
            playerAttack = GetComponent<PlayerAttack>();
            ChargeEnergy().Forget();
        }

        private async UniTask ChargeEnergy()
        {
            while(!IsDead())
            {
                await UniTask.WaitUntil(() => !singletonEnergy.IsFull());
                for(int i = 0; i < 10; i++)
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(_playerParameter.ChargingEnergyTime * 0.1f));
                    singletonEnergy = singletonEnergy.Charge(0.1f);
                }
            }
        }

        public static bool CanUseEnergy(int consumption)
        {
            return singletonEnergy.CanUse(consumption);
        }

        public bool IsDamaging()
        {
            return playerAttack.IsDamaging;
        }

        public void DamageTo(EnemyMain enemy)
        {
            enemy.TakeDamage(_playerParameter.AttackPower);
        }

        public static void ConsumeEnergy(int consumption)
        {
            singletonEnergy = singletonEnergy.Consume(consumption);
        }
    }
}
