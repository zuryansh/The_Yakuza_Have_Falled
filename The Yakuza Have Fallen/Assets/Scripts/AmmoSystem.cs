using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoSystem : MonoBehaviour
{
   
    public int currentAmmo;
    public int clipSize;
    public int clipCount;
    public int reloadSpeed;
    public bool isReloading;
    Animator animator;
    GunBase gun;
    public HealthBar ammoBar;

    public TextMeshProUGUI AmmoText;

    private void OnEnable()
    {
        ammoBar.SetMaxHealth(clipSize);
        ammoBar.SetHealth(currentAmmo);
    }

    private void Start()
    {
        currentAmmo = clipSize;
        animator = GetComponent<Animator>();
        gun = GetComponent<GunBase>();
        ammoBar.SetMaxHealth(clipSize);
    }

    private void OnDisable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    private void Update()
    {
        
        if (currentAmmo <= 0  && currentAmmo != clipSize && clipCount!=0 && !isReloading)
        {
            StartCoroutine(Reload());
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        AmmoText.text = $"{currentAmmo}/{clipCount}";
        ammoBar.SetHealth(currentAmmo);
    }

    public IEnumerator Reload()
    {
        animator.SetBool("Reloading",true);
        isReloading = true;
        
        yield return new WaitForSeconds(reloadSpeed);
        currentAmmo = clipSize;
        clipCount--;
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    //private void OnDestroy()
    //{
    //    //EventMangaer.current.OnGunFire -= UpdateCount;
    //}
}
