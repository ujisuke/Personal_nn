using Assets.Scripts.Objects;
using Assets.ScriptableObjects;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using System.Threading;
using Assets.Scripts.Stage;
using Assets.Scripts.UI;
using Assets.Scripts.Sounds;
using Assets.Scripts.Effect;

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
        private PlayerAttackEffect playerAttackEffect;
        private SpriteRenderer spriteRenderer;
        private Color32 playerColor;
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
            playerAttackEffect = GetComponentInChildren<PlayerAttackEffect>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            playerColor = spriteRenderer.color;
            audioSource = GetComponent<AudioSource>();
            cancellationTokenSource = new();
            ChargeEnergy().Forget();
        }

        public void SetReady()
        {
            isReady = true;
        }

        public async void TakeDamage(int damage)
        {
            if(isInvincible) return;
            BecomeInvincible().SuppressCancellationThrow().Forget();
            SEPlayer.SingletonInstance.PlayTakeDamage(audioSource);
            ViewEffect.SingletonInstance.PlayerTakeDamage();
            if(hP.IsFatalDamage(damage))
            {
                PlayerHPBar.SingletonInstance.TakeDamage(0);
                spriteRenderer.color = new Color32(0, 0, 0, playerColor.a);
                await ViewEffect.SingletonInstance.PlayerTakeFatalDamage(cancellationTokenSource.Token);
                spriteRenderer.color = playerColor;
            }
            else
                Flash().SuppressCancellationThrow().Forget();
            hP = hP.TakeDamage(damage);
            PlayerHPBar.SingletonInstance.TakeDamage(hP.CurrentHP);
        }

        public async UniTask BecomeInvincible()
        {
            cancellationTokenSource.Token.ThrowIfCancellationRequested();
            isInvincible = true;
            await UniTask.Delay(TimeSpan.FromSeconds(_playerParameter.InvincibleTime), cancellationToken: cancellationTokenSource.Token);
            isInvincible = false;
        }

        private async UniTask Flash()
        {
            cancellationTokenSource.Token.ThrowIfCancellationRequested();
            for(int i = 0; i < 3; i++)
            {
                spriteRenderer.color = new Color32(playerColor.r, playerColor.g, playerColor.b, 0);
                await UniTask.Delay(TimeSpan.FromSeconds(_playerParameter.InvincibleTime / 6f), cancellationToken: cancellationTokenSource.Token);
                spriteRenderer.color = playerColor;
                await UniTask.Delay(TimeSpan.FromSeconds(_playerParameter.InvincibleTime / 6f), cancellationToken: cancellationTokenSource.Token);
            }
        }
        
        public bool IsDead()
        {
            return hP.IsZero();
        }

        public (Vector3 minImPos3, Vector3 maxImPos3) GetImPos3s()
        {
            Vector3 minRePos3 = transform.position - new Vector3(transform.localScale.x * 0.25f, 0f, 0f);
            Vector3 maxRePos3 = transform.position + new Vector3(transform.localScale.x * 0.25f, transform.localScale.y * 0.6f, transform.localScale.y * 0.6f / StageCreator._TileHeight);
            return (ObjectMove.ConvertToImPos3FromRePos3(minRePos3), ObjectMove.ConvertToImPos3FromRePos3(maxRePos3));
        }

        public (Vector3 minImPos3, Vector3 maxImPos3) GetAttackEffectImPos3s()
        {
            return playerAttackEffect.GetImPos3s();
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
