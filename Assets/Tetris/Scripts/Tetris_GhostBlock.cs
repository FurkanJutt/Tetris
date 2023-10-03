using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetris_GhostBlock : Tetris_Block
{
    internal bool IsDestroyed() {
        return transform.childCount == 0;
    }
}
