using UnityEngine;
using Assets.Scripts.Objects;
using System.Collections;
using Assets.ScriptableObjects;

namespace Assets.Scripts.Enemies
{
    public class Enemy1Move : MonoBehaviour
    {
        ObjectMove objectMove;
        private Vector3 targetPos3 = new();
        private Vector3 initialtargetPos3 = new(-1, -1, -1);
        private readonly float stopDistance = 0.4f;
        [SerializeField] private ObjectData objectData;

        private void Start()
        {
            objectMove = GetComponent<ObjectMove>();
            objectMove.Initialize(objectData, transform.position);
            targetPos3 = initialtargetPos3;
            StartCoroutine(UpdateTargetPos3());
        }

        private IEnumerator UpdateTargetPos3()
        {
            while(!ObjectFacade.IsPlayerDead())
            {
                yield return new WaitForSeconds(0.2f);
                targetPos3 = ObjectFacade.GetPlayerPos3();
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
