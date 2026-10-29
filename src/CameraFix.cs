// TFCybertronVR - Camera Fix System
// VR-safe camera rendering to prevent clipping and FOV issues

using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace TFCybertronVR.Camera
{
    public class CameraFix : MonoBehaviour
    {
        [Header("Camera Settings")]
        [SerializeField] private float baseFOV = 90f;
        [SerializeField] private float vrFOV = 110f;
        [SerializeField] private float cameraDistance = 0.6f;
        [SerializeField] private float cameraHeight = 1.7f;

        [Header("Vignette Settings")]
        [SerializeField] private float vignetteIntensity = 0.8f;
        [SerializeField] private float vignetteRoundness = 0.8f;

        private Camera mainCamera;
        private CameraField cameraField;
        private bool isVRMode = true;

        private void Awake()
        {
            mainCamera = Camera.main;
            InitializeCameraField();
            ApplyCameraFix();
        }

        private void Start()
        {
            // Subscribe to VR state changes
            UnityEngine.XR.XRSettings.enabled += OnVRStateChanged;
            OnVRStateChanged(null, UnityEngine.XR.XRSettings.enabled);
        }

        private void Update()
        {
            UpdateCamera();
            UpdateVignette();
        }

        private void InitializeCameraField()
        {
            // Create camera field for VR rendering
            cameraField = new CameraField();

            // Add main camera to field
            var mainCameraField = new CameraField.CameraEntry
            {
                camera = mainCamera,
                eye = CameraField.CameraEye.Left,
                projectionMatrix = mainCamera.projectionMatrix,
                aspectRatio = mainCamera.aspect,
                enabled = true
            };

            cameraField.AddEntry(mainCameraField);
        }

        private void ApplyCameraFix()
        {
            // Apply camera position fix
            if (mainCamera != null)
            {
                // Offset camera to VR eye position
                mainCamera.transform.position = new Vector3(0, cameraHeight, cameraDistance);
                mainCamera.transform.LookAt(transform.position + mainCamera.transform.forward * -0.1f);
            }

            // Apply FOV correction
            ApplyFOVCorrection();

            // Fix camera clipping
            ApplyCameraClippingFix();
        }

        private void UpdateCamera()
        {
            // Smooth camera follow for VR
            var targetPosition = CalculateVRCameraPosition();
            mainCamera.transform.position = Vector3.Lerp(
                mainCamera.transform.position,
                targetPosition,
                5f * Time.deltaTime
            );

            // Maintain camera orientation
            mainCamera.transform.LookAt(transform.position + mainCamera.transform.forward * -0.1f);
        }

        private Vector3 CalculateVRCameraPosition()
        {
            // Calculate ideal VR camera position
            var basePosition = new Vector3(0, cameraHeight, cameraDistance);

            // Adjust for head tracking
            if (UnityEngine.XR.XRLoaderHelper.GetSpatialTrackingReference() != null)
            {
                var headsetPosition = GetHeadsetPosition();
                basePosition += headsetPosition;
            }

            return basePosition;
        }

        private void UpdateVignette()
        {
            if (!mainCamera)
            {
                return;
            }

            // Apply VR-appropriate vignette
            mainCamera.farClipPlane = 100f; // Extended far clip for VR

            // Apply color correction for better VR contrast
            mainCamera.colorCorrection = new ColorCorrection
            {
                mode = ColorCorrection.Mode.Vignette
            };
        }

        private void ApplyFOVCorrection()
        {
            // Adjust FOV for VR comfort
            var currentFOV = mainCamera.fieldOfView;
            var targetFOV = isVRMode ? vrFOV : baseFOV;

            // Smooth FOV transition
            mainCamera.fieldOfView = Mathf.Lerp(
                mainCamera.fieldOfView,
                targetFOV,
                2f * Time.deltaTime
            );

            // Apply VR-specific projection
            if (isVRMode)
            {
                ApplyVRProjection();
            }
        }

        private void ApplyVRProjection()
        {
            // VR projection matrix for reduced distortion
            var vrProjectionMatrix = CalculateVRProjectionMatrix();
            mainCamera.projectionMatrix = vrProjectionMatrix;
        }

        private void ApplyCameraClippingFix()
        {
            // Fix camera clipping through walls/objects
            var clippedObjects = FindObjectsWithClipIssue();

            foreach (var obj in clippedObjects)
            {
                ApplyClippingFix(obj);
            }
        }

        private CameraField.CameraEntry[] FindObjectsWithClipIssue()
        {
            // Find objects that block camera view
            return new CameraField.CameraEntry[0]; // Placeholder
        }

        private void ApplyClippingFix(GameObject obj)
        {
            // Remove or disable clipping-causing objects
            // Or adjust camera culling mask
        }

        private void OnVRStateChanged(object sender, bool enabled)
        {
            if (enabled)
            {
                isVRMode = true;
                ApplyVRSettings();
            }
            else
            {
                isVRMode = false;
                ApplyFlatScreenSettings();
            }
        }

        private void ApplyVRSettings()
        {
            baseFOV = 90f;
            vrFOV = 110f;
            cameraDistance = 0.6f;
            cameraHeight = 1.7f;
        }

        private void ApplyFlatScreenSettings()
        {
            baseFOV = 90f;
            vrFOV = 90f;
            cameraDistance = 0.5f;
            cameraHeight = 1.6f;
        }

        private Matrix4x4 CalculateVRProjectionMatrix()
        {
            // Calculate VR-friendly projection matrix
            // Reduces barrel distortion effects
            var fovRadians = vrFOV * (Mathf.PI / 180f);
            var aspect = mainCamera.aspect;
            var near = 0.1f;
            var far = 1000f;

            return Matrix4x4.Perspective(fovRadians, aspect, near, far);
        }

        private Vector3 GetHeadsetPosition()
        {
            // Get headset position from XR loader
            try
            {
                var xrLoader = UnityEngine.XR.XRLoaderHelper.GetSpatialTrackingReference();
                if (xrLoader != null)
                {
                    return xrLoader.position;
                }
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError($"Error getting headset position: {e.Message}");
            }

            return Vector3.zero;
        }
    }
}