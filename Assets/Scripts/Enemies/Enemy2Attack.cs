using UnityEngine;
using System.Collections;
using Assets.Scripts.Objects;
using Assets.ScriptableObjects;
using Unity.Mathematics;

namespace Assets.Scripts.Enemies
{
    public class Enemy2Attack : MonoBehaviour
    {
        private Enemy2Parameter enemy2Parameter;
        [SerializeField] private GameObject damageObjectPrefab;
        private ObjectMove objectMove;
        private bool isAttacking = true;
        public bool IsAttacking => isAttacking;
        
        public void Initialize(Enemy2Parameter enemy2Parameter)
        {
            this.enemy2Parameter = enemy2Parameter;
            objectMove = GetComponent<ObjectMove>();
        }

        private void OnEnable()
        {
            isAttacking = true;
            objectMove.HeadToPlusImX(false);
            objectMove.HeadToMinusImX(false);
            objectMove.HeadToPlusImY(false);
            objectMove.HeadToMinusImY(false);
            objectMove.TryToJump(false);
            StartCoroutine(Attack());
        }

        private IEnumerator Attack()
        {   
            for(int i = 0; i < enemy2Parameter.AttackCount; i++)
            {
                ObjectCreator.InstantiateDamageObject(damageObjectPrefab, ObjectMove.ConvertToTileRePos3FromImPos3(ObjectMove.ConvertToImPos3FromRePos3(ObjectFacade.GetPlayerRePos3()) + new Vector3(0f, 0f, enemy2Parameter.SearchedTargetZ)), enemy2Parameter.DamageObjectParameter);
                yield return new WaitForSeconds(enemy2Parameter.AttackCoolDownTime);
            } 
            isAttacking = false;
        }

        public (bool isLookingPlusImX, bool isLookingMinusImX, bool isLookingPlusImY, bool isLookingMinusImY) GetLookingDirection()
        {
            Vector3 moveDirectionIm3 = ObjectMove.CalculateImDirection3BetWeenTwoRePos3(transform.position, ObjectFacade.GetPlayerRePos3());
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
