using UnityEngine;

namespace nickmaltbie.PoolSet.UI
{
    /// <summary>
    /// Simple class to hide element on a specific runtime platform.
    /// </summary>
    public class HideOnPlatform : MonoBehaviour
    {
        public RuntimePlatform hidePlatform = RuntimePlatform.WebGLPlayer;

        public void Awake()
        {
            gameObject.SetActive(Application.platform != hidePlatform);
        }
    }
}