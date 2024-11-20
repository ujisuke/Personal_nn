using UnityEngine;
using System.Collections;
using Assets.Scripts.Objects;
using Assets.Scripts.Stage;
using System.Collections.Generic;

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
            List<List<Vector3>> objectRePos3ListList = ObjectMove.GetAllRePos3ReachableWithoutJumping(transform.position);
            for(int i = 0; i < objectRePos3ListList.Count; i++)
            {
                List<Vector3> objectRePos3List = objectRePos3ListList[i];
                for(int j = 0; j < objectRePos3List.Count; j++)
                    Instantiate(damageObjectPrefab, objectRePos3List[j], Quaternion.identity);
                yield return new WaitForSeconds(0.2f);
            }
            isAttacking = false;
        }
    }
}
