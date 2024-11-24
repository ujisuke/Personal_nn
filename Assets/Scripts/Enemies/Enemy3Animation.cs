using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class Enemy3Animation : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetLookingDirection((bool PlusImX, bool MinusImX, bool PlusImY, bool MinusImY) isLooking)
        {
            _animator.SetBool("IsHeadingToPlusImX", isLooking.PlusImX);
            _animator.SetBool("IsHeadingToMinusImX", isLooking.MinusImX);
            _animator.SetBool("IsHeadingToPlusImY", isLooking.PlusImY);
            _animator.SetBool("IsHeadingToMinusImY", isLooking.MinusImY);
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
    }
}