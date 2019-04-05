using UnityEngine;

public class Gun : MonoBehaviour {

    public float damage = 10f;
    public float range = 200f;

    public Camera fpsCam;

    public ParticleSystem muzzleFlash;
    public GameObject impactEffect; 

    public AudioClip gunClip;
    public AudioSource gunSound;


	private void Start()
	{
        gunSound.clip = gunClip;	
    }
	// Update is called once per frame
	void Update () {
		
        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }

	}

    void Shoot(){

        muzzleFlash.Play();
        gunSound.Play();
        gunSound.pitch = Random.Range(0.85f, 0.95f);
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range )){
            Debug.Log(hit.transform.name);

            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null){
                enemy.TakeDamage(damage);
            }
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 5f);
        }

    }
}
