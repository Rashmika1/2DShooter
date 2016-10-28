using UnityEngine;
using System.Collections;

/*
 * Source: Enemy.cs
 * Author:
 * Last Modified by: 
 * Date last Modified: 
 * Program description: Defines the behavior of an generic enemy
 */

public abstract class Enemy : MonoBehaviour, IDestroyable {
    [SerializeField]
    protected float speed = 2f;
    [SerializeField]
    protected int damagePower = 1;
    [SerializeField]
    protected int life = 1;

    public float speedVariation = 1f;
    public int points = 10;
    protected AudioSource audioSource;
    protected Animator anim;
    protected Rigidbody2D rb2d;
    protected Collider2D col;

    // Use this for initialization
    protected virtual void Start() {
        speed += Random.Range(-speedVariation, speedVariation);
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    void FixedUpdate() {
        Move();
    }

    /// <summary>
    /// Activates all the effects when is destroyed
    /// </summary>
    virtual protected void DestroyMe() {
        StopAllCoroutines();
        col.enabled = false;
        Debug.Log("Enemy was destroyed");
        audioSource.Play();
        anim.SetBool("Explode", true);
        StartCoroutine(DestroyMeRoutine());
    }

    /// <summary>
    /// On collision, apply damage to an IDestroyable
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter2D(Collider2D collision) {
        IDestroyable des = collision.GetComponent<IDestroyable>();
        if(des != null)
            des.DestroyMe(damagePower);        
    }


    /// <summary>
    /// Destroys the gameobject after the sound is played
    /// </summary>
    /// <returns></returns>
    protected IEnumerator DestroyMeRoutine() {
        while(audioSource.isPlaying)
            yield return null;

        GameManager.instance.AddScore(points);
        Destroy(gameObject);
    }

    /// <summary>
    /// Each kind of enemy will have its own Move method, called on each time step
    /// </summary>
    public abstract void Move();

    public void DestroyMe(int damage) {
        if(life > damage) {
            life -= damage;
        } else {
            StopAllCoroutines();
            DestroyMe();
        }
    }
}
