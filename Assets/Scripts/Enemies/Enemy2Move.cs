using UnityEngine;
using Assets.Scripts.Objects;
using System.Collections;

namespace Assets.Scripts.Enemies
{
    public class Enemy2Move : MonoBehaviour
    {
        ObjectMove objectMove;
        private Vector3 targetPos3 = new();
        private Vector3 initialtargetPos3 = new(-1, -1, -1);
        private readonly float stopDistance = 0.2f;
        [SerializeField] private GameObject target;

        private void Start()
        {
            objectMove = GetComponent<ObjectMove>();
            targetPos3 = initialtargetPos3;
            StartCoroutine(UpdateTargetPos3());
        }

        private IEnumerator UpdateTargetPos3()
        {
            while(!ObjectFacade.IsPlayerDead())
            {
                yield return new WaitForSeconds(2f);
                targetPos3 = objectMove.DrawRePos3AroundRePos3(transform.position);
                target.transform.position = targetPos3;
            }
        }
    
        private void FixedUpdate()
        {
            Vector3 moveDirectionIm3 = ObjectMove.CalclateImDirection3BetWeenTwoRePos3(transform.position, targetPos3);
            objectMove.HeadToD(moveDirectionIm3.x >= stopDistance);
            objectMove.HeadToA(moveDirectionIm3.x < -stopDistance);
            objectMove.HeadToW(moveDirectionIm3.y >= stopDistance / 2f);
            objectMove.HeadToS(moveDirectionIm3.y < -stopDistance / 2f);

            objectMove.TryToJump(objectMove.IsDestinationTileZReachableWithJumping(transform.position));
        }
    }
}
