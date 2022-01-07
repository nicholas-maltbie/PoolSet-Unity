
using System.Collections.Generic;
using nickmaltbie.TileMap.Hexagon;
using UnityEngine;
using System.Linq;

namespace nickmaltbie.PoolSet.Pool
{
    /// <summary>
    /// Layout balls in a proper 9 ball grid in the scene.
    /// </summary>
    public class BallLayout : MonoBehaviour
    {
        /// <summary>
        /// Ball library for looking up ball prefabs.
        /// </summary>
        [SerializeField]
        private PoolBallLibrary ballLibrary;

        /// <summary>
        /// Diameter of a pool ball in units.
        /// </summary>
        [SerializeField]
        private float ballDiameter = 2.25f;

        /// <summary>
        /// buffer of space between pool balls.
        /// </summary>
        [SerializeField]
        private float ballBuffer = 0.05f;

        /// <summary>
        /// Balls placed during setup of ball layout.
        /// </summary>
        private List<GameObject> placedBalls = new List<GameObject>();

        public void Start()
        {
            SetupPoolBalls();
        }

        /// <summary>
        /// Clear out any previously placed balls and setup the 15 balls on the screen.
        /// </summary>
        public void SetupPoolBalls()
        {
            // Clear out any previously placed balls.
            foreach (GameObject poolBall in placedBalls)
            {
                GameObject.Destroy(poolBall);
            }

            // Clear out stale references.
            placedBalls.Clear();

            HexWorldGrid hexGrid = new HexWorldGrid(ballDiameter / 2 + ballBuffer, transform);

            // Layout balls in the following format in the hex grid
            //
            // _ _ x _ _
            //  _ x x _ _
            // _ x 8 x _
            //  x x x x _
            // x x x x x

            // Save all positions in this grid
            // Don't confuse x and y positions :(
            List<Vector2Int> positions = Enumerable.Range(0, 5).SelectMany(
                row => Enumerable.Range(0, row + 1).Select(
                    col => new Vector2Int(col - row / 2 - row % 2, row))).ToList();

            Vector2Int eightBallPos = new Vector2Int(0, 2);

            // Remove position for eight ball from the grid as that is where the 8 ball is placed
            positions.Remove(eightBallPos);
            var eightBall = PlacePoolBall(hexGrid, eightBallPos, 8);

            // Add the ball to the list of saved balls.
            placedBalls.Add(eightBall);

            // Go through each of the remaining positions and place a random ball at each (without replacement)
            List<int> remaining = Enumerable.Range(1, 15).ToList();

            // We already placed the 8 ball so skip that for now
            remaining.Remove(8);

            // Shuffle the remaining balls

            // For each remaining ball, place it at the given position
            var otherBalls = positions.Zip(
                remaining.OrderBy(a => Random.Range(0, 100)),
                (pos, num) => PlacePoolBall(hexGrid, pos, num));

            // Add the placed balls ot the list of saved balls
            placedBalls.AddRange(otherBalls);
        }

        /// <summary>
        /// Places a ball at a given position in the provided hex grid.
        /// </summary>
        /// <param name="hexGrid">Hex grid with positions of balls.</param>
        /// <param name="pos">Position ball is being placed at.</param>
        /// <param name="num">Number of ball being placed.</param>
        /// <returns>The instantiated ball created at the given position.</returns>
        public GameObject PlacePoolBall(HexWorldGrid hexGrid, Vector2Int pos, int num)
        {
            Vector3 worldPos = hexGrid.GetWorldPosition(pos);
            Quaternion worldRot = hexGrid.GetWorldRotation(pos);
            Quaternion randomRot = Random.rotation;
            Quaternion targetRot = Quaternion.Lerp(worldRot, randomRot, 0.5f);

            return GameObject.Instantiate(this.ballLibrary.GetBallPrefab(num), worldPos, targetRot, transform);
        }
    }
}