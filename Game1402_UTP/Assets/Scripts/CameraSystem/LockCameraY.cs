using UnityEngine;
using Cinemachine;


public class LockCameraY : CinemachineExtension
{
    [SerializeField]
    float _mYPosition = 13;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            var position = state.RawPosition;
            position.y = _mYPosition;
            state.RawPosition = position;
        }
    }
}