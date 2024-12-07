using Assets.Scripts.Objects;
using UnityEngine;
using Assets.ScriptableObjects;

namespace Assets.Scripts.ExitGameEnemy
{
    public class ExitGameEnemyDead : MonoBehaviour
    {
        private ObjectParameter _objectParameter;

        public void Initialize(ObjectParameter objectParameter)
        {
            _objectParameter = objectParameter;
        }

        private void OnEnable()
        {
            GetComponent<ObjectMove>().Dead();
            GetComponent<SetShadow>().DestroyShadow();
            GetComponent<ExitGameEnemyMain>().DestroyDeadObject();
        } 
    }
}