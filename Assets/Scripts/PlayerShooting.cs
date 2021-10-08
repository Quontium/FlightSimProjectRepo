using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    public float shootingPower;
    public int maxAmmoCapacity;
    int currentAmmoInTurret;
    public float reloadTime;
    bool reloading;
    [SerializeField] Transform Turret;
    private void Start()
    {
        currentAmmoInTurret = maxAmmoCapacity;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!reloading)
            {
                shootBullet();
                currentAmmoInTurret--;
                if(currentAmmoInTurret <= 0)
                {
                    reloading = true;
                    StartCoroutine(reloadTurrets());
                }
            }
        }
    }
    IEnumerator reloadTurrets()
    {
        yield return new WaitForSeconds(reloadTime);
        reloading = false;
        currentAmmoInTurret = maxAmmoCapacity;
    }
    void shootBullet()
    {
        GameObject Instance = Instantiate(bulletPrefab, Turret.position, Turret.rotation);
        Instance.GetComponent<Rigidbody>().AddForce(shootingPower * Vector3.forward);
        Bullet bullet = bulletPrefab.GetComponent<Bullet>();
        bullet.Shooter = gameObject;
        bullet.paperPiercing = shootingPower;
    }
}
