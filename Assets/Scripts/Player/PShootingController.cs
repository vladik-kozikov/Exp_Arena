
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PShootingController : MonoBehaviour
{
    public int DamagePlayer;

    public bool AddBulletSpread = true;

    public float damage;

    public Vector3 BulletSpreadVariance = new Vector3(0f, 0f, 0f);
    public Transform directionPoint;
    public ParticleSystem ShootingSystem;

    public Transform BulletSpawnPoint;

    public ParticleSystem ImpactParticleSystem;

    public TrailRenderer BulletTrail;

    public float ShootDelay = 0.5f;

    public LayerMask Mask;

    public float BulletSpeed = 100;


    public AudioSource source;
    private Animator Animator;
    private float LastShootTime;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    public void Shoot()
    {
        if (LastShootTime + ShootDelay < Time.time && gameObject.GetComponent<PMoveController>().weapon.ammunitionCurrent >0)
        {


            //Animator.SetBool("IsShooting", true);
            ShootingSystem.Play();
            //source.Play();
            Vector3 direction = GetDirection();

            if (Physics.Raycast(BulletSpawnPoint.position, direction-BulletSpawnPoint.position, out RaycastHit hit, float.MaxValue, Mask))
            {
                TrailRenderer trail = Instantiate(BulletTrail, BulletSpawnPoint.position, Quaternion.identity);

                StartCoroutine(SpawnTrail(trail, hit.point, hit.normal, true));

                LastShootTime = Time.time;
            }

            else
            {
                TrailRenderer trail = Instantiate(BulletTrail, BulletSpawnPoint.position, Quaternion.identity);

                StartCoroutine(SpawnTrail(trail, directionPoint.position+(directionPoint.position-BulletSpawnPoint.position)*100f, Vector3.zero, false));

                LastShootTime = Time.time;
            }
        }
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = directionPoint.position;
        if (AddBulletSpread)
        {
            direction += new Vector3(
                Random.Range(-BulletSpreadVariance.x, BulletSpreadVariance.x),
                Random.Range(-BulletSpreadVariance.y, BulletSpreadVariance.y),
                Random.Range(-BulletSpreadVariance.z, BulletSpreadVariance.z)
            );
            direction.Normalize();
            
        }

        return direction;
    }

    private IEnumerator SpawnTrail(TrailRenderer Trail, Vector3 HitPoint, Vector3 HitNormal, bool MadeImpact)
    {

        Vector3 startPosition = Trail.transform.position;
        float distance = Vector3.Distance(Trail.transform.position, HitPoint);
        float remainingDistance = distance;

        while (remainingDistance > 0)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, HitPoint, 1 - (remainingDistance / distance));

            remainingDistance -= BulletSpeed * Time.deltaTime;

            yield return null;
        }
        //Animator.SetBool("IsShooting", false);
        Trail.transform.position = HitPoint;
        if (MadeImpact)
        {
           ParticleSystem impact = Instantiate(ImpactParticleSystem, HitPoint, Quaternion.LookRotation(HitNormal));


        }

        Destroy(Trail.gameObject, Trail.time);
    }
}
