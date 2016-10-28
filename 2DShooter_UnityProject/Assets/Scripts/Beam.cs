using UnityEngine;

/*
 * Source: Beam.cs
 * Author:
 * Last Modified by: 
 * Date last Modified: 
 * Program description: Controls the behavior of a beam fired by the player or an enemy
 */
 
public class Beam : MonoBehaviour {
    public float speed = 10f;
    public int damagePower = 2;
    Rigidbody2D rb2d;

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(speed, 0);
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        IDestroyable destroyable = collision.GetComponent<IDestroyable>();

        if(destroyable != null) {
            destroyable.DestroyMe(damagePower);
            Destroy(gameObject);
        }
    }
}
