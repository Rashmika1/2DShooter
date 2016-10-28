using UnityEngine;
using System.Collections;

/*
 * Source: Asteroid.cs
 * Author:
 * Last Modified by: 
 * Date last Modified: 
 * Program description: Defines the behavior of an asteroid enemy
 */

public class Asteroid : Enemy {
    public override void Move() {
        rb2d.velocity = (new Vector2(-speed, 0));
    }
}
