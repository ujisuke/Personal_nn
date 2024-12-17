using UnityEngine;

namespace Assets.Scripts.Objects
{
    public class ObjectCleaned : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<ObjectMove>().Freeze();
            GetComponent<ShadowSetter>().DestroyShadow();
            GetComponent<IObjectMain>().DestroyAliveObject();
        }
    }
}