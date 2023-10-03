using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetris_Block: MonoBehaviour
{
    public Vector3 rotationPoint;
    public SpriteRenderer sprite;
    
    public void Destroy() {
        foreach (Transform children in this.transform) {
            Destroy(children.gameObject);
        }
    }
}
