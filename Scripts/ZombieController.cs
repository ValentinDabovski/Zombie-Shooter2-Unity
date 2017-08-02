using System.Collections;
using UnityEngine;

using Random = System.Random;

namespace Assets.Scripts
{
    public class ZombieController : MonoBehaviour
    {
        public Transform Player;

        public float distanceToLookAtPlayer = 10f;
        public float distanceToEngage = 5f;
        public float zombieWalkingSpeed = 1;
        public float health = 100f;

        private Animator animator;

        private AudioManager audioManager;


        void OnEnable()
        {

            this.audioManager = FindObjectOfType<AudioManager>();
        }

        void Start()
        {
            this.animator = this.GetComponent<Animator>();

        }

        void Update()
        {

            LookAtPlayerAndMove();
        }

        public void TakeDamage(float damageAmount)
        {
            this.TakeDamageSound();

            health -= damageAmount;

            if (health <= 0)
            {
                Die();
            }
        }

        #region Enemy Die 

        private void Die()
        {
            this.audioManager.Play("ZombieDie");

            animator.SetBool("Die", true);

            var zombieScript = GetComponent<ZombieController>();

            zombieScript.enabled = false;

            Destroy(gameObject, 2);
        }

        #endregion


        #region Enemy animations and movement
        private void LookAtPlayerAndMove()
        {
            var playerPositionDistance = Vector3.Distance(this.Player.position, this.transform.position);

            if (playerPositionDistance < this.distanceToLookAtPlayer)
            {
                Vector3 direction = this.Player.position - this.transform.position;
                direction.y = 0;

                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.2f);

                animator.SetBool("Iddle", false);
                Debug.Log("enemy walking");

                if (direction.magnitude > distanceToEngage)
                {
                    
                    this.transform.Translate(0, 0, zombieWalkingSpeed * Time.deltaTime);
                    animator.SetBool("Walk", true);
                    animator.SetBool("Attack", false);

                }

                else
                {
                    this.AttackSound();
                    Debug.Log("enemy atacking");
                    animator.SetBool("Attack", true);
                    animator.SetBool("Walk", false);
                }

            }

            else
            {
                animator.SetBool("Iddle", true);
                animator.SetBool("Walk", false);
                animator.SetBool("Attack", false);
                animator.SetBool("Die", false);

            }

        }

        #endregion

        private void TakeDamageSound()
        {
            Random random = new Random();
            int soundToPlay = random.Next(0, 2);

            audioManager.Play(soundToPlay > 0 ? "ZombieTakeDamage1" : "ZombieTakeDamage2");
        }

        private void AttackSound()
        {
            Random random = new Random();
            int soundToPlay = random.Next(0, 2);

            audioManager.Play(soundToPlay > 0 ? "ZombieTalk1" : "ZombieTalk2");
        }
    }
}
