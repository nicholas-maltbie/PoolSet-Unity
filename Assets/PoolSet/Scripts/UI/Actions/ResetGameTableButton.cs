using nickmaltbie.PoolSet.Player;
using nickmaltbie.PoolSet.Pool;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace nickmaltbie.PoolSet.UI.Actions
{
    /// <summary>
    /// Reset the game table by placing pool balls in starting positions.
    /// </summary>
    public class ResetGameTableButton : Button
    {
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            // When the player hits this button, reset the pool balls on the table
            BallLayout layout = GameObject.FindObjectOfType<BallLayout>();
            layout.SetupPoolBalls();

            // Also reset the player cue ball position
            foreach (ResetablePlayer player in GameObject.FindObjectsOfType<ResetablePlayer>())
            {
                player.ResetPlayer();
            }
        }
    }
}
