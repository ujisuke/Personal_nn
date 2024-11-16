using UnityEngine;
using System.Collections;
using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemies
{
    public class Enemy3Attack : MonoBehaviour
    {
        [SerializeField] private GameObject damageObjectPrefab;
        private ObjectMove objectMove;
        private bool isAttacking = true;
        public bool IsAttacking => isAttacking;
        
        private void OnEnable()
        {
            isAttacking = true;
            objectMove = GetComponent<ObjectMove>();;
            StartCoroutine(Attack());
        }

        private void FixedUpdate()
        {
            objectMove.HeadToD(false);
            objectMove.HeadToA(false);
            objectMove.HeadToW(false);
            objectMove.HeadToS(false);
            objectMove.TryToJump(false);
        }

        private IEnumerator Attack()
        {
            Vector3 fireRePos3 = transform.position + new Vector3(0f, 0f, 1f);
            Vector3 imDirection3 = ObjectMove.CalclateImDirection3BetWeenTwoRePos3(fireRePos3, ObjectFacade.GetPlayerRePos3()).normalized / 10f;
            Vector3 enemy3ImPos3 = ObjectMove.ConvertToImPos3FromRePos3(fireRePos3);
            Vector3 targetImPos3 = ObjectMove.ConvertToImPos3FromRePos3(fireRePos3);
            Vector3 playerImPos3 = ObjectMove.ConvertToImPos3FromRePos3(ObjectFacade.GetPlayerRePos3());
            while(true)
            {
                targetImPos3 += imDirection3;
                if(ObjectMove.IsHitStage(targetImPos3))
                    break;
            }
            yield return new WaitForSeconds(0.5f);
            targetImPos3 = enemy3ImPos3;
            while(true)
            {
                targetImPos3 += imDirection3;
                Instantiate(damageObjectPrefab, ObjectMove.ConvertToRePos3FromImPos3(targetImPos3), Quaternion.identity);
                if(ObjectMove.IsHitStage(targetImPos3))
                    break;
            }
            yield return new WaitForSeconds(0.5f);
            isAttacking = false;
        }
    }
}
