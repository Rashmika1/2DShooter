using UnityEngine;

/*
 * Source: ObjectCleaner.cs
 * Author:
 * Last Modified by: 
 * Date last Modified: 
 * Program description: Destroys all objects that crosses the boundaries of the game
 */

public class ObjectCleaner : MonoBehaviour {
    public void OnTriggerEnter2D(Collider2D collision) {
        Destroy(collision.gameObject);
    }
}
