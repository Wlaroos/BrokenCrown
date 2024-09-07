using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullets : MonoBehaviour
{
    [SerializeField] private GameObject ps;
    private Rigidbody2D _rb;

    private int _damage;
    private float _knockback;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // Will destroy the bullet after 8 seconds if it doesn't hit anything (Just in case)
        StartCoroutine(DestroyBullet(8.0f));   
    }

    public void BulletSetup(Vector3 shootDir, float angle, float shotSpeed, int damage, float knockback, float size)
    {
        _damage = damage;
        _knockback = knockback;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        
        // Size
        transform.localScale = new Vector3(size, size, size);

        // Angle/Direction
        transform.eulerAngles = new Vector3(0, 0, angle);

        // Speed
        float vel = shotSpeed;
        rb.AddForce(shootDir * vel, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        // If the bullet hits a wall
        if(collision.tag == "BulletBounds")
        {
            Destroy();
        }
        
        // If the bullet hits an enemy
        else if (collision.GetComponent<EnemyHealth>() != null)
        {
            collision.GetComponent<EnemyHealth>().TakeDamage(_rb.velocity.normalized * _knockback, _damage);
            Destroy();
        }
    }

    public IEnumerator DestroyBullet(float delay)
    {
        
        yield return new WaitForSeconds(delay);
        
        // Particles
        if(ps != null)
        {
            Instantiate(ps,transform.position,Quaternion.identity);
        }
        
        //CameraShaker.Instance.ShakeOnce(2f,2f,0.2f,0.2f);
        //AudioManager.PlaySound("PoisonBullet");

        Destroy(gameObject); 
    }

    public void Destroy()
    {
        StartCoroutine(DestroyBullet(0f));
    }

    /* 
            if (collision.GetComponent<Enemy>() != null && gameObject.name == "NormalBullet(Clone)")
            {
                Instantiate(ps, transform.position, Quaternion.identity);
                collision.GetComponent<Enemy>().TakeDamage(_rb.velocity.normalized * _knockback, _damage);
                Destroy(gameObject);
            }

            if (collision.gameObject.layer == LayerMask.NameToLayer("Walls"))
            {
                Instantiate(ps, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
    */
}
