using UnityEngine;

public static class Noise
{
    private static readonly int MAX_OFFSET_RANGE = 100000;
    
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale,
        int octaves, float persistance, float lacunarity, Vector2 offset )
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        System.Random rng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];
        for (int i = 0; i < octaves; i++)
        {
            float offsetX = rng.Next(-MAX_OFFSET_RANGE, MAX_OFFSET_RANGE) + offset.x;
            float offsetY = rng.Next(-MAX_OFFSET_RANGE, MAX_OFFSET_RANGE) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }
        
        if (scale <= 0)
            scale = 0.0001f;

        float maxNoiseHeight =  float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;
        
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {

                float amplitude = 1f;
                float frequency = 1f;
                float noiseHeight = 0f;
                
                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = ((x - halfWidth) / scale + octaveOffsets[i].x) * frequency;
                    float sampleY = ((y - halfHeight) / scale + octaveOffsets[i].y) * frequency;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;

                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                    
                    
                }

                maxNoiseHeight = Mathf.Max(maxNoiseHeight, noiseHeight);
                minNoiseHeight = Mathf.Min(minNoiseHeight, noiseHeight);
                
                noiseMap[x, y] = noiseHeight;
            }
        }
        
        for (int y = 0; y < mapHeight; y++)
            for (int x = 0; x < mapWidth; x++)
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);

        return noiseMap;
    }
    
    
}
