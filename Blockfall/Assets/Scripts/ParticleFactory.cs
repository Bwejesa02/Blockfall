using System.Collections.Generic;
using UnityEngine;

public static class ParticleFactory
{
    private static Dictionary<string, GameObject> particleCache = new Dictionary<string, GameObject>();

    public static void CreateMultipleParticleEffects(string effectType, Vector3 startPosition, int count, bool useFlyweight)
    {
        for (int i = 0; i < count; i++)
        {
            // Generate random offset positions
            Vector3 offsetPosition = startPosition + new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);

            // Spawn particles based on Flyweight toggle
            GameObject particlePrefab = useFlyweight ? GetCachedParticlePrefab(effectType) : LoadParticlePrefab(effectType);

            if (particlePrefab != null)
            {
                GameObject.Instantiate(particlePrefab, offsetPosition, Quaternion.identity);
            }
        }
    }

    private static GameObject GetCachedParticlePrefab(string effectType)
    {
        if (!particleCache.ContainsKey(effectType))
        {
            GameObject prefab = Resources.Load<GameObject>($"Particles/{effectType}Particle");
            if (prefab != null)
            {
                particleCache[effectType] = prefab;
            }
        }
        return particleCache.ContainsKey(effectType) ? particleCache[effectType] : null;
    }

    private static GameObject LoadParticlePrefab(string effectType)
    {
        // Load particle prefab without caching
        return Resources.Load<GameObject>($"Particles/{effectType}Particle");
    }
}
