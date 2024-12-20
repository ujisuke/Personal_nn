using Assets.Scripts.Objects;
using Assets.ScriptableObjects;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using System.Threading;
using Assets.Scripts.Stage;
using Assets.Scripts.UI;
using Assets.Scripts.Sounds;

namespace Assets.Scripts.Player
{
    public class PlayerMain : MonoBehaviour, IObjectMain
    {
        private PlayerParameter _playerParameter;
        private HP hP;
        private Energy energy;
        private bool isReady = false;
        public bool IsReady => isReady;
        private bool isCleaned = false;
        public bool IsCleaned => isCleaned;
        private bool isInvincible = false;
        CancellationTokenSource cancellationTokenSource = null;
        private PlayerAttack playerAttack;
        private AudioSource audioSource;

        public void Initialize(PlayerParameter playerParameter)
        {
            _playerParameter = playerParameter;
            hP = HP.Initialize(_playerParameter.MaxHP);
            energy = Energy.Initialize(_playerParameter.MaxEnergy);
            GetComponent<ObjectMove>().Initialize(_playerParameter, transform.position);
            playerAttack = GetComponent<PlayerAttack>();
            PlayerHPBar.SingletonInstance.ResetValue();
            PlayerEnergyBar.SingletonInstance.ResetValue();
            PlayerAvailableEnergyBar.SingletonInstance.ResetValue();
            audioSource = GetComponent<AudioSource>();
            ChargeEnergy().Forget();
        }

        public void SetReady()
        {
            isReady = true;
        }

        public void TakeDamage(int damage)
        {
            if(isInvincible) return;
            hP = hP.TakeDamage(damage);
            BecomeInvincible().SuppressCancellationThrow().Forget();
            PlayerHPBar.SingletonInstance.TakeDamage(hP.CurrentHP);
            SEPlayer.SingletonInstance.PlayTakeDamage(audioSource);
        }

        public async UniTask BecomeInvincible()
        {
            isInvincible = true;
            cancellationTokenSource = new();
            await UniTask.Delay(TimeSpan.FromSeconds(_playerParameter.InvincibleTime), cancellationToken : cancellationTokenSource.Token);
            isInvincible = false;
        }
        
        public bool IsDead()
        {
            return hP.IsZero();
        }

        public (Vector3 minImPos3, Vector3 maxImPos3) GetImPos3s()
        {
            Vector3 minRePos3 = transform.position - new Vector3(transform.localScale.x / 4f, 0f, 0f);
            Vector3 maxRePos3 = transform.position + new Vector3(transform.localScale.x / 4f, transform.localScale.y, transform.localScale.y / StageCreator._TileHeight);
            return (ObjectMove.ConvertToImPos3FromRePos3(minRePos3), ObjectMove.ConvertToImPos3FromRePos3(maxRePos3));
        }
        
        public void SetCleaned()
        {
            isCleaned = true;
        }

        public async void DestroyDeadObject()
        {
            StopTokenSources();
            SEPlayer.SingletonInstance.PlayDead(audioSource);
            await UniTask.Delay(TimeSpan.FromSeconds(_playerParameter.DeadTime));
            Destroy(gameObject);
        }

        public void DestroyAliveObject()
        {
            StopTokenSources();
            Destroy(gameObject);
        }

        private void StopTokenSources()
        {
            cancellationTokenSource?.Cancel();
            cancellationTokenSource?.Dispose();
        }

        public bool IsDamaging()
        {
            return playerAttack.IsDamaging;
        }

        public void DamageTo(EnemyMain enemy)
        {
            enemy.TakeDamage(_playerParameter.AttackPower);
        }

        public bool CanUseEnergy(int consumption)
        {
            return energy.CanUse(consumption);
        }

        private async UniTask ChargeEnergy()
        {
            while(!IsDead() && !isCleaned)
            {
                await UniTask.WaitUntil(() => !energy.IsFull());
                energy = energy.Charge(0.1f);
                await UniTask.Delay(TimeSpan.FromSeconds(_playerParameter.ChargingEnergyTime * 0.1f));
                PlayerEnergyBar.SingletonInstance.SetValue(energy.CurrentEnergy);
                PlayerAvailableEnergyBar.SingletonInstance.SetValue(energy.CurrentAvailableEnergy);                      
            }
        }

        public void ConsumeEnergy(int consumption)
        {
            energy = energy.Consume(consumption);
            PlayerEnergyBar.SingletonInstance.SetValue(energy.CurrentEnergy);
            PlayerAvailableEnergyBar.SingletonInstance.SetValue(energy.CurrentAvailableEnergy);
        }
    }
}
