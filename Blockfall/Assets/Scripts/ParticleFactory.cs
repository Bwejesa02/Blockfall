using System.Collections.Generic;
using UnityEngine;

public static class ParticleFactory
{
    private static Dictionary<string, GameObject> particleCache = new Dictionary<string, GameObject>();

    public static void CreateParticleEffect(string effectType, Vector3 position)
    {
        GameObject particlePrefab = GetParticlePrefab(effectType);
        if (particlePrefab != null)
        {
            GameObject particle = Object.Instantiate(particlePrefab, position, Quaternion.identity);
            Object.Destroy(particle, 3f); // Automatically destroy after 3 seconds
        }
    }

    private static GameObject GetParticlePrefab(string effectType)
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
}
