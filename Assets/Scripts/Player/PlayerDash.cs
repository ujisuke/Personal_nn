using UnityEngine;
using Assets.Scripts.Objects;
using System.Collections;
using Assets.ScriptableObjects;

namespace Assets.Scripts.Player
{
    public class PlayerDash : MonoBehaviour
    {
        private PlayerParameter playerParameter;
        private ObjectMove objectMove;
        private bool isDashing = true;
        public bool IsDashing => isDashing;
        
        public void Initialize(PlayerParameter playerParameter)
        {
            objectMove = GetComponent<ObjectMove>();
            this.playerParameter = playerParameter;
        }

        private void OnEnable()
        {
            isDashing = true;
            PlayerMain.ConsumeEnergy(playerParameter.DashEnergyConsumption);
            StartCoroutine(Dash());
        }

        private IEnumerator Dash()
        {
            objectMove.StartDash(playerParameter.DashSpeedRatio);
            yield return new WaitForSeconds(playerParameter.DashingTime);
            objectMove.StopDash();
            isDashing = false;
        }
    }
}
