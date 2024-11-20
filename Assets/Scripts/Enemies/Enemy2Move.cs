using UnityEngine;
using Assets.Scripts.Objects;
using Unity.Mathematics;
using Assets.ScriptableObjects;

namespace Assets.Scripts.Enemies
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
            Vector3 moveDirectionIm3 = ObjectMove.CalclateImDirection3BetWeenTwoRePos3(transform.position, targetRePos3);
            objectMove.HeadToPlusX(moveDirectionIm3.x >= enemy2Parameter.StopMoveImDistanceFromPlayer);
            objectMove.HeadToMinusX(moveDirectionIm3.x < -enemy2Parameter.StopMoveImDistanceFromPlayer);
            objectMove.HeadToPlusY(moveDirectionIm3.y >= enemy2Parameter.StopMoveImDistanceFromPlayer);
            objectMove.HeadToMinusY(moveDirectionIm3.y < -enemy2Parameter.StopMoveImDistanceFromPlayer);

            objectMove.TryToJump(objectMove.IsDestinationTileZReachableWithJumping(transform.position));
        }

        public bool CanAttack()
        {
            Vector3 moveDirectionIm3 = ObjectMove.CalclateImDirection3BetWeenTwoRePos3(transform.position, targetRePos3);
            return math.abs(moveDirectionIm3.x) <= enemy2Parameter.StopMoveImDistanceFromPlayer && math.abs(moveDirectionIm3.y) <= enemy2Parameter.StopMoveImDistanceFromPlayer;
        }
    }
}
