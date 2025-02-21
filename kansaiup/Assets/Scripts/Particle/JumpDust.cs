using UnityEngine;

public class JumpDust : MonoBehaviour
{
    [SerializeField] private ParticleSystem dustEffect;

    public void PlayDustEffect()
    {
        if (dustEffect != null)
        {
            dustEffect.Play();
        }
    }

    public void SpawnDustEffect(Vector3 spawnPosition)
    {
        if (dustEffect != null)
        {
            ParticleSystem instance = Instantiate(dustEffect, spawnPosition, Quaternion.identity);
            instance.Play();
        }
    }
}