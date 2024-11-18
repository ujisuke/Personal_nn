using UnityEngine;
using Assets.Scripts.Objects;
using Unity.Mathematics;
using System.Collections;

namespace Assets.Scripts.Enemies
{
    public class Enemy3Move : MonoBehaviour
    {
        private ObjectMove objectMove;
        private Vector3 targetImPos3 = new();
        private Vector3 initialtargetPos3 = new(-1, -1, -1);
        private bool canAttack = false;
        public bool CanAttack => canAttack;

        private void OnEnable()
        {
            objectMove = GetComponent<ObjectMove>();
            objectMove.Initialize(transform.position);
            targetImPos3 = initialtargetPos3;
            StartCoroutine(TrackPlayer());
        }

        private IEnumerator TrackPlayer()
        {
            for(int i = 0; i < 10; i++)
            {
                targetImPos3 = ObjectMove.ConvertToImPos3FromRePos3(ObjectFacade.GetPlayerRePos3());
                yield return new WaitForSeconds(0.2f);
            }
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
