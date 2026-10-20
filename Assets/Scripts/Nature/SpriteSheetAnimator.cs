using UnityEngine;

/// <summary>
/// Sprite sheet karelerini döngüyle oynatır (ağaç, çalı, koyun, sudaki kaya...).
/// Her obje rastgele bir kareden ve hafif farklı hızda başlar, böylece orman aynı anda sallanmaz.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class SpriteSheetAnimator : MonoBehaviour
{
    public Sprite[] frames;
    public float fps = 10f;
    public bool randomStart = true;
    [Range(0f, 0.5f)] public float speedVariation = 0.15f;

    SpriteRenderer sr;
    float time;
    float speed;
    int current = -1;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        speed = fps * Random.Range(1f - speedVariation, 1f + speedVariation);
        if (randomStart && frames != null && frames.Length > 0)
            time = Random.Range(0f, frames.Length / speed);
    }

    void Update()
    {
        if (frames == null || frames.Length == 0) return;

        time += Time.deltaTime;
        int index = (int)(time * speed) % frames.Length;
        if (index != current)
        {
            current = index;
            sr.sprite = frames[index];
        }
    }
}
