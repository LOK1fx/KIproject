using UnityEngine;

public class LerpScale : TransformLerpBase
{
    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, Target, Time.deltaTime * Speed);
    }
}