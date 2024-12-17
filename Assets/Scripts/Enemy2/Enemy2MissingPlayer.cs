using UnityEngine;
using Assets.Scripts.Objects;
using Assets.ScriptableObjects;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;

namespace Assets.Scripts.Enemy2
{
    public class Enemy2MissingPlayer : MonoBehaviour
    {
        private Enemy2Parameter _enemy2Parameter;
        private ObjectMove objectMove;
        private bool isMissingPlayer = true;
        public bool IsMissingPlayer => isMissingPlayer;
        private CancellationTokenSource cancellationTokenSource = null;

        
        public void Initialize(Enemy2Parameter enemy2Parameter)
        {
            this._enemy2Parameter = enemy2Parameter;
            objectMove = GetComponent<ObjectMove>();
        }

        private async void OnEnable()
        {
            isMissingPlayer = true;
            objectMove.Stop();
            cancellationTokenSource = new();
            await UniTask.Delay(TimeSpan.FromSeconds(_enemy2Parameter.MissingPlayerTime), cancellationToken: cancellationTokenSource.Token).SuppressCancellationThrow();
            isMissingPlayer = false;
        }

        public void StopMissingPlayer()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
    }
}
