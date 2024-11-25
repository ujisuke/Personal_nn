using Assets.ScriptableObjects;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class Enemy3Animation : MonoBehaviour
    {
        private Animator _animator;
        private Enemy3Parameter _enemy3Parameter;

        public void Initialize(Enemy3Parameter enemy3Parameter)
        {
            _enemy3Parameter = enemy3Parameter;
            _animator = GetComponent<Animator>();
            _animator.SetFloat("DeadSpeed", 1f / _enemy3Parameter.DeadTime);
        }

        public void SetLookingDirection((bool PlusImX, bool MinusImX, bool PlusImY, bool MinusImY) isLooking)
        {
            _animator.SetBool("IsLookingPlusImX", isLooking.PlusImX);
            _animator.SetBool("IsLookingMinusImX", isLooking.MinusImX);
            _animator.SetBool("IsLookingPlusImY", isLooking.PlusImY);
            _animator.SetBool("IsLookingMinusImY", isLooking.MinusImY);
        }

        public void StartStand()
        {
            _animator.SetBool("IsStanding", true);
        }

        public void StopStand()
        {
            _animator.SetBool("IsStanding", false);
        }

        public void StartAttack()
        {
            _animator.SetBool("IsAttacking", true);
        }

        public void StopAttack()
        {
            _animator.SetBool("IsAttacking", false);
        }

        public void StartDead()
        {
            _animator.SetBool("IsDead", true);
        }
    }
}