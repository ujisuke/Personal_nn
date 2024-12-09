using Assets.ScriptableObjects;
using Assets.Scripts.Objects;
using UnityEngine;

namespace Assets.Scripts.Enemy4
{
    public class Enemy4Initializer : MonoBehaviour
    {
        [SerializeField] private Enemy4Parameter _enemy4Parameter;
        private void Awake()
        {
            GetComponent<ObjectStateMachine>().Initialize(new Enemy4ReadyState());
            GetComponent<EnemyMain>().Initialize(_enemy4Parameter);
            GetComponent<Enemy4Attack1>().Initialize(_enemy4Parameter);
            GetComponent<Enemy4Attack2>().Initialize(_enemy4Parameter);
            GetComponent<Enemy4Move>().Initialize(_enemy4Parameter);
            GetComponent<Enemy4Animation>().Initialize(_enemy4Parameter);
        }
    }
}