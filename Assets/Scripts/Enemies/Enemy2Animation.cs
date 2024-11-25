using Assets.ScriptableObjects;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class Enemy2Animation : MonoBehaviour
    {
        private Animator _animator;
        private Enemy2Parameter _enemy2Parameter;

        public void Initialize(Enemy2Parameter enemy2Parameter)
        {
            _enemy2Parameter = enemy2Parameter;
            _animator = GetComponent<Animator>();
            _animator.SetFloat("DeadSpeed", 1f / _enemy2Parameter.DeadTime);
        }

        public void SetLookingDirection((bool PlusImX, bool MinusImX, bool PlusImY, bool MinusImY) isLooking)
        {
            _animator.SetBool("IsLookingPlusImX", isLooking.PlusImX);
            _animator.SetBool("IsLookingMinusImX", isLooking.MinusImX);
            _animator.SetBool("IsLookingPlusImY", isLooking.PlusImY);
            _animator.SetBool("IsLookingMinusImY", isLooking.MinusImY);
        }

        public void StartWalk()
        {
            _animator.SetBool("IsWalking", true);
        }

        public void StopWalk()
        {
            _animator.SetBool("IsWalking", false);
        }

        public void StartAttack()
        {
            _animator.SetBool("IsAttacking", true);
        }

        public void StopAttack()
        {
            _animator.SetBool("IsAttacking", false);
        }

        public void StartStand()
        {
            _animator.SetBool("IsStanding", true);
        }

        public void StopStand()
        {
            _animator.SetBool("IsStanding", false);
        }

        public void StartDead()
        {
            _animator.SetBool("IsDead", true);
        }
    }
}