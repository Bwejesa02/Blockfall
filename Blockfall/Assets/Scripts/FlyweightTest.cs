using UnityEngine;

public class FlyweightTest : MonoBehaviour
{
    [SerializeField] private bool useFlyweight = true; // Toggle Flyweight ON/OFF

    void Start()
    {
        Debug.Log($"Starting test with Flyweight: {useFlyweight}");

        // Spawn 30 particle effects
        ParticleFactory.CreateMultipleParticleEffects("RowClear", Vector3.zero, 30, useFlyweight);
    }
}
