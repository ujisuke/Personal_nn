using UnityEngine;
using System.Collections;
using Assets.Scripts.Objects;
using System.Collections.Generic;
using Assets.ScriptableObjects;

namespace Assets.Scripts.Enemy1
{
    public class Enemy1Attack : MonoBehaviour
    {
        private ObjectMove objectMove;
        private Enemy1Parameter enemy1Parameter;
        private bool isAttacking = true;
        public bool IsAttacking => isAttacking;
        private bool isDamaging = false;
        public bool IsDamaging => isDamaging;
        [SerializeField] private GameObject _damageObject;

        public void Initialize(Enemy1Parameter enemy1Parameter)
        {
            objectMove = GetComponent<ObjectMove>();
            this.enemy1Parameter = enemy1Parameter;
        }

        private void OnEnable()
        {
            isAttacking = true;
            isDamaging = false;
            objectMove.Stop();
            StartCoroutine(Attack());
        }

        private IEnumerator Attack()
        {          
            List<Vector3> damageObjectRePos3List = ObjectMove.DrawSomeRePos3AtRandom(enemy1Parameter.AttackPanelCount, ObjectMove.ConvertToTileIndexFromRePos3(transform.position),
            enemy1Parameter.AttackPanelMinImRadius, enemy1Parameter.AttackPanelMaxImRadius);
            IEnemyMain enemy = GetComponent<IEnemyMain>();
            for(int i = 0; i < damageObjectRePos3List.Count; i++)
                ObjectCreator.InstantiateDamageObject(_damageObject, damageObjectRePos3List[i], enemy1Parameter.DamageObjectParameter, enemy);
            yield return new WaitForSeconds(enemy1Parameter.AttackCoolDownTime);
            isAttacking = false;
        }
    }
}
