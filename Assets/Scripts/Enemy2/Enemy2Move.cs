using UnityEngine;
using Assets.Scripts.Objects;
using Unity.Mathematics;
using Assets.ScriptableObjects;

namespace Assets.Scripts.Enemy2
{
    public class Enemy2Move : MonoBehaviour
    {
        private Enemy2Parameter enemy2Parameter;
        private ObjectMove objectMove;
        private Vector3 targetRePos3 = new();
        private Vector3 initialtargetRePos3 = new(-1, -1, -1);

        public void Initialize(Enemy2Parameter enemy2Parameter)
        {
            this.enemy2Parameter = enemy2Parameter;
            objectMove = GetComponent<ObjectMove>();
        }

        private void OnEnable()
        {
            targetRePos3 = initialtargetRePos3;
            targetRePos3 = objectMove.DrawRePos3AroundRePos3(transform.position);
        }
    
        private void FixedUpdate()
        {
            Vector3 moveDirectionIm3 = ObjectMove.CalculateImDirection3BetWeenTwoRePos3(transform.position, targetRePos3);
            objectMove.HeadToPlusImX(moveDirectionIm3.x >= enemy2Parameter.StopMoveImDistanceFromPlayer);
            objectMove.HeadToMinusImX(moveDirectionIm3.x < -enemy2Parameter.StopMoveImDistanceFromPlayer);
            objectMove.HeadToPlusImY(moveDirectionIm3.y >= enemy2Parameter.StopMoveImDistanceFromPlayer);
            objectMove.HeadToMinusImY(moveDirectionIm3.y < -enemy2Parameter.StopMoveImDistanceFromPlayer);
            objectMove.TryToJump(objectMove.IsDestinationTileZReachableWithJumping(transform.position));
        }

        public bool CanAttack()
        {
            Vector3 moveDirectionIm3 = ObjectMove.CalculateImDirection3BetWeenTwoRePos3(transform.position, targetRePos3);
            return math.abs(moveDirectionIm3.x) <= enemy2Parameter.StopMoveImDistanceFromPlayer && math.abs(moveDirectionIm3.y) <= enemy2Parameter.StopMoveImDistanceFromPlayer;
        }

        public bool IsMissingPlayer()
        {
            if(!CanAttack()) return false;
            Vector3 fireRePos3 = transform.position;
            Vector3 imDirection3 = ObjectMove.CalculateImDirection3BetWeenTwoRePos3(fireRePos3, ObjectStorage.GetPlayerRePos3()).normalized * 0.2f;
            Vector3 targetImPos3 = ObjectMove.ConvertToImPos3FromRePos3(fireRePos3)  + new Vector3(0f, 0f, enemy2Parameter.SearchEnemy2Z);
            Vector3 playerImPos3 = ObjectMove.ConvertToImPos3FromRePos3(ObjectStorage.GetPlayerRePos3()) + new Vector3(0f, 0f, enemy2Parameter.SearchedTargetZ);
            while(true)
            {
                targetImPos3 += imDirection3;
                if((targetImPos3 - playerImPos3).magnitude <= 0.1f)
                    return false;
                if(ObjectMove.IsHitWall(targetImPos3))
                    return true;
            }
        }
    }
}
