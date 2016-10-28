using UnityEngine;
using System.Collections;

/*
 * Source: Boss.cs
 * Author:
 * Last Modified by: 
 * Date last Modified: 
 * Program description: Defines the Boss enemy behavior
 */

public class Boss : Enemy {
    [SerializeField]
    Beam[] beams;
    public float curveMovementHeight = 3f;
    public float shootDelay = 2.0f;

    public float leftBoundary;
    public float rightBoundary;
    public float shootRandomization = 1f;

    SpriteRenderer sr;
    bool isLeft = true;
    float dir = -1;

    protected override void Start() {
        FlipBeams();
        base.Start();
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(ShootRoutine());
    }

    /// <summary>
    /// Inverts the beam direction
    /// </summary>
    void FlipBeams() {
        foreach(var b in beams)
            b.speed *= -1;
    }

    protected override void DestroyMe() {
        base.DestroyMe();
        GameManager.instance.BossDefeated();
    }

    public override void Move() {
        rb2d.velocity = new Vector2(dir * speed, curveMovementHeight * Mathf.Sin(Time.time));
        if((transform.position.x < leftBoundary && isLeft) || (transform.position.x > rightBoundary && !isLeft))
            Flip();
    }

    /// <summary>
    /// Inverts the speed direction, and also inverts the transform
    /// </summary>
    void Flip() {
        speed += .1f;
        dir *= -1;
        transform.localScale = new Vector3(transform.localScale.x*-1, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        isLeft = !isLeft;
        FlipBeams();
    }

    /// <summary>
    /// Shoots every "shootDelay" seconds
    /// </summary>
    /// <returns></returns>
    IEnumerator ShootRoutine() {
        while(true) {
            yield return new WaitForSeconds(Random.Range(shootDelay - shootRandomization, shootDelay + shootRandomization));

            foreach(var beam in beams) {
                GameObject beamInstance = Instantiate(beam.gameObject);
                beamInstance.transform.position = beam.transform.position;
                beamInstance.SetActive(true);
            }            
        }
    }
}
