using UnityEngine;

namespace Assets.Scripts.Objects
{
    public class ObjectDead : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<ObjectMove>().Freeze();
            GetComponent<ShadowSetter>().DestroyShadow();
            GetComponent<IObjectMain>().DestroyDeadObject();
        }
    }
}