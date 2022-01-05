using UnityEngine;

namespace nickmaltbie.PoolSet.Player
{
    /// <summary>
    /// Player who's position can be reset to it's initial value.
    /// </summary>
    public class ResetablePlayer : MonoBehaviour
    {
        /// <summary>
        /// Position to reset player to upon request.
        /// </summary>
        private Vector3 resetPosition;

        /// <summary>
        /// Rotation to reset player to upon request.
        /// </summary>
        private Quaternion resetRotation;

        /// <summary>
        /// Setup the initial reset position and rotation to be the player's current location.
        /// </summary>
        public void Start()
        {
            this.resetPosition = transform.position;
            this.resetRotation = transform.rotation;
        }

        /// <summary>
        /// Resets the player's position and rotation to their initial values.
        /// </summary>
        public void ResetPlayer()
        {
            this.transform.position = this.resetPosition;
            this.transform.rotation = this.resetRotation;
        }

    }
}