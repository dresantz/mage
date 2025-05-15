using UnityEngine;
using System.Collections.Generic;

public class BedTrigger : MonoBehaviour
{
    [SerializeField] private Vector3 targetScale = new Vector3(1.18f, 1.18f, 1f);
    [SerializeField] private float lerpSpeed = 9f;
    [SerializeField] private float slowMultiplier = 0.7f;

    private List<IMovableEntity> entitiesOnBed = new List<IMovableEntity>();
    private Dictionary<IMovableEntity, Vector3> originalScales = new Dictionary<IMovableEntity, Vector3>();
    private Dictionary<IMovableEntity, float> originalSpeeds = new Dictionary<IMovableEntity, float>();

    private void Update()
    {
        foreach (var entity in entitiesOnBed)
        {
            if (entity != null && originalScales.ContainsKey(entity))
            {
                Transform t = entity.GetTransform();
                t.localScale = Vector3.Lerp(
                    t.localScale,
                    targetScale,
                    Time.deltaTime * lerpSpeed
                );
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IMovableEntity entity = other.GetComponent<IMovableEntity>();
        if (entity != null)
        {
            if (!entitiesOnBed.Contains(entity))
                entitiesOnBed.Add(entity);

            if (!originalScales.ContainsKey(entity))
                originalScales[entity] = entity.GetTransform().localScale;

            if (!originalSpeeds.ContainsKey(entity))
                originalSpeeds[entity] = entity.GetSpeed();

            entity.SetSpeed(entity.GetSpeed() * slowMultiplier);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IMovableEntity entity = other.GetComponent<IMovableEntity>();
        if (entity != null)
        {
            entitiesOnBed.Remove(entity);

            if (originalScales.TryGetValue(entity, out Vector3 originalScale))
            {
                entity.GetTransform().localScale = originalScale;
                originalScales.Remove(entity);
            }

            if (originalSpeeds.TryGetValue(entity, out float originalSpeed))
            {
                entity.SetSpeed(originalSpeed);
                originalSpeeds.Remove(entity);
            }
        }
    }
}
