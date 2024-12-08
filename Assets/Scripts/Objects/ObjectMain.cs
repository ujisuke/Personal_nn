using UnityEngine;
using Assets.Scripts.Stage;
using Assets.ScriptableObjects;
using System.Threading;
using Cysharp.Threading.Tasks;
using System;

namespace Assets.Scripts.Objects
{
    public class ObjectMainBase : MonoBehaviour
    {
        protected ObjectParameter _objectParameter;
        protected HP hP;
        private bool isReady = false;
        public bool IsReady => isReady;
        private bool isCreaned = false;
        public bool IsCleaned => isCreaned;
        private bool isInvincible = false;
        CancellationTokenSource cancellationTokenSource = null;

        public void Initialize(ObjectParameter objectParameter)
        {
            _objectParameter = objectParameter;
            hP = HP.Initialize(_objectParameter.MaxHP);
            GetComponent<ObjectMove>().Initialize(_objectParameter, transform.position);
        }

        public void SetReady()
        {
            isReady = true;
        }

        public void TakeDamage(int damage)
        {
            if(isInvincible) return;
            hP = hP.TakeDamage(damage);
            BecomeInvincible().Forget();
        }

        private async UniTask BecomeInvincible()
        {
            isInvincible = true;
            cancellationTokenSource = new();
            await UniTask.Delay(TimeSpan.FromSeconds(_objectParameter.InvincibleTime), cancellationToken : cancellationTokenSource.Token).SuppressCancellationThrow();
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
            isCreaned = true;
        }

        public async UniTask DestroyDeadObject()
        {
            cancellationTokenSource?.Cancel();
            cancellationTokenSource?.Dispose();
            await UniTask.Delay(TimeSpan.FromSeconds(_objectParameter.DeadTime));
            Destroy(gameObject);
        }

        public void DestroyAliveObject()
        {
            cancellationTokenSource?.Cancel();
            cancellationTokenSource?.Dispose();
            Destroy(gameObject);
        }
    }
}