using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtirarComArma : MonoBehaviour
{
    [Header("Controller Bullet and Ammo")]
    public int Bullet = 15;
    public int Ammo = 120;
    public int maxBullet = 15;
    public AudioClip ShootSound;

    public GameObject tiroBala;
    public GameObject canoDaArma;

    [Header("Controller Canvas")]
    public Text bulletText;
    public Text ammoText;
    public GameObject ReloadingText;
    public GameObject NoAmmoRun;
    public GameObject needReload;

    private float timeToShoot = 0.3f;
    private float timeToReloading = 1.5f;

    private bool canShoot;
    private bool reloading;
    private float initialTimeToShoot;
    private float initialTimeToReloading;

    public void Start()
    {
        this.initialTimeToReloading = this.timeToReloading;
        this.initialTimeToShoot = this.timeToShoot;

    }

    void Update()
    {
        bulletText.text = "Bullet: " + Bullet.ToString();
        ammoText.text = "Ammo: " + Ammo.ToString();

        if (Input.GetButtonDown("Fire1") && this.Bullet > 0 && this.canShoot && !this.reloading)
        {
            Instantiate(tiroBala, canoDaArma.transform.position, canoDaArma.transform.rotation);
            this.Bullet--;
            AudioController.currentAdioSource.PlayOneShot(ShootSound);
            canShoot = false;
        }
        if (!canShoot)
        {
            this.timeToShoot -= Time.deltaTime;
            if(this.timeToShoot <= 0)
            {
                this.canShoot = true;
                this.timeToShoot = this.initialTimeToShoot;
            }
        }
        

        if (Input.GetKeyDown(KeyCode.R) && this.Ammo > 0 && this.Bullet < this.maxBullet)
        {
            this.reloading = true;
        }
        if (this.reloading)
        {
            this.ReloadingText.SetActive(true);
            this.timeToReloading -= Time.deltaTime; 
            if(timeToReloading <= 0)
            {
                
                int currentValue = this.maxBullet - this.Bullet;
                if (currentValue >= this.Ammo)
                {
                    this.Bullet += Ammo;
                    Ammo = 0;
                }
                else
                {
                    this.Bullet += currentValue;
                    this.Ammo -= currentValue;
                }

                this.reloading = false;
                this.timeToReloading = this.initialTimeToReloading;
            }
            
        }
        else
        {
            this.ReloadingText.SetActive(false);
        }

        bool Active = Bullet <= 0 ? true : false;
        this.needReload.SetActive(Active);

        if (this.Bullet <= 0 && this.Ammo <= 0)
        {
            this.NoAmmoRun.SetActive(true);
            this.needReload.SetActive(false);
        }
        else
        {
            this.NoAmmoRun.SetActive(false);
        }
    }
}
