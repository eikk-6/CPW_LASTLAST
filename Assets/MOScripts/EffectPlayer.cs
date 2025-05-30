using UnityEngine;

public class EffectPlayer : MonoBehaviour
{
    public GameObject sparkEffectPrefab;
    public GameObject hitEffectPrefab;
    public GameObject batteryEffectPrefab;
    public AudioClip swordClashSound;
    public AudioClip bodyHitSound;
    public AudioClip batteryHitSound;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayEffect(Vector3 position, string type)
    {
        GameObject effect = null;
        AudioClip clip = null;

        switch (type)
        {
            case "Sword":
                effect = sparkEffectPrefab;
                clip = swordClashSound;
                break;
            case "Body":
                effect = hitEffectPrefab;
                clip = bodyHitSound;
                break;
            case "Battery":
                effect = batteryEffectPrefab;
                clip = batteryHitSound;
                break;
        }

        if (effect != null)
        {
            Instantiate(effect, position, Quaternion.identity);
        }

        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
