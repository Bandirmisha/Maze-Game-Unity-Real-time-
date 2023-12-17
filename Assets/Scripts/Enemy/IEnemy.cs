using UnityEngine;

namespace MazeGame
{
    internal interface IEnemy
    {
        static Vector3 position;
        static bool alive;
        void Move();
        void Attack();
    }
}
