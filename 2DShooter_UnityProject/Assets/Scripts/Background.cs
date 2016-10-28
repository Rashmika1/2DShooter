using UnityEngine;

/*
 * Source: Background.cs
 * Author:
 * Last Modified by: 
 * Date last Modified: 
 * Program description: Controls the scrolling of the background bi offseting the texture
 */
 
public class Background : MonoBehaviour {
    public float scrollSpeed;
    private Vector2 savedOffset;
    Renderer r;

    void Start() {
        r = GetComponent<Renderer>();
        savedOffset = r.sharedMaterial.GetTextureOffset("_MainTex");
    }

    void Update() {
        float x = Mathf.Repeat(Time.time * scrollSpeed, 1);
        Vector2 offset = new Vector2(x, savedOffset.y);
        r.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }

    void OnDisable() {
        r.sharedMaterial.SetTextureOffset("_MainTex", savedOffset);
    }
}
