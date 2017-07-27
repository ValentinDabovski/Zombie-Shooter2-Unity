using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class ZombieController : MonoBehaviour
    {

        public Transform Player;
        public float distanceToLookAtPlayer = 10f;
        public float distanceToEngage = 5f;
        public float zombieWalkingSpeed = 1;
        public float health = 100f;

        private static Animator Animator;
        void Start()
        {
            Animator = this.GetComponent<Animator>();
        }


        void Update()
        {
            LookAtPlayerAndMove();
        }

        public void TakeDamage(float damageAmount)
        {
            health -= damageAmount;

            if (health <= 0)
            {

                Die();

            }
        }

        private void Die()
        {
            Animator.SetBool("IsDying", true);

            Destroy(gameObject);
        }

        private void LookAtPlayerAndMove()
        {
            var playerPositionDistance = Vector3.Distance(this.Player.position, this.transform.position);

            if (playerPositionDistance < this.distanceToLookAtPlayer)
            {
                Vector3 direction = this.Player.position - this.transform.position;
                direction.y = 0;

                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.2f);

                Animator.SetBool("isIdle", false);
                if (direction.magnitude > distanceToEngage)
                {
                    this.transform.Translate(0, 0, zombieWalkingSpeed * Time.deltaTime);
                    Animator.SetBool("isRunning", true);
                    Animator.SetBool("isAttacking", false);
                }

                else
                {
                    Animator.SetBool("isAttacking", true);
                    Animator.SetBool("isRunning", false);
                }


            }

            else
            {
                Animator.SetBool("isIdle", true);
                Animator.SetBool("isRunning", false);
                Animator.SetBool("isAttacking", false);
                Animator.SetBool("isDying", false);
                Animator.SetBool("isTakingDamage", false);
            }

        }

    }
}
