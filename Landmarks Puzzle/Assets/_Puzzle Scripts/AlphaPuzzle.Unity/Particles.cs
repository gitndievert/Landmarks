using UnityEngine;

public class Particles : MonoBehaviour
{
    private const float DefaultDelaySec = 3.5f;
    private static float _delay = DefaultDelaySec;

    public static float DestructDelay
    {
        get { return _delay; }
        set { _delay = value; }
    }

    public static void FireRandomParticle(GameObject[] particles, Vector3 position)
    {
        int rand = Random.Range(0, particles.Length);
        FireParticle(particles[rand], position);
    }

    public static void FireRandomParticle(GameObject[] particles, Vector3 position, AudioClip clip)
    {
        SoundManager.PlaySound(clip);
        int rand = Random.Range(0, particles.Length);
        FireParticle(particles[rand], position);
    }

    public static void FireParticle(GameObject particle, Vector3 position)
    {
        if (particle == null) return;
        var poof = (GameObject)Instantiate(particle, position, Quaternion.identity);
        Destroy(poof, _delay);
        DestructDelay = DefaultDelaySec;
    }

    public static void FireParticle(GameObject particle, Vector3 position, AudioClip clip)
    {
        SoundManager.PlaySound(clip);
        FireParticle(particle, position);
    }

    public static void FireParticle(GameObject particle, Vector3 position, AudioClip[] clips, float delay)
    {
        SoundManager.QueueSounds(clips, delay);
        FireParticle(particle, position);
    }

}
