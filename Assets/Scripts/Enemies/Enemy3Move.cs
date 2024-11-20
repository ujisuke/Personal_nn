using UnityEngine;
using Assets.Scripts.Objects;
using System.Collections;
using Assets.ScriptableObjects;

namespace Assets.Scripts.Enemies
{
    public class Enemy3Move : MonoBehaviour
    {
        private Enemy3Parameter enemy3Parameter;
        private ObjectMove objectMove;
        private bool canAttack = false;
        public bool CanAttack => canAttack;

        public void Initialize(Enemy3Parameter enemy3Parameter)
        {
            this.enemy3Parameter = enemy3Parameter;
            objectMove = GetComponent<ObjectMove>();
        }

        private void OnEnable()
        {
            canAttack = false;
            StartCoroutine(CoolDown());
        }

        private IEnumerator CoolDown()
        { 
            yield return new WaitForSeconds(enemy3Parameter.AttackCoolDownTime);
            canAttack = true;
        }
    
        private void FixedUpdate()
        {
            objectMove.HeadToPlusX(false);
            objectMove.HeadToMinusX(false);
            objectMove.HeadToPlusY(false);
            objectMove.HeadToMinusY(false);
            objectMove.TryToJump(false);
        }
    }
}
