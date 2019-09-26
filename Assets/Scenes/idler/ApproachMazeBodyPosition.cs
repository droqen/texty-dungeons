using UnityEngine;
using System.Collections;

public class ApproachMazeBodyPosition : MonoBehaviour
{
    navdi3.maze.MazeBody body { get { return GetComponent<navdi3.maze.MazeBody>(); } }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = transform.position * 0.8f + 0.2f * body.master.tilemap.GetCellCenterWorld(body.my_cell_pos);
    }
}
