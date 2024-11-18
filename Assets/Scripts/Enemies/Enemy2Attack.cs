using UnityEngine;
using System.Collections;
using Assets.Scripts.Objects;

namespace Assets.Scripts.Enemies
{
    public class Enemy2Attack : MonoBehaviour
    {
        [SerializeField] private GameObject damageObjectPrefab;
        private ObjectMove objectMove;
        private bool isAttacking = true;
        public bool IsAttacking => isAttacking;
        
        private void OnEnable()
        {
            isAttacking = true;
            objectMove = GetComponent<ObjectMove>();
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
            for(int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(0.3f);
                Vector3 fireRePos3 = transform.position + new Vector3(0f, 0f, 1f);
                Vector3 imDirection3 = ObjectMove.CalclateImDirection3BetWeenTwoRePos3(fireRePos3, ObjectFacade.GetPlayerRePos3()).normalized * 0.2f;
                Vector3 enemy2ImPos3 = ObjectMove.ConvertToImPos3FromRePos3(fireRePos3);
                Vector3 targetImPos3 = ObjectMove.ConvertToImPos3FromRePos3(fireRePos3);
                Vector3 playerImPos3 = ObjectMove.ConvertToImPos3FromRePos3(ObjectFacade.GetPlayerRePos3());
                while(true)
                {
                    targetImPos3 += imDirection3;
                    if((targetImPos3 - playerImPos3).magnitude <= 0.1f || ObjectMove.IsHitWall(targetImPos3))
                        break;
                }
                yield return new WaitForSeconds(0.1f);
                targetImPos3 = enemy2ImPos3;
                while(true)
                {
                    targetImPos3 += imDirection3;
                    if((targetImPos3 - playerImPos3).magnitude <= 0.1f || ObjectMove.IsHitWall(targetImPos3))
                        break;
                }           
                Instantiate(damageObjectPrefab, ObjectMove.ConvertToRePos3FromImPos3(targetImPos3), Quaternion.identity);
            }
            isAttacking = false;
        }
    }
}
