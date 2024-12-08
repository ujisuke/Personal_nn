using UnityEngine;

namespace Assets.Scripts.Objects
{
    public class UIEnemyMove : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<ObjectMove>().Stop();
        }
    }
}
