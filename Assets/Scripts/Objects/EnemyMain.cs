using UnityEngine;
using Assets.Scripts.Stage;
using Assets.ScriptableObjects;
using System.Threading;
using Cysharp.Threading.Tasks;
using System;
using Assets.Scripts.Sounds;
using Assets.Scripts.Effect;

namespace Assets.Scripts.Objects
{
    public class EnemyMain : MonoBehaviour, IObjectMain
    {
        private ObjectParameter _objectParameter;
        private HP hP;
        private bool isReady = false;
        public bool IsReady => isReady;
        private bool isCleaned = false;
        public bool IsCleaned => isCleaned;
        private bool isInvincible = false;
        private ObjectMove objectMove;
        private SpriteRenderer spriteRenderer;
        private Color32 enemyColor;
        CancellationTokenSource cancellationTokenSource = null;

        public void Initialize(ObjectParameter objectParameter)
        {
            _objectParameter = objectParameter;
            hP = HP.Initialize(_objectParameter.MaxHP);
            GetComponent<ObjectMove>().Initialize(_objectParameter, transform.position);
            objectMove = GetComponent<ObjectMove>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            enemyColor = spriteRenderer.color;
            cancellationTokenSource = new();
        }

        public void SetReady()
        {
            isReady = true;
        }

        public async void TakeDamage(int damage)
        {
            if(isInvincible) return;
            BecomeInvincible().SuppressCancellationThrow().Forget();
            if(hP.IsFatalDamage(damage))
            {
                GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, enemyColor.a);
                await ViewEffect.SingletonInstance.EnemyTakeFatalDamage(cancellationTokenSource.Token);
                GetComponent<SpriteRenderer>().color = enemyColor;
            }
            else
                await Flash();
            objectMove.KnockBack().Forget();
            hP = hP.TakeDamage(damage);
        }

        public async UniTask BecomeInvincible()
        {
            cancellationTokenSource.Token.ThrowIfCancellationRequested();
            isInvincible = true;
            await UniTask.Delay(TimeSpan.FromSeconds(_objectParameter.InvincibleTime), cancellationToken: cancellationTokenSource.Token);
            isInvincible = false;
        }

        private async UniTask Flash()
        {
            cancellationTokenSource.Token.ThrowIfCancellationRequested();
            for(int i = 0; i < 3; i++)
            {
                spriteRenderer.color = new Color32(enemyColor.r, enemyColor.g, enemyColor.b, 0);
                await UniTask.Delay(TimeSpan.FromSeconds(_objectParameter.InvincibleTime / 6f), cancellationToken: cancellationTokenSource.Token);
                spriteRenderer.color = enemyColor;
                await UniTask.Delay(TimeSpan.FromSeconds(_objectParameter.InvincibleTime / 6f), cancellationToken: cancellationTokenSource.Token);
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
        
        public void SetCleaned()
        {
            isCleaned = true;
        }

        public async void DestroyDeadObject()
        {
            StopTokenSources();
            SEPlayer.SingletonInstance.PlayDead(GetComponent<AudioSource>());
            await UniTask.Delay(TimeSpan.FromSeconds(_objectParameter.DeadTime));
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
    }
}