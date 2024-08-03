using System.Data;
using UnityEngine;

public class GPUGraph : MonoBehaviour
{

    [SerializeField]
    ComputeShader computeShader;

    [SerializeField]
    Material material;
    [SerializeField]
    Mesh mesh;

    static readonly int
    positionsId = Shader.PropertyToID("_Positions"),
    resolutionId = Shader.PropertyToID("_Resolution"),
    stepId = Shader.PropertyToID("_Step"),
    timeId = Shader.PropertyToID("_Time");


    [SerializeField, Range(0, 200)]
    int resolution = 10;

    [SerializeField]
    FunctionalLibrary.FunctionName function;

    public enum TransitionMode { Cycle, Random }

    [SerializeField]
    TransitionMode transitionMode = TransitionMode.Cycle;

    [SerializeField, Min(0f)]
    float functionDuration = 1f, transitionDuration = 1f;


    float duration;

    bool transitioning;

    FunctionalLibrary.FunctionName transitionFunction;

    ComputeBuffer positionBuffer;


    void OnEnable()
    {
        positionBuffer = new ComputeBuffer(resolution * resolution, 3 * 4);
    }


    void OnDisable()
    {
        positionBuffer.Release();
        positionBuffer = null;
    }
    void UpdateFunctionOnGPU()
    {
        float step = 2f / resolution;

        computeShader.SetInt(resolutionId, resolution);
        computeShader.SetFloat(stepId, step);
        computeShader.SetFloat(timeId, Time.time);

        computeShader.SetBuffer(0, positionsId, positionBuffer);

        int groups = Mathf.CeilToInt(resolution / 8f);
        computeShader.Dispatch(0, groups, groups, 1);

        material.SetBuffer(positionsId, positionBuffer);
        material.SetFloat(stepId, step);

        var bounds = new Bounds(Vector3.zero, Vector3.one * (2f + 2f / resolution));

        Graphics.DrawMeshInstancedProcedural(
            mesh, 0, material, bounds, positionBuffer.count
        );
    }
    void Update()
    {
        duration += Time.deltaTime;
        if (transitioning)
        {
            if (duration >= transitionDuration)
            {
                duration -= transitionDuration;
                transitioning = false;
            }
        }
        else if (duration >= functionDuration)
        {
            duration -= functionDuration;
            transitioning = true;
            transitionFunction = function;
            PickNextFunction();
        }

        UpdateFunctionOnGPU();

    }

    void PickNextFunction()
    {
        function = transitionMode == TransitionMode.Cycle ?
            FunctionalLibrary.GetNextFunctionName(function) :
            FunctionalLibrary.GetRandomFunctionNameOtherThan(function);
    }

}