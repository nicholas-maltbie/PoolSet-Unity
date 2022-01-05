using nickmaltbie.PoolSet.Player;
using UnityEngine;

namespace nickmaltbie.PoolSet.UI.Events
{
    /// <summary>
    /// Simple class to pause any active players when the menu is loaded.
    /// </summary>
    public class PlayerPauseStateOnMenuLoad : MonoBehaviour, IScreenComponent
    {
        /// <summary>
        /// Target state of players when this menu is loaded
        /// </summary>
        public bool enabledState;

        public void OnScreenLoaded()
        {
            foreach (MoveableBall player in GameObject.FindObjectsOfType<MoveableBall>())
            {
                player.enabled = enabledState;
            }
        }

        public void OnScreenUnloaded()
        {

        }
    }
}