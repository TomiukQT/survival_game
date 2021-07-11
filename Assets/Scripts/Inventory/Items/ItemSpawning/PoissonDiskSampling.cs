using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PoissonDiskSampling : Singleton<PoissonDiskSampling>
{

    [SerializeField] private int _rejectionConstant;
    [SerializeField] private float _minDistance;
    [SerializeField] private Vector2 _regionSize;
    [SerializeField] private Vector2 _regionCenter;

    private const float CELL_SIZE = 1f;

    [SerializeField] private float _displayRadius;
    private List<Vector2> _points;
    
    public void Start()
    {
        _points = GetPoints();
    }

    public List<Vector2> GetPoints()
    {
        float cellSize = _minDistance / Mathf.Sqrt(2);
        //Step0
        int[,] grid = new int[Mathf.CeilToInt(_regionSize.x / cellSize), Mathf.CeilToInt(_regionSize.y / cellSize)];
        //InitGrid(ref grid);
        //Step1
        //Vector2 x0 = new Vector2(Random.Range(0, _regionSize.x), Random.Range(0, _regionSize.y));
        //grid[Mathf.FloorToInt(x0.x), Mathf.FloorToInt(x0.y)] = 0;
        List<Vector2> spawnPoints = new List<Vector2>();
        List<Vector2> points = new List<Vector2>();

        spawnPoints.Add(_regionSize/2);
        while (spawnPoints.Count > 0)
        {
            int spawnIndex = Random.Range(0, spawnPoints.Count);
            Vector2 spawnCenter = spawnPoints[spawnIndex];
            bool candidateAccepted = false;
            
            for (int i = 0; i < _rejectionConstant; i++)
            {
                float angle = Random.value * 2 * Mathf.PI;
                Vector2 dir = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
                Vector2 candidate = spawnCenter + dir * Random.Range(_minDistance, 2 * _minDistance);
                if (IsValid(candidate, _regionSize, cellSize, _minDistance,points,grid))
                {
                    points.Add(candidate);
                    spawnPoints.Add(candidate);
                    grid[(int) (candidate.x / cellSize), (int) (candidate.y / cellSize)] = points.Count;
                    candidateAccepted = true;
                    break;
                }
            }
            if(!candidateAccepted)
                spawnPoints.RemoveAt(spawnIndex);
        }

        return points;
    }
    

    private void InitGrid(ref int [,] grid)
    {
        for (int i = 0; i < grid.GetLength(0); i++)
            for (int j = 0; j < grid.GetLength(1); j++)
                grid[i, j] = -1;
    }

    private bool IsValid(Vector2 candidate, Vector2 regionSize, float cellSize, float radius, List<Vector2> points,
        int[,] grid)
    {
        if (candidate.x < 0 || candidate.x >= regionSize.x || candidate.y < 0 || candidate.y >= regionSize.y)
            return false;

        int cellX = (int) (candidate.x / cellSize);
        int cellY = (int) (candidate.y / cellSize);
        int searchStartX = Mathf.Max(0, cellX - 2);
        int searchEndX = Mathf.Min(cellX + 2, grid.GetLength(0) - 1);
        int searchStartY = Mathf.Max(0, cellY - 2);
        int searchEndY = Mathf.Min(cellY + 2, grid.GetLength(1) - 1);

        for (int x = searchStartX; x <= searchEndX; x++)
        {
            for (int y = searchStartY; y < searchEndY; y++)
            {
                int pointIndex = grid[x, y] - 1;
                if (pointIndex != -1)
                {
                    float sqrDst = (candidate - points[pointIndex]).sqrMagnitude;
                    if (sqrDst < radius * radius)
                        return false;
                }
            }
        }
        
        return true;
    }


    private void OnValidate()
    {
        _points = GetPoints();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(new Vector3(_regionCenter.x,0,_regionCenter.y), new Vector3(_regionSize.x, 1, _regionSize.y));
        Gizmos.color = Color.red;
        if (_points != null)
        {
            foreach (var point in _points)
            {
                Gizmos.DrawSphere(new Vector3(_regionCenter.x + point.x,0,_regionCenter.y + point.y),_displayRadius);
            }
        }
    }
    
    
}
