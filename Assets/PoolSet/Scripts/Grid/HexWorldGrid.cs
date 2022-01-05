using nickmaltbie.TileMap.Common;
using UnityEngine;

namespace nickmaltbie.TileMap.Hexagon
{
    /// <summary>
    /// Grid for loading a hex grid map into the unity game scene.
    /// </summary>
    public class HexWorldGrid : IWorldGrid<Vector2Int>
    {
        /// <summary>
        /// Cosine of a 30 degree angle
        /// </summary>
        private const float cos30 = 0.866025404f;

        /// <summary>
        /// Distance from the center of a hex to any edge of the hex.
        /// </summary>
        private float distanceToEdge;

        /// <summary>
        /// Distance between the centers of two hexes.
        /// </summary>
        private float DistanceBetweenCenter => 2 * distanceToEdge;

        /// <summary>
        /// Radius of each hexagon (distance from center to a vertex).
        /// </summary>
        private float hexRadius;

        /// <summary>
        /// Base position for this tile map.
        /// </summary>
        private Transform basePosition;

        /// <summary>
        /// Create a hex grid with a given tile map.
        /// </summary>
        /// <param name="hexRadius">Radius of hexagon, distance from center to vertex.</param>
        /// <param name="basePosition">Base position of the square grid.</param>
        public HexWorldGrid(float hexRadius, Transform basePosition)
        {
            this.basePosition = basePosition;
            this.hexRadius = hexRadius;

            // Compute distance from center of hex to any edge
            this.distanceToEdge = cos30 * hexRadius;
        }

        /// <inheritdoc/>
        public Vector3 GetWorldPosition(Vector2Int loc)
        {
            bool evenRow = loc.y % 2 == 0;

            // Offset hexes within a row by twice the distance to an edge
            // Offset even and odd rows by the distance to an edge of a hex in the grid
            float offsetX = DistanceBetweenCenter * loc.x + (evenRow ? 0 : distanceToEdge);

            // Offset hexes in consecutive rows by 1.5 times the hexagon radius
            float offsetY = (hexRadius * 1.5f) * loc.y;

            return basePosition.TransformPoint(offsetX, 0, offsetY);
        }

        /// <inheritdoc/>
        public Quaternion GetWorldRotation(Vector2Int loc) => basePosition.rotation;
    }
}