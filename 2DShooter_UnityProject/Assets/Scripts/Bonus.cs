using UnityEngine;
using System.Collections;

/*
 * Source: Bonus.cs
 * Author:
 * Last Modified by: 
 * Date last Modified: 
 * Program description: Defines the behavior of an bonus object
 */

public class Bonus : MonoBehaviour {
    public int points = 50;
    public float speed = 2;
    Rigidbody2D rb2d;
    AudioSource audioSource;

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        rb2d.velocity = new Vector2(-speed, 0);
    }

    /// <summary>
    /// When collides with the player, adds score to it
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter2D(Collider2D collision) {
        Player des = collision.GetComponent<Player>();
        if(des != null) {
            GameManager.instance.AddScore(points);
            audioSource.Play();
            GetComponent<Renderer>().enabled = false;
            StartCoroutine(DestoyRoutine());
        }            
    }

    /// <summary>
    /// Destroys after playing the sfx
    /// </summary>
    /// <returns></returns>
    IEnumerator DestoyRoutine() {
        while(audioSource.isPlaying)
            yield return null;

        Destroy(gameObject);
    }
}
