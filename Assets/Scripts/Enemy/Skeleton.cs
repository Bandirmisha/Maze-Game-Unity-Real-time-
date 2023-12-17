using UnityEngine;


namespace MazeGame
{
    public class Skeleton : Enemy
    {
        public GameObject arrowPrefab;

        [SerializeField] private float shootCooldown;
        private float time;

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            if (!alive) return;

            time += Time.deltaTime;
            if (time >= shootCooldown)
            {
                time = 0;
                Shoot();
            }
        }

        public void Shoot()
        {
            GameManager.instance.CreateGameObject(arrowPrefab, transform.position);
        }

    }
}
