using UnityEngine;

public class GPUGraph : MonoBehaviour
{


    [SerializeField, Range(10, 200)]
    int resolution = 10;

    [SerializeField]
    FunctionalLibrary.FunctionName function;

    public enum TransitionMode { Cycle, Random }

    [SerializeField]
    TransitionMode transitionMode;

    [SerializeField, Min(0f)]
    float functionDuration = 1f, transitionDuration = 1f;


    float duration;

    bool transitioning;

    FunctionalLibrary.FunctionName transitionFunction;

    ComputeBuffer positionBuffer;



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


    }

    void PickNextFunction()
    {
        function = transitionMode == TransitionMode.Cycle ?
            FunctionalLibrary.GetNextFunctionName(function) :
            FunctionalLibrary.GetRandomFunctionNameOtherThan(function);
    }

}