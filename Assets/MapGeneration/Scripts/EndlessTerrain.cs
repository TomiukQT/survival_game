using System;
using System.Collections.Generic;
using UnityEngine;

public class EndlessTerrain : MonoBehaviour
{
    public const float maxViewDistance = 450f;
    public Transform viewer;

    public static Vector2 viewerPositon;
    private int _chunkSize;
    private int _chunkVisibleInViewDistance;

    private Dictionary<Vector2, TerrainChunk> _terrainChunks = new Dictionary<Vector2, TerrainChunk>();
    private List<TerrainChunk> _terrainChunksVisibleLastUpdate = new List<TerrainChunk>();
    
    private void Start()
    {
        _chunkSize = MapGenerator.MAP_CHUNK_SIZE - 1;
        _chunkVisibleInViewDistance = Mathf.RoundToInt(maxViewDistance / _chunkSize);
    }

    private void Update()
    {
        viewerPositon = new Vector2(viewer.position.x, viewer.position.z);
        UpdateVisibleChunks();
    }

    private void UpdateVisibleChunks()
    {
        foreach (var terrainChunk in _terrainChunksVisibleLastUpdate)
        {
            terrainChunk.SetVisible(false);
        }
        _terrainChunksVisibleLastUpdate.Clear();
        
        int currentChunkCoordX = Mathf.RoundToInt(viewerPositon.x / _chunkSize);
        int currentChunkCoordY = Mathf.RoundToInt(viewerPositon.y / _chunkSize);
        for (int yOffset = -_chunkVisibleInViewDistance; yOffset <= _chunkVisibleInViewDistance; yOffset++)
        {
            for (int xOffset = -_chunkVisibleInViewDistance; xOffset <= _chunkVisibleInViewDistance; xOffset++)
            {
                Vector2 viewedChunkCoord = new Vector2(currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);
                if (_terrainChunks.ContainsKey(viewedChunkCoord))
                {
                    TerrainChunk chunk = _terrainChunks[viewedChunkCoord];
                    chunk.UpdateTerrainChunk();
                    if(chunk.IsVisible())
                        _terrainChunksVisibleLastUpdate.Add(chunk);
                }
                else
                {
                    _terrainChunks.Add(viewedChunkCoord,new TerrainChunk(viewedChunkCoord,_chunkSize,transform));
                }
            }
        }
    }
    
    public class TerrainChunk
    {
        private Vector2 _position;
        private Bounds _bounds;
        private GameObject _meshObject;
    
        public TerrainChunk(Vector2 coordinate, int size, Transform parent)
        {
            _position = coordinate * size;
            _bounds = new Bounds(_position, Vector2.one * size);
            Vector3 positionV3 = new Vector3(_position.x, 0, _position.y);

            _meshObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
            _meshObject.transform.position = positionV3;
            _meshObject.transform.localScale = Vector3.one * size / 10f;
            _meshObject.transform.parent = parent;
            SetVisible(false);
        }

        public void UpdateTerrainChunk()
        {
            float viewerDistanceFromNearestEdge = Mathf.Sqrt(_bounds.SqrDistance(viewerPositon));
            bool visible = viewerDistanceFromNearestEdge < maxViewDistance;
            SetVisible(visible);
        }

        public void SetVisible(bool visible)
        {
            _meshObject.SetActive(visible);
        }

        public bool IsVisible() => _meshObject.activeSelf;
    }
    
}


