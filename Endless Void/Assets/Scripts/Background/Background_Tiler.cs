using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_Tiler : MonoBehaviour {

    public Transform backgroundTile;
    public Transform targetCamera;
    public Vector3[] offsetMatrix;

    public Transform[] activeTiles;

    public float offset; 
	
    // Use this for initialization
	void Start () {
        offsetMatrix[0] = new Vector3(-offset, -10, -offset);
        offsetMatrix[1] = new Vector3(0, -10, -offset);
        offsetMatrix[2] = new Vector3(offset, -10, -offset);

        offsetMatrix[3] = new Vector3(-offset, -10, 0);
        offsetMatrix[4] = new Vector3(0, -10, 0);
        offsetMatrix[5] = new Vector3(offset, -10, 0);

        offsetMatrix[6] = new Vector3(-offset, -10, offset);
        offsetMatrix[7] = new Vector3(0, -10, offset);
        offsetMatrix[8] = new Vector3(offset, -10, offset);

        UpdateTiles(0);
    }
	
	// Update is called once per frame
	void Update () {
        float distance_x = targetCamera.localPosition.x - transform.localPosition.x;
        float distance_z = targetCamera.localPosition.y - transform.localPosition.z;

        if(Mathf.Abs(distance_x) > offset)
        {
            if(distance_x > offset) // Gone over right border
            {
                Vector3 currentPosition = transform.localPosition;
                currentPosition.x += offset;
                transform.localPosition = currentPosition;
                UpdateTiles(2);
            }
            else // Gone over left border
            {
                Vector3 currentPosition = transform.localPosition;
                currentPosition.x -= offset;
                transform.localPosition = currentPosition;
                UpdateTiles(4);
            }
        }

        if(Mathf.Abs(distance_z) > offset)
        {
            if (distance_z > offset) // Gone over top border
            {
                Vector3 currentPosition = transform.localPosition;
                currentPosition.z += offset;
                transform.localPosition = currentPosition;
                UpdateTiles(1);
            }
            else // Gone over bottom border
            {
                Vector3 currentPosition = transform.localPosition;
                currentPosition.z -= offset;
                transform.localPosition = currentPosition;
                UpdateTiles(3);
            }
        }
    }

    void UpdateTiles(int direction) // 1 = up, 2 = right, 3 = down, 4 = left, 0 = fresh
    {
        if (direction == 0) // Fresh
        {
            int index = 0;
            foreach (Vector3 position in offsetMatrix)
            {
                activeTiles[index] = Instantiate(backgroundTile, (position + transform.localPosition), Quaternion.identity);
                index++;
            }
        }

        else if (direction == 1) // Up
        {
            // Left row
            Destroy(activeTiles[0].gameObject);
            activeTiles[0] = activeTiles[3];
            activeTiles[3] = activeTiles[6];
            activeTiles[6] = Instantiate(backgroundTile, (offsetMatrix[6] + transform.localPosition), Quaternion.identity);

            // Middle row
            Destroy(activeTiles[1].gameObject);
            activeTiles[1] = activeTiles[4];
            activeTiles[4] = activeTiles[7];
            activeTiles[7] = Instantiate(backgroundTile, (offsetMatrix[7] + transform.localPosition), Quaternion.identity);

            // Right row
            Destroy(activeTiles[2].gameObject);
            activeTiles[2] = activeTiles[5];
            activeTiles[5] = activeTiles[8];
            activeTiles[8] = Instantiate(backgroundTile, (offsetMatrix[8] + transform.localPosition), Quaternion.identity);
        }

        else if (direction == 2) // Right
        {
            // Bottom row
            Destroy(activeTiles[0].gameObject);
            activeTiles[0] = activeTiles[1];
            activeTiles[1] = activeTiles[2];
            activeTiles[2] = Instantiate(backgroundTile, (offsetMatrix[2] + transform.localPosition), Quaternion.identity);

            // Middle row
            Destroy(activeTiles[3].gameObject);
            activeTiles[3] = activeTiles[4];
            activeTiles[4] = activeTiles[5];
            activeTiles[5] = Instantiate(backgroundTile, (offsetMatrix[5] + transform.localPosition), Quaternion.identity);

            // Top row
            Destroy(activeTiles[6].gameObject);
            activeTiles[6] = activeTiles[7];
            activeTiles[7] = activeTiles[8];
            activeTiles[8] = Instantiate(backgroundTile, (offsetMatrix[8] + transform.localPosition), Quaternion.identity);
        }

        else if (direction == 3) // Down
        {
            // Left row
            Destroy(activeTiles[6].gameObject);
            activeTiles[6] = activeTiles[3];
            activeTiles[3] = activeTiles[0];
            activeTiles[0] = Instantiate(backgroundTile, (offsetMatrix[0] + transform.localPosition), Quaternion.identity);

            // Middle row
            Destroy(activeTiles[7].gameObject);
            activeTiles[7] = activeTiles[4];
            activeTiles[4] = activeTiles[1];
            activeTiles[1] = Instantiate(backgroundTile, (offsetMatrix[1] + transform.localPosition), Quaternion.identity);

            // Right row
            Destroy(activeTiles[8].gameObject);
            activeTiles[8] = activeTiles[5];
            activeTiles[5] = activeTiles[2];
            activeTiles[2] = Instantiate(backgroundTile, (offsetMatrix[2] + transform.localPosition), Quaternion.identity);
        }

        else if(direction == 4) // Left
        {
            // Bottom row
            Destroy(activeTiles[2].gameObject);
            activeTiles[2] = activeTiles[1];
            activeTiles[1] = activeTiles[0];
            activeTiles[0] = Instantiate(backgroundTile, (offsetMatrix[0] + transform.localPosition), Quaternion.identity);

            // Middle row
            Destroy(activeTiles[5].gameObject);
            activeTiles[5] = activeTiles[4];
            activeTiles[4] = activeTiles[3];
            activeTiles[3] = Instantiate(backgroundTile, (offsetMatrix[3] + transform.localPosition), Quaternion.identity);

            // Top row
            Destroy(activeTiles[8].gameObject);
            activeTiles[8] = activeTiles[7];
            activeTiles[7] = activeTiles[6];
            activeTiles[6] = Instantiate(backgroundTile, (offsetMatrix[6] + transform.localPosition), Quaternion.identity);
        }
    }
}
