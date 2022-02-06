using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class melleWeaponScript : MonoBehaviour
{
    public int damage;
    public float swingTime;
    public bool isSwinging;
    Animator animator;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        GetComponent<Collider>().enabled = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1")&& !isSwinging)
            StartCoroutine(SwingWeapon());
    }

    IEnumerator SwingWeapon()
    {
        animator.SetTrigger("Attack");
        CameraShaker.Instance.ShakeOnce(4f, 3f, 0.1f, 0.3f);
        isSwinging = true;
        GetComponent<Collider>().enabled = true;
        
        yield return new WaitForSeconds(swingTime);
        GetComponent<Collider>().enabled = false;
        isSwinging = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyBase>()!= null)
            other.GetComponent<EnemyBase>().TakeDamage(damage);
    }
}
