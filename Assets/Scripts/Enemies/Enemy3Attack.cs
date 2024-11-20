using UnityEngine;
using System.Collections;
using Assets.Scripts.Objects;
using Assets.Scripts.Stage;
using System.Collections.Generic;
using Assets.ScriptableObjects;

namespace Assets.Scripts.Enemies
{
    public class Enemy3Attack : MonoBehaviour
    {
        private Enemy3Parameter enemy3Parameter;
        [SerializeField] private GameObject damageObjectPrefab;
        private ObjectMove objectMove;
        private bool isAttacking = true;
        public bool IsAttacking => isAttacking;
    
        public void Initialize(Enemy3Parameter enemy3Parameter)
        {
            this.enemy3Parameter = enemy3Parameter;
            objectMove = GetComponent<ObjectMove>();
        }

        private void OnEnable()
        {
            isAttacking = true;
            StartCoroutine(Attack());
        }

        private void FixedUpdate()
        {
            objectMove.HeadToPlusImX(false);
            objectMove.HeadToMinusImX(false);
            objectMove.HeadToPlusImY(false);
            objectMove.HeadToMinusImY(false);
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
                yield return new WaitForSeconds(enemy3Parameter.WaveMoveTime);
            }
            isAttacking = false;
        }
    }
}
