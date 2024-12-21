using UnityEngine;
using Assets.Scripts.Objects;
using System.Collections.Generic;
using Assets.ScriptableObjects;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using Assets.Scripts.Sounds;

namespace Assets.Scripts.Enemy3
{
    public class Enemy3Attack : MonoBehaviour
    {
        private Enemy3Parameter _enemy3Parameter;
        private ObjectMove objectMove;
        private bool isAttacking = true;
        public bool IsAttacking => isAttacking;
        private CancellationTokenSource cancellationTokenSource = null;
        private AudioSource audioSource;

        public void Initialize(Enemy3Parameter enemy3Parameter)
        {
            _enemy3Parameter = enemy3Parameter;
            objectMove = GetComponent<ObjectMove>();
            audioSource = GetComponent<AudioSource>();
        }

        private async void OnEnable()
        {
            cancellationTokenSource = new();
            isAttacking = true;
            objectMove.Stop();
            await Attack().SuppressCancellationThrow();
            isAttacking = false;
        }

        private async UniTask Attack()
        {
            List<List<Vector3>> objectRePos3ListList = ObjectMove.GetAllReachableRePos3WithoutJumping(transform.position);
            EnemyMain enemy = GetComponent<EnemyMain>();
            for(int i = 0; i < objectRePos3ListList.Count; i++)
            {
                List<Vector3> objectRePos3List = objectRePos3ListList[i];
                if(objectRePos3List.Count == 0) continue;
                for(int j = 0; j < objectRePos3List.Count; j++)
                    ObjectCreator.SingletonInstance.InstantiateEnemyDamageObject(objectRePos3List[j], _enemy3Parameter.EnemyDamageObjectParameter, enemy);
                SEPlayer.SingletonInstance.PlayInstantiateEnemy3DamageObject(audioSource);
                await UniTask.Delay(TimeSpan.FromSeconds(_enemy3Parameter.WaveMoveTime), cancellationToken: cancellationTokenSource.Token);
            }
        }

        public void StopAttack()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
    }
}
