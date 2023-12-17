using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UIElements;

namespace MazeGame
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Sword sword;

        public int HP;
        public string Quest;
        public bool isKeyPicked;

        private Direction direction;

        private void Start()
        {
            HP = 100;
            SetQuest(0);

            isKeyPicked = false;

            direction = Direction.Right;
        }

        private void Update()
        {
            ChangeMovementDirection();

            if (Input.GetMouseButtonDown(0))
            {
                Attack();
            }

        }
        private void FixedUpdate()
        {
            Move();
        }

        private void SetQuest(int index)
        {
            switch (index)
            {
                case 0: Quest = "Найдите ключ"; break;
                case 1: Quest = "Доберитесь до выхода"; break;
            }

            GameManager.instance.uiManager.onQuestChanged.Invoke();
        }
        private void ChangeMovementDirection()
        {
            if (Input.GetKey(KeyCode.W) && direction != Direction.Up)
            {
                direction = Direction.Up;
                ChangeRotation(-90);
            }

            if (Input.GetKey(KeyCode.S) && direction != Direction.Down)
            {
                direction = Direction.Down;
                ChangeRotation(90);
            }

            if (Input.GetKey(KeyCode.A) && direction != Direction.Left)
            {
                direction = Direction.Left;
                ChangeRotation(180);
            }

            if (Input.GetKey(KeyCode.D) && direction != Direction.Right)
            {
                direction = Direction.Right;
                ChangeRotation(0);
            }
        }
        private void ChangeRotation(float degree)
        {
            transform.rotation = Quaternion.Euler(0, degree, 0);
        }
        private void Move()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            transform.position += movement * 4f * Time.deltaTime;
        }
        private void Attack()
        {
            sword.StartAttackAnimation();
        }
        public void TakeDamage(int damage)
        {
            HP -= damage;

            if (HP <= 0)
            {
                GameManager.instance.onGameEnd.Invoke();
                return;
            }

            GameManager.instance.uiManager.onPlayerHealthChanged.Invoke();
        }
        public void PickUpKey()
        {
            isKeyPicked = true;
            SetQuest(1);
        }
        public void Quit()
        {
            if(isKeyPicked)
            {
                GameManager.instance.onGameEnd.Invoke();
            }
            else
            {
                Debug.Log("Нужно найти ключ!");
            }
        }

    }
}
