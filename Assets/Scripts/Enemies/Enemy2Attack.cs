using UnityEngine;
using System.Collections;
using Assets.Scripts.Objects;
using Assets.ScriptableObjects;

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
            StartCoroutine(Attack());
        }

        private void FixedUpdate()
        {
            objectMove.HeadToPlusX(false);
            objectMove.HeadToMinusX(false);
            objectMove.HeadToPlusY(false);
            objectMove.HeadToMinusY(false);
            objectMove.TryToJump(false);
        }

        private IEnumerator Attack()
        {   
            Vector3 fireRePos3 = transform.position;
            Vector3 imDirection3 = ObjectMove.CalclateImDirection3BetWeenTwoRePos3(fireRePos3, ObjectFacade.GetPlayerRePos3()).normalized * 0.2f;
            Vector3 targetImPos3 = ObjectMove.ConvertToImPos3FromRePos3(fireRePos3)  + new Vector3(0f, 0f, enemy2Parameter.SearchEnemy2Z);
            Vector3 playerImPos3 = ObjectMove.ConvertToImPos3FromRePos3(ObjectFacade.GetPlayerRePos3()) + new Vector3(0f, 0f, enemy2Parameter.SearchedTargetZ);
            while(true)
            {
                targetImPos3 += imDirection3;
                if((targetImPos3 - playerImPos3).magnitude <= 0.1f)
                    break;
                if(ObjectMove.IsHitWall(targetImPos3))
                {
                    yield return new WaitForSeconds(enemy2Parameter.AttackCoolDownTime);
                    isAttacking = false;
                    yield break;
                }
            }     
            Instantiate(damageObjectPrefab, ObjectMove.ConvertToTileRePos3FromImPos3(playerImPos3), Quaternion.identity);
            isAttacking = false;
        }
    }
}
