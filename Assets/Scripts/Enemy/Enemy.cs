using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

namespace MazeGame
{
    public class Enemy : MonoBehaviour, IEnemy
    {
        private int HP;
        protected bool alive;
        private bool isFollowsPlayer;
        private bool canAttack;

        [SerializeField] private float attackCooldown;
        private float currentTime;

        [SerializeField] private NavMeshAgent navMeshAgent;
        [HideInInspector] private Transform moveTargetTransform;

        public Slider healthBar;

        private void Start()
        {
            HP = 5;
            alive = true;
            canAttack = true;

            healthBar.value = HP;

            GetRandomTarget();
            Move();
        }

        protected virtual void FixedUpdate()
        {
            if(!alive)
            {
                navMeshAgent.enabled = false;

                //Проваливание под землю
                if (transform.position.y > -10f)
                    transform.position += new Vector3(0, -0.1f, 0);
                else Destroy(this.gameObject);

                return;
            }


            if(MustChangeTarget())
            {
                GetRandomTarget();
                Move();
            }
            else
            {
                if (isFollowsPlayer)
                {
                    //обновление таргет позиции (так как положение игрока меняется постоянно)
                    FollowPlayer();
                }
            }


            if(!canAttack)
            {
                currentTime += Time.deltaTime;
                if (currentTime >= attackCooldown)
                {
                    currentTime = 0;
                    canAttack = true;
                }
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.layer == 7)
            {
                FollowPlayer();

                if (canAttack)
                    Attack();
            }
        }
        
        private bool MustChangeTarget()
        {
            if (isFollowsPlayer)
                return false;

            var distance = Vector3.Distance(transform.position, moveTargetTransform.position);
            if (distance > 1f)
            {
                return false;
            }
            
            return true;
        }

        private void GetRandomTarget()
        {
            int randIndex = Random.Range(0, GameManager.instance.floors.Count);
            moveTargetTransform = GameManager.instance.floors[randIndex].transform;
        }

        public void FollowPlayer()
        {
            isFollowsPlayer = true;
            moveTargetTransform = GameManager.instance.player.transform;
            Move();
        }

        public void Move()
        {
            navMeshAgent.SetDestination(moveTargetTransform.position);
        }

        public void Attack()
        {
            canAttack = false;
            GameManager.instance.player.GetComponent<Player>().TakeDamage(10);
        }

        public void TakeDamage(int damage)
        {
            HP -= damage;
            healthBar.value = HP;
            
            if (HP <= 0)
            {
                alive = false;
                return;
            }

            FollowPlayer();

            //отбрасывание
            Vector3 dir = transform.position - GameManager.instance.player.transform.position;
            GetComponent<Rigidbody>().AddForce(dir * 150);   
        }
    }
}
