using Assets.ScriptableObjects;
using UnityEngine;

namespace Assets.Scripts.ExitGameEnemy
{
    public class ExitGameEnemyAnimation : MonoBehaviour
    {
        private Animator animator;
        private ObjectParameter _objectParameter;

        public void Initialize(ObjectParameter objectParameter)
        {
            _objectParameter = objectParameter;
            animator = GetComponent<Animator>();
            animator.SetFloat("DeadSpeed" , 1f / _objectParameter.DeadTime);
            animator.SetFloat("ReadySpeed", 1f / _objectParameter.ReadyTime);
        }

        public void StartMove()
        {
            animator.SetBool("IsStanding", true);
        }

        public void StopMove()
        {
            animator.SetBool("IsStanding", false);
        }

        public void StartDead()
        {
            animator.SetBool("IsDead", true);
        }
    }
}
