using UnityEngine;
using System.Collections;
using Assets.Scripts.Objects;
using System.Collections.Generic;

namespace Assets.Scripts.Enemies
{
    public class Enemy1Attack : MonoBehaviour
    {
        private ObjectMove objectMove;
        private bool isAttacking = true;
        public bool IsAttacking => isAttacking;
        private bool isDamaging = false;
        public bool IsDamaging => isDamaging;
        [SerializeField] GameObject damageObject;

        private void OnEnable()
        {
            isAttacking = true;
            isDamaging = false;
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
            List<Vector3> damageObjectRePos3List = ObjectMove.DrawSomeRePos3AtRandom(6, ObjectMove.ConvertToTileIndexFromRePos3(transform.position), 1, 1);
            for(int i = 0; i < damageObjectRePos3List.Count; i++)
                Instantiate(damageObject, damageObjectRePos3List[i], Quaternion.identity);
            yield return new WaitForSeconds(1f);
            isAttacking = false;
        }
    }
}
