using System;
using UnityEngine;

namespace nickmaltbie.PoolSet.Pool
{
    /// <summary>
    /// Collection fo all types of pool balls in the game.
    /// </summary>
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PoolBallLibrary", order = 1)]
    public class PoolBallLibrary : ScriptableObject
    {
        /// <summary>
        /// List of all ball prefabs in order.
        /// </summary>
        [SerializeField]
        public GameObject[] ballPrefabs;

        /// <summary>
        /// Get a ball prefab for a specific ball number.
        /// </summary>
        /// <param name="num">Must be between 0 and 15. This is the Number of the ball,
        /// between 1 and 15, number 0 is the cue ball.</param>
        /// <returns>GameObject prefab describing the ball of the specified number.</returns>
        public GameObject GetBallPrefab(int num)
        {
            return ballPrefabs[num];
        }
    }
}