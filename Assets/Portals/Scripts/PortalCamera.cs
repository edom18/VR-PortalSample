using UnityEngine;
using System.Collections;

public class PortalCamera : MonoBehaviour {

    public Camera VrEye;

    RenderTexture _leftEyeRenderTexture;
    RenderTexture _rightEyeRenderTexture;
    Camera _cameraForPortal;
    Vector3 _eyeOffset;

    protected void Awake()
    {
        _cameraForPortal = GetComponent<Camera>();
        _cameraForPortal.enabled = false;

        _leftEyeRenderTexture = new RenderTexture((int)SteamVR.instance.sceneWidth, (int)SteamVR.instance.sceneHeight, 24);
        _rightEyeRenderTexture = new RenderTexture((int)SteamVR.instance.sceneWidth, (int)SteamVR.instance.sceneHeight, 24);

        int aa = QualitySettings.antiAliasing == 0 ? 1 : QualitySettings.antiAliasing;
        _leftEyeRenderTexture.antiAliasing = aa;
        _rightEyeRenderTexture.antiAliasing = aa;
    }

    protected Matrix4x4 HMDMatrix4x4ToMatrix4x4(Valve.VR.HmdMatrix44_t input)
    {
        var m = Matrix4x4.identity;

        m[0, 0] = input.m0;
        m[0, 1] = input.m1;
        m[0, 2] = input.m2;
        m[0, 3] = input.m3;

        m[1, 0] = input.m4;
        m[1, 1] = input.m5;
        m[1, 2] = input.m6;
        m[1, 3] = input.m7;

        m[2, 0] = input.m8;
        m[2, 1] = input.m9;
        m[2, 2] = input.m10;
        m[2, 3] = input.m11;

        m[3, 0] = input.m12;
        m[3, 1] = input.m13;
        m[3, 2] = input.m14;
        m[3, 3] = input.m15;

        return m;
    }

    public void RenderIntoMaterial(Material material)
    {
        // Left eye.
        _eyeOffset = SteamVR.instance.eyes[0].pos;
        _eyeOffset.z = 0f;
        transform.localPosition = _eyeOffset;

        Valve.VR.HmdMatrix44_t leftMatrix = SteamVR.instance.hmd.GetProjectionMatrix(Valve.VR.EVREye.Eye_Left, VrEye.nearClipPlane, VrEye.farClipPlane, Valve.VR.EGraphicsAPIConvention.API_DirectX);
        _cameraForPortal.projectionMatrix = HMDMatrix4x4ToMatrix4x4(leftMatrix);
        _cameraForPortal.targetTexture = _leftEyeRenderTexture;
        _cameraForPortal.Render();
        material.SetTexture("_LeftEyeTexture", _leftEyeRenderTexture);

        // Right eye.
        _eyeOffset = SteamVR.instance.eyes[0].pos;
        _eyeOffset.z = 0f;
        transform.localPosition = _eyeOffset;

        Valve.VR.HmdMatrix44_t rightMatrix = SteamVR.instance.hmd.GetProjectionMatrix(Valve.VR.EVREye.Eye_Right, VrEye.nearClipPlane, VrEye.farClipPlane, Valve.VR.EGraphicsAPIConvention.API_DirectX);
        _cameraForPortal.projectionMatrix = HMDMatrix4x4ToMatrix4x4(rightMatrix);
        _cameraForPortal.targetTexture = _rightEyeRenderTexture;
        _cameraForPortal.Render();
        material.SetTexture("_RightEyeTexture", _rightEyeRenderTexture);
    }
}
