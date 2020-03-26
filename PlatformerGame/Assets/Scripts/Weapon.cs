using UnityEngine;

public class Weapon : MonoBehaviour {

	public float fireRate = 0;
	public float Damage = 10;
	public LayerMask whatToHit;

	public Transform BulletTrailPrefab;
	public Transform MuzzleFlashPrefab;
	float timeToSpawnEffect = 0;
	public float effectSpawnRate = 10;

	float timeToFire = 0;
	Transform firePoint;

	// Use this for initialization
	void Awake () {
		firePoint = transform.Find("FirePoint");
		if (firePoint == null)
        {
			Debug.LogError("No FirePoint found as a child of pistol");
        }

	}
	
	// Update is called once per frame
	void Update () {
		if (fireRate == 0)
        {
			if (Input.GetButtonDown ("Fire1"))
            {
				Shoot();
            }
        }
		else
        {
			if (Input.GetButton ("Fire1") && Time.time > timeToFire) 
				{
				timeToFire = Time.time + 1 / fireRate;
				Shoot();
            }
        }
	}

	void Shoot()
    {
		Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);        // storing firepoint on gun as vector 2
		RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatToHit);      //casting ray from then direction then distance then what not to hit
		if (Time.time >= timeToSpawnEffect)
        {
			Effect();
			timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
        }
		Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition) * 100, Color.cyan);
		if (hit.collider != null) // if something hits
        {
			Debug.DrawLine(firePointPosition, hit.point, Color.red);
			Debug.Log("We hit " + hit.collider.name + " and did " + Damage + " damage.");
        }
	
	}

	void Effect()
    {
		Instantiate(BulletTrailPrefab, firePoint.position, firePoint.rotation);
		Transform clone = Instantiate(MuzzleFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
		clone.parent = firePoint;
		float size = Random.Range(0.5f, 0.8f);
		clone.localScale = new Vector3 (size, size, size);
		Destroy (clone.gameObject, 0.02f);
    }
}
