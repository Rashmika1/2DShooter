using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
 * Source: Player.cs
 * Author:
 * Last Modified by: 
 * Date last Modified: 
 * Program description: Class that handles the player
 */

public class Player : MonoBehaviour, IDestroyable {
    public float steerSpeed = 1f;
    public float shootDelay = .5f;
    public int life = 10;
    protected float totalLife;
    public Transform cannonTip;
    public GameObject missilePrefab;
    Vector2 lowerBoundary;
    Vector2 upperBoundary;
    [SerializeField]
    Slider lifeMeter;   //the life meter UI element

    Rigidbody2D rb2d;
    AudioSource audioSource;
    Animator anim;

    float horizontalInput;
    float verticalInput;
    bool canShoot = true;

    // Use this for initialization
    void Start () {
        totalLife = life;
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        StartCoroutine(EnableShoot());
        float size = GetComponent<Renderer>().bounds.size.magnitude / 2;
        Vector2 sizeWorld = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight));
        sizeWorld -= new Vector2(size, size);
        lowerBoundary = -sizeWorld;
        upperBoundary = sizeWorld;
    }
	
	// Update is called once per frame
	void Update () {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        if(Input.GetKeyDown(KeyCode.Space) && canShoot)
            Shoot();
    }

    // FixedUpdate is called once per time step
    void FixedUpdate() {
        rb2d.velocity = new Vector2(horizontalInput, verticalInput) * steerSpeed;
        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, lowerBoundary.x, upperBoundary.x),
            Mathf.Clamp(transform.position.y, lowerBoundary.y, upperBoundary.y));
    }

    /// <summary>
    /// Instantiates a shot prefab at cannon tip, that will go straight forward
    /// </summary>
    public void Shoot() {
        var instance = Instantiate(missilePrefab);
        instance.transform.position = cannonTip.position;
        instance.SetActive(true);
        canShoot = false;
    }

    /// <summary>
    /// Set the explosion animation and starts the DestroyMeRoutine
    /// </summary>
    public void DestroyMe() {
        Debug.Log("I was destroyed!!!");
        anim.SetBool("Explode", true);
        audioSource.Play();
        StartCoroutine(DestroyMeRoutine());
    }

    /// <summary>
    /// Apply 2 of damage when collides with an IDestroyable
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter2D(Collider2D collision) {
        IDestroyable des = collision.GetComponent<IDestroyable>();
        if(des != null)
            des.DestroyMe(2);
    }

    /// <summary>
    /// Destroys the gameobject after the sound is played
    /// </summary>
    /// <returns></returns>
    IEnumerator DestroyMeRoutine() {
        while(audioSource.isPlaying)
            yield return null;

        GameManager.instance.GameOver();
        Destroy(gameObject);
    }

    /// <summary>
    /// Reenables the shooting each shootDelay time...
    /// </summary>
    /// <returns></returns>
    IEnumerator EnableShoot() {
        while(true) {
            yield return new WaitForSeconds(shootDelay);
            canShoot = true;
        }
    }

    /// <summary>
    /// Method called when the player takes damage
    /// </summary>
    /// <param name="damage"></param>
    public void DestroyMe(int damage) {
        if(life > damage) {
            life -= damage;
            lifeMeter.value = life / totalLife;
        } else {
            lifeMeter.value = 0;
            StopAllCoroutines();
            DestroyMe();
        }
    }
}
