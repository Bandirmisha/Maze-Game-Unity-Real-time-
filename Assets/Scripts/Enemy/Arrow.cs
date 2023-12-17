
using UnityEngine;

namespace MazeGame
{
    public class Arrow : MonoBehaviour
    {
        [HideInInspector] public Direction direction;
        private float shiftV;
        private float shiftH;
        private bool isStuck;

        private void OnEnable()
        {
            direction = Random.Range(0, 3) switch
            {
                0 => Direction.Right,
                1 => Direction.Left,
                2 => Direction.Up,
                3 => Direction.Down,
                _ => throw new System.NotImplementedException(),
            };

            
            if (direction == Direction.Up)
            {
                shiftV = -1f;
            }
            else if (direction == Direction.Down)
            {
                shiftV = 1f;
            }
            else if (direction == Direction.Left)
            {
                shiftH = -1f;
            }
            else if (direction == Direction.Right)
            {
                shiftH = 1f;
            }


            if (direction == Direction.Up || direction == Direction.Down)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
        }


        private void FixedUpdate()
        {
            if (!isStuck)
                Move();
        }

        public void Move()
        {
            Vector3 movement = new Vector3(shiftH, 0.0f, shiftV);
            transform.position += movement * 4f * Time.deltaTime;
        }

        private void OnCollisionEnter(Collision collision)
        {
            //Если игрок
            if (collision.gameObject.layer == 7)
            {
                collision.gameObject.GetComponent<Player>().TakeDamage(5);
            }
            
            Destroy(this.gameObject);
        }
    }
}
