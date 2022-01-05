using UnityEngine;
using UnityEngine.InputSystem;

namespace nickmaltbie.PoolSet.Player
{
    /// <summary>
    /// Moveable ball that supports a player moving an object to follow their cursor.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class MoveableBall : MonoBehaviour
    {
        /// <summary>
        /// Input action reference for finding the current player cursor position.
        /// </summary>
        [SerializeField]
        private InputActionReference cursorPosition;

        /// <summary>
        /// Input action reference for if the cursor is pressed down.
        /// </summary>
        [SerializeField]
        private InputActionReference cursorPressed;

        /// <summary>
        /// Speed of the ball's movement in units per second.
        /// </summary>
        [SerializeField]
        private float movementSpeed = 5.0f;

        /// <summary>
        /// Rigidbody attached to this pool ball object.
        /// </summary>
        private Rigidbody attachedRigidbody;

        /// <summary>
        /// Setup function for the pool ball when it is created.
        /// </summary>
        public void Awake()
        {
            this.attachedRigidbody = GetComponent<Rigidbody>();
            this.attachedRigidbody.isKinematic = true;

            // Ensure actions are enabled
            cursorPressed.action.Enable();
            cursorPosition.action.Enable();
            cursorPressed.action.actionMap.Enable();
            cursorPosition.action.actionMap.Enable();
        }

        /// <summary>
        /// Fixed update run with each physics update to move the ball to track the player's cursor.
        /// </summary>
        public void FixedUpdate()
        {
            // If camera is not setup, do not move the ball.
            if (Camera.main == null)
            {
                return;
            }

            // If the player is not pressing down, don't move the ball
            if (cursorPressed.action.ReadValue<float>() == 0)
            {
                return;
            }

            // Get the position that the cursor is in game space.
            // Translate from screen space to world space using the main camera view.
            Vector2 targetScreenPosition = this.cursorPosition.action.ReadValue<Vector2>();
            Vector3 targetWorldPosition = Camera.main.ScreenToWorldPoint(
                new Vector3(targetScreenPosition.x, targetScreenPosition.y, Camera.main.transform.position.y));

            // Compute the distance we would like to move to reach the cursor.
            Vector3 delta = targetWorldPosition - transform.position;

            // Remove vertical component as we only care about horizontal plane.
            delta.y = 0;

            // Scale the distance of this delta to be a maximum of the distance we can move this fixed update,
            // distance we can move this update is fixed delta time * movement speed.
            float maxDist = Time.fixedDeltaTime * this.movementSpeed;
            delta = delta.normalized * Mathf.Min(delta.magnitude, maxDist);

            // Move our pool ball using the attached rigidbody
            attachedRigidbody.MovePosition(transform.position + delta);
        }
    }
}