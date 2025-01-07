using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTentacle : MonoBehaviour
{
    public int length;
    public LineRenderer lineRenderer;
    public Vector3[] segmentPoses;
    public Vector3[] segmentVelocity;

    public Transform targetDir;
    public float targetDist;
    public float smoothSpeed;

    private void Start()
    {
        lineRenderer.positionCount = length;
        segmentPoses = new Vector3[length];
        segmentVelocity = new Vector3[length];
    }

    private void Update()
    {
        // Primeiro segmento fica na cabeça(Target)
        segmentPoses[0] = targetDir.position;

        // Outros se ligam ao anterior
        for (int i = 1; i < segmentPoses.Length; i++)
        {
            // As conecções são suavizadas pelo SmoothDamp
            segmentPoses[i] = Vector3.SmoothDamp (
                segmentPoses[i],
                segmentPoses[i - 1] + targetDir.right * targetDist,
                ref segmentVelocity[i], smoothSpeed
                );
        }
        lineRenderer.SetPositions(segmentPoses);
    }
}
