using UnityEngine;

namespace Assets.Scripts.Objects
{
    public class ObjectCleaned : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<ObjectMove>().Freeze();
            GetComponent<SetShadow>().DestroyShadow();
            GetComponent<IObjectMain>().DestroyAliveObject();
        }
    }
}