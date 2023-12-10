using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator;

    // attacking
   // public Transform attackPoint;
   // public LayerMask enemyLayers;
  //  public float attackRange = 0.5f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    public void Attack()
    {
        animator.SetTrigger("attack");
    }
}
