using UnityEngine;

public class TerrainRandomizer : MonoBehaviour
{
    public int vertexInterval = 50;
    public float baseLine = 5f;
    public float minHeight = -5f;
    public float maxHeight = 5f;
    public float smoothness = 1.0f;

    void Start()
    {
        RandomizeTerrain();
    }

    void RandomizeTerrain()
    {
        Terrain terrain = GetComponent<Terrain>();
        TerrainData terrainData = terrain.terrainData;

        int width = terrainData.heightmapResolution;
        int height = terrainData.heightmapResolution;

        float[,] heights = terrainData.GetHeights(0, 0, width, height);
        
        int vertexIntervalHeightMap = terrain.terrainData.heightmapResolution / vertexInterval;

        for (int i = 0; i < width; i += vertexInterval)
        {
            for (int j = 0; j < height; j += vertexInterval)
            {
                heights[i, j] = baseLine + Random.Range(minHeight, maxHeight);
            }
        }

        terrainData.SetHeights(0, 0, heights);
    }
}