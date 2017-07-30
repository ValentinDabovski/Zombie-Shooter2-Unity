using System.Collections;
using UnityEngine;


namespace Assets.Scripts
{
    public class GunController : MonoBehaviour
    {

        public Camera FpsCamera;
        public ParticleSystem MuzzleFlash;
        public Animator Animator;
        public AudioClip WeaponShotAudio;
        public AudioClip WeaponReloadAudio;
     


        public float WeaponDamage = 10f;
        public float WeapomRamge = 100f;
        public float WeaponImapctForce = 10f;
        public float WeaponFireRate = 15;
        public int WeaponMaxAmmo = 8;
        public float WeaponReloadTime = 2.5f;



        private AudioSource audioSource;

        private int currentAmmo;
        private float nextTimeToFire = 0f;
        private bool isReloading = false;
        private bool isFiring = false;


    

        
        void Start()
        {
            this.audioSource = GetComponent<AudioSource>();
            currentAmmo = this.WeaponMaxAmmo;
          
        }


        void OnEnable()
        {
            this.isReloading = false;
            this.Animator.SetBool("Reloading", false);


        }

        void Update()
        {

            if (isFiring)
            {
                this.Animator.SetBool("Shooting", false);
            }

            if (isReloading)
            {
               
                this.Animator.SetBool("Iddle", false);
                return;
            }

            if (this.currentAmmo <= 0)
            {
                this.audioSource.PlayOneShot(this.WeaponReloadAudio);
                StartCoroutine(this.Reload());
                return;
            }

            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                this.audioSource.PlayOneShot(this.WeaponShotAudio, 1f);
                this.nextTimeToFire = Time.time + 1 / WeaponFireRate;
                StartCoroutine(this.Shoot());
            }

        }


        private IEnumerator Reload()
        {
           
            isReloading = true;

            Debug.Log("Reloading");

            this.Animator.SetBool("Reloading", true);

            yield return new WaitForSeconds(this.WeaponReloadTime - .02f);

            this.Animator.SetBool("Reloading", false);

            yield return new WaitForSeconds(.02f);

            this.currentAmmo = this.WeaponMaxAmmo;

            this.Animator.SetBool("Iddle", true);

            isReloading = false;
        }


        private IEnumerator Shoot()
        {
            this.Animator.SetBool("Iddle", true);

            isFiring = true;

            this.Animator.SetBool("Shooting", true);


            yield return new WaitForSeconds(.10f);

            this.MuzzleFlash.Play();

            this.currentAmmo--;

            RaycastHit hit;

            bool rayShotHitSomething = Physics.Raycast(this.FpsCamera.transform.position, FpsCamera.transform.forward, out hit, WeapomRamge);

            if (rayShotHitSomething)
            {
                //  Debug.Log(hit.transform.tag);

                var target = hit.transform.GetComponent<ZombieController>();

                if (target != null)
                {
                    target.TakeDamage(WeaponDamage);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * WeaponImapctForce);
                }

                // TODO: hit effect
            }

            this.Animator.SetBool("Shooting", false);

            yield return new WaitForSeconds(.10f);

            isFiring = false;

        }
    }
}
