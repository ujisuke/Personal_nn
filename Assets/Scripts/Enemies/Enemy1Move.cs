using UnityEngine;
using Assets.Scripts.Objects;
using System.Collections;
using Unity.Mathematics;
using Assets.ScriptableObjects;


namespace Assets.Scripts.Enemies
{
    public class Enemy1Move : MonoBehaviour
    {
        private Enemy1Parameter enemy1Parameter;
        private ObjectMove objectMove;
        private Vector3 targetRePos3 = new(-1, -1, -1);


        public void Initialize(Enemy1Parameter enemy1Parameter)
        {
            this.enemy1Parameter = enemy1Parameter;
            objectMove = GetComponent<ObjectMove>();
        }

        private void OnEnable()
        {
            targetRePos3 = ObjectFacade.GetPlayerRePos3();
            StartCoroutine(UpdateTargetPos3());
        }

        private IEnumerator UpdateTargetPos3()
        {
            while(!ObjectFacade.IsPlayerDead())
            {
                yield return new WaitForSeconds(0.2f);
                targetRePos3 = ObjectFacade.GetPlayerRePos3();
            }
        }
    
        private void FixedUpdate()
        {
            Vector3 moveDirectionIm3 = ObjectMove.CalclateImDirection3BetWeenTwoRePos3(transform.position, targetRePos3);
            objectMove.HeadToPlusX(moveDirectionIm3.x >= enemy1Parameter.StopMoveImDistanceFromPlayer);
            objectMove.HeadToMinusX(moveDirectionIm3.x < -enemy1Parameter.StopMoveImDistanceFromPlayer);
            objectMove.HeadToPlusY(moveDirectionIm3.y >= enemy1Parameter.StopMoveImDistanceFromPlayer);
            objectMove.HeadToMinusY(moveDirectionIm3.y < -enemy1Parameter.StopMoveImDistanceFromPlayer);
            objectMove.TryToJump(objectMove.IsDestinationTileZReachableWithJumping(transform.position));
        }

        private bool IsNearPlayer()
        {
            Vector3 moveDirectionIm3 = ObjectMove.CalclateImDirection3BetWeenTwoRePos3(transform.position, targetRePos3);
            return math.abs(moveDirectionIm3.x) <= enemy1Parameter.AttackImDistanceFromPlayer && math.abs(moveDirectionIm3.y) <= enemy1Parameter.AttackImDistanceFromPlayer;
        }

        public bool CanAttack()
        {
            return IsNearPlayer() && !objectMove.IsJumping;
        }
    }
}
