using Assets.Scripts.Objects;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class Description : MonoBehaviour
    {
        private EnemyMain enemyMain;

        private void Awake()
        {
            enemyMain = transform.parent.GetComponent<EnemyMain>();
            transform.SetParent(CanvasStorage.SingletonDescriptionCanvas.transform);
        }

        private void FixedUpdate()
        {
            if (enemyMain.IsDead() || enemyMain == null)
                Destroy(gameObject);
        }
    }
}
