using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class AutomaticGun : MonoBehaviour
{
    public float offset;
    public GameObject bullet;
    public Transform shotPoint;


    private float timeBtwShots;
    public float startTimeBtwShots;

    public static SpriteRenderer sr;


    private float timerOverheating;
    public float StartTimeOverheating = 0.5f;


    // ======================== Ammo ================================

    public GameObject ammo;


    public int currentAmmo = 35;
    public int full = 35;

    public Text ammoCount;

    private Animator anim;


    // ==============================================================

    public AudioSource GetGunSound;
    public AudioSource ReloadSound;
    public AudioSource ShotSound;

    private void Start()
    {      
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (gameObject.transform.parent != null)
            {
                GetGunSound.enabled = true;
                GameObject ParentHands = gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject;
                anim = ParentHands.GetComponent<Animator>();
                ammoCount = ParentHands.GetComponent<HandsAutomaticGun>().AmmoCountText;


                OutText();

                if (timeBtwShots <= 0 && currentAmmo > 0)
                {

                    if (Input.GetMouseButton(0) && gameObject.transform.parent != null && !anim.GetBool("reload"))
                    {
                        ShotSound.Play();
                        Quaternion bulletrot = transform.rotation;


                        if (timerOverheating < StartTimeOverheating)
                        {
                            timerOverheating += Time.deltaTime * 10;
                        }
                        else if (timerOverheating >= StartTimeOverheating)
                        {
                            anim.SetBool("Overheating", true);
                        }


                        Instantiate(bullet, shotPoint.position, bulletrot);
                        timeBtwShots = startTimeBtwShots;
                        currentAmmo -= 1;
                        anim.SetBool("Shot", true);
                        shotPoint.GetComponent<Light2D>().intensity = 10;
                        Invoke("offLight", 0.05f);
                    }
                    else
                    {
                        timerOverheating = 0;
                        anim.SetBool("Overheating", false);

                        anim.SetBool("Shot", false);
                    }
                }
                else
                {
                    timeBtwShots -= Time.deltaTime;
                }

                if (currentAmmo == 0)
                {
                    anim.SetBool("Shot", false);
                }


                // ======================== Ammo ================================

                if (Input.GetKeyDown(KeyCode.R) && Player.automaticGun_ammo > 0 && currentAmmo < 35)
                {
                    anim.SetBool("reload", true);
                    ReloadSound.Play();
                }
            }
            else
            {
                GetGunSound.enabled = false;
            }

        }
    }

    public void Reload()
    {
        int reason = 35 - currentAmmo;
        if (Player.automaticGun_ammo >= reason)
        {
            Player.automaticGun_ammo -= reason;
            currentAmmo = 35;
        }
        else
        {
            currentAmmo = currentAmmo + Player.automaticGun_ammo;
            Player.automaticGun_ammo = 0;
        }

    }

    public void OutText()
    {
        ammoCount.text = currentAmmo + "/" + Player.automaticGun_ammo;
    }


    // ==============================================================


    public void offLight()
    {
        shotPoint.GetComponent<Light2D>().intensity = 0;
    }
}
