using UnityEngine;
using Assets.Scripts.Enemy1;
using Assets.Scripts.Enemy2;
using Assets.Scripts.Enemy3;
using Assets.Scripts.Player;
using Assets.Scripts.ExitGameEnemy;
using Assets.Scripts.StartBattleEnemy;
using Assets.Scripts.ResetDataEnemy;
using Assets.Scripts.SetGameEnemy;
using Assets.Scripts.BackToLobbyEnemy;

namespace Assets.Scripts.Objects
{
    public class ObjectStateMachine : MonoBehaviour
    {
        private IObjectState currentState;
        [SerializeField] private ObjectStateList initialState;

        private void Awake()
        {
            if(initialState == ObjectStateList.Player)
                currentState = new PlayerNotReadyState();
            else if(initialState == ObjectStateList.Enemy1)
                currentState = new Enemy1NotReadyState();
            else if(initialState == ObjectStateList.Enemy2)
                currentState = new Enemy2NotReadyState();
            else if(initialState == ObjectStateList.Enemy3)
                currentState = new Enemy3NotReadyState();
            else if(initialState == ObjectStateList.ExitEnemy)
                currentState = new ExitEnemyNotReadyState();
            else if(initialState == ObjectStateList.StartBattleEnemy)
                currentState = new StartBattleEnemyNotReadyState();
            else if(initialState == ObjectStateList.ResetEnemy)
                currentState = new ResetDataEnemyNotReadyState();
            else if(initialState == ObjectStateList.SettingEnemy)
                currentState = new SetGameEnemyNotReadyState();
            else if(initialState == ObjectStateList.BackToLobbyEnemy)
                currentState = new BackToLobbyEnemyNotReadyState();
            currentState.Enter(this);
        }

        public void TransitionTo(IObjectState newState)
        {
            currentState.Exit();
            currentState = newState;
            newState.Enter(this);
        }

        private void FixedUpdate()
        {
            currentState.FixedUpdate();
        }
    }

    public enum ObjectStateList
    {
        Player,
        Enemy1,
        Enemy2,
        Enemy3,
        ExitEnemy,
        StartBattleEnemy,
        ResetEnemy,
        SettingEnemy,
        BackToLobbyEnemy
    }
}