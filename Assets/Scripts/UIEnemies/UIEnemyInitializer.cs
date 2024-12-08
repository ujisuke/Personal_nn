using Assets.ScriptableObjects;
using Assets.Scripts.Objects;
using UnityEngine;

namespace Assets.Scripts.UIEnemies
{
    public class UIEnemyInitializer : MonoBehaviour
    {
        [SerializeField] private ObjectParameter _uiEnemyParameter;
        private void Awake()
        {
            GetComponent<ObjectStateMachine>().Initialize(new UIEnemyReadyState());
            GetComponent<EnemyMain>().Initialize(_uiEnemyParameter);
            GetComponent<UIEnemyAnimation>().Initialize(_uiEnemyParameter);
        }
    }
}