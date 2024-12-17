using UnityEngine;
using Assets.Scripts.Objects;
using Assets.ScriptableObjects;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;

namespace Assets.Scripts.Enemy3
{
    public class Enemy3Move : MonoBehaviour
    {
        private Enemy3Parameter _enemy3Parameter;
        private ObjectMove objectMove;
        private bool canAttack = false;
        public bool CanAttack => canAttack;
        CancellationTokenSource cancellationTokenSource = null;

        public void Initialize(Enemy3Parameter enemy3Parameter)
        {
            _enemy3Parameter = enemy3Parameter;
            objectMove = GetComponent<ObjectMove>();
        }

        private async void OnEnable()
        {
            canAttack = false;
            objectMove.Stop();
            cancellationTokenSource = new();
            await UniTask.Delay(TimeSpan.FromSeconds(_enemy3Parameter.AttackCoolDownTime), cancellationToken: cancellationTokenSource.Token).SuppressCancellationThrow();
            canAttack = true;
        }

        public void StopMove()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
    }
}
