using UnityEngine;
using Cinemachine;


public class LockCameraY : CinemachineExtension
{
    [SerializeField]
    float yPosition = 13;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            Vector3 position = state.RawPosition;
            position.y = yPosition;
            state.RawPosition = position;
        }
    }
}