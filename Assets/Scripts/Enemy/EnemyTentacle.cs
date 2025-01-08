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
    public float trailSpeed;

    public float wiggleSpeed;
    public float wiggleMagnitude;
    public Transform wiggleDir;

    private void Start()
    {
        lineRenderer.positionCount = length;
        segmentPoses = new Vector3[length];
        segmentVelocity = new Vector3[length];
        ResetPos();
    }

    private void Update()
    {
        // Controla o rebolado
        wiggleDir.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * wiggleSpeed) * wiggleMagnitude);

        // Primeiro segmento fica na cabeça(Target)
        segmentPoses[0] = targetDir.position;

        // Outros se ligam ao anterior
        for (int i = 1; i < segmentPoses.Length; i++)
        {
            // As conecções são suavizadas pelo SmoothDamp
            segmentPoses[i] = Vector3.SmoothDamp (
                segmentPoses[i],
                segmentPoses[i - 1] + targetDir.right * targetDist,
                ref segmentVelocity[i],
                smoothSpeed + i / trailSpeed);
        }
        lineRenderer.SetPositions(segmentPoses);
    }

    private void ResetPos()
    {
        segmentPoses[0] = targetDir.position;
        for (int i = 1; i < length; i++)
        {
            segmentPoses[i] = segmentPoses [i -1] + targetDir.right * targetDist;
        }
        lineRenderer.SetPositions(segmentPoses);
    }
}
