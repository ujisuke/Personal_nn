using UnityEngine;
using Assets.Scripts.Objects;
using Assets.ScriptableObjects;
using Unity.Mathematics;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;

namespace Assets.Scripts.Enemy2
{
    public class Enemy2Attack : MonoBehaviour
    {
        private Enemy2Parameter enemy2Parameter;
        private ObjectMove objectMove;
        private bool isAttacking = true;
        public bool IsAttacking => isAttacking;
        private CancellationTokenSource cancellationTokenSource = null;
        private CancellationTokenSource linkedTokenSource = null;
        
        public void Initialize(Enemy2Parameter enemy2Parameter)
        {
            this.enemy2Parameter = enemy2Parameter;
            objectMove = GetComponent<ObjectMove>();
        }

        private async void OnEnable()
        {
            cancellationTokenSource = new();
            linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationTokenSource.Token, this.GetCancellationTokenOnDestroy());
            isAttacking = true;
            objectMove.Stop();
            await Attack(linkedTokenSource.Token).SuppressCancellationThrow();
            isAttacking = false;
        }

        private async UniTask Attack(CancellationToken token)
        {
            IEnemyMain enemy = GetComponent<IEnemyMain>();
            for(int i = 0; i < enemy2Parameter.AttackCount; i++)
            {
                ObjectCreator.InstantiateEnemyDamageObject(ObjectMove.ConvertToTileRePos3FromImPos3(ObjectMove.ConvertToImPos3FromRePos3(ObjectStorage.GetPlayerRePos3()) + new Vector3(0f, 0f, enemy2Parameter.SearchedTargetZ)), enemy2Parameter.EnemyDamageObjectParameter, enemy);
                await UniTask.Delay(TimeSpan.FromSeconds(enemy2Parameter.AttackCoolDownTime), cancellationToken: token);
            } 
        }

        public void StopAttack()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
            linkedTokenSource.Cancel();
            linkedTokenSource.Dispose();
        }

        private void OnDestory()
        {
            StopAttack();
        }

        public (bool isLookingPlusImX, bool isLookingMinusImX, bool isLookingPlusImY, bool isLookingMinusImY) GetLookingDirection()
        {
            Vector3 moveDirectionIm3 = ObjectMove.CalculateImDirection3BetWeenTwoRePos3(transform.position, ObjectStorage.GetPlayerRePos3());
            if(math.abs(moveDirectionIm3.x) > math.abs(moveDirectionIm3.y))
            {
                if(moveDirectionIm3.x > 0)
                    return (true, false, false, false);
                else
                    return (false, true, false, false);
            }
            else
            {
                if(moveDirectionIm3.y > 0)
                    return (false, false, true, false);
                else
                    return (false, false, false, true);
            }
        }
    }
}
