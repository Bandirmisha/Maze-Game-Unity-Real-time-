
using MazeGame;
using UnityEngine;



public class Sword : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        //если атакует и объект находится на слое Enemy
        if (isAttaks() && collision.gameObject.layer == 6)
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(1);
        }
    }

    public void StartAttackAnimation()
    {
        GetComponent<Animator>().SetTrigger("Punch");
    }

    public bool isAttaks()
    {
        return GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("SwordPunch");
    }
}

