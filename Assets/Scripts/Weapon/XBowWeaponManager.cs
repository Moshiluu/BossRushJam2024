using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Animator))]
public class XBowWeaponManager : MonoBehaviour
{
    [SerializeField]
    private float damage = 1;
    [SerializeField]
    private bool AddBulletSpread = true;
    [SerializeField]
    private Vector3 BulletSpreadVariance = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField]
    private ParticleSystem ShootingSystem;
    [SerializeField]
    private Transform bulletSpawnpoint;
    [SerializeField]
    private ParticleSystem ImpactParticleSystem;
    [SerializeField]
    private TrailRenderer BulletTrail;
    [SerializeField]
    private float ShootDelay = 0.5f;
    [SerializeField]
    private LayerMask Mask;
    [SerializeField]
    private GameObject gun;
    [SerializeField]
    private Vector3 target;
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private LayerMask enemyLayer;


    private Animator animator;
    private float LastShootTime;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit, 100f, enemyLayer) )
        {
            target = hit.point;
        }
        else
        {
            target = ray.GetPoint(100);
        }

        gun.transform.LookAt(target);


        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        if(LastShootTime + ShootDelay < Time.time)
        {
            animator.SetBool("IsShooting", true);
            ShootingSystem.Play();
            Vector3 direction = GetDirection();

            if (Physics.Raycast(bulletSpawnpoint.position, direction, out RaycastHit hit, float.MaxValue, Mask))
            {
                var enemy = hit.collider.gameObject.GetComponent<EnemyController>();
                if (enemy != null)
                {
                    enemy.health.HP -= damage;
                }
                TrailRenderer trail = Instantiate(BulletTrail, bulletSpawnpoint.position, Quaternion.identity);
                StartCoroutine(SpawnTrail(trail, hit));
                LastShootTime = Time.time;
            }
        }
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = transform.forward;

        if (AddBulletSpread)
        {
            direction += new Vector3(
                Random.Range(-BulletSpreadVariance.x, BulletSpreadVariance.x),
                 Random.Range(-BulletSpreadVariance.y, BulletSpreadVariance.y),
                  Random.Range(-BulletSpreadVariance.z, BulletSpreadVariance.z));
        }
        direction.Normalize();

        return direction;
    }
    
    private IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit)
    {
        float time = 0;
        Vector3 startPosition = trail.transform.position;

        while(time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
            time += Time.deltaTime / trail.time;
            yield return null;
        }
        animator.SetBool("IsShooting", false);
        trail.transform.position = hit.point;
        Instantiate(ImpactParticleSystem,hit.point, Quaternion.LookRotation(hit.normal));

        Destroy(trail.gameObject, trail.time);

    }
}
