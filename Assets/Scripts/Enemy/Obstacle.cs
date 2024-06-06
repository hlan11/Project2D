using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private int point;
    public int GetObstaclePoint()
    {
        return point;
    }
    public void SetObstaclePoint(int point)
    {
        this.point = point;
    }
}
