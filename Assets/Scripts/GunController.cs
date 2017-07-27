using Assets.Scripts;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public float fireRate = 1f;
    public float fireRange = 5f;
    public float damage = 10f;

    public Camera FpsCamera;

    private bool isReloading = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }


    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(FpsCamera.transform.position, FpsCamera.transform.forward, out hit, fireRange))
        {
            if (hit.transform.tag.Equals("enemy"))
            {
                var zombie = hit.transform.GetComponent<ZombieController>();

                if (zombie != null)
                {
                    var animator = zombie.GetComponent<Animator>();
                   
                    zombie.TakeDamage(damage);
                  
                }

            }
        }



    }
}
