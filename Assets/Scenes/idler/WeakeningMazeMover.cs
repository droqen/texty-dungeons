using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using navdi3;

public class WeakeningMazeMover : navdi3.maze.MazeBody
{
    public int hp = 8;
    override public void OnMoved(twin prev_pos, twin target_pos) {
        hp--;
        transform.localScale = Vector3.one * 2 * hp / 8f;
        if (hp <= 0)
        {
            Destroy(gameObject); // boom
        }
    }
}
