using UnityEngine;
using Assets.Scripts.Stage;
using Assets.ScriptableObjects;
using System.Threading;
using Cysharp.Threading.Tasks;
using System;
using Assets.Scripts.Sounds;

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
        CancellationTokenSource cancellationTokenSource = null;

        public void Initialize(ObjectParameter objectParameter)
        {
            _objectParameter = objectParameter;
            hP = HP.Initialize(_objectParameter.MaxHP);
            GetComponent<ObjectMove>().Initialize(_objectParameter, transform.position);
            cancellationTokenSource = new();
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
        }

        public async UniTask BecomeInvincible()
        {
            isInvincible = true;
            cancellationTokenSource = new();
            await UniTask.Delay(TimeSpan.FromSeconds(_objectParameter.InvincibleTime), cancellationToken : cancellationTokenSource.Token);
            isInvincible = false;
        }
        
        public bool IsDead()
        {
            return hP.IsZero();
        }

        public (Vector3 minImPos3, Vector3 maxImPos3) GetImPos3s()
        {
            Vector3 minRePos3 = transform.position - new Vector3(transform.localScale.x / 4f, 0f, 0f);
            Vector3 maxRePos3 = transform.position + new Vector3(transform.localScale.x / 4f, transform.localScale.y, transform.localScale.y / StageFacade.TileHeight);
            return (ObjectMove.ConvertToImPos3FromRePos3(minRePos3), ObjectMove.ConvertToImPos3FromRePos3(maxRePos3));
        }
        
        public void SetCleaned()
        {
            isCleaned = true;
        }

        public async void DestroyDeadObject()
        {
            StopTokenSources();
            PlaySE.SingletonInstance.PlayDead();
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