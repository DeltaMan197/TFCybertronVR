// TFCybertronVR - Weapon Grab System
// Handles proper weapon grab poses for transformers

using System;
using System.Collections.Generic;
using UnityEngine;

namespace TFCybertronVR.Weapons
{
    public class WeaponGrab : MonoBehaviour
    {
        [Header("Grab Settings")]
        [SerializeField] private float grabDistance = 0.8f;
        [SerializeField] private float grabSpeed = 5.0f;
        [SerializeField] private float autoReloadDelay = 1.0f;

        [Header("Weapon Data")]
        private Dictionary<string, WeaponData> weaponData = new Dictionary<string, WeaponData>();

        private List<WeaponHandle> activeHandles = new List<WeaponHandle>();
        private float lastReloadTime = 0f;

        private void Awake()
        {
            InitializeWeaponData();
        }

        private void Update()
        {
            UpdateWeaponGrabs();
            HandleAutoReload();
        }

        private void InitializeWeaponData()
        {
            // Define weapon-specific grab data
            weaponData = new Dictionary<string, WeaponData>
            {
                {
                    "AutobotBlaster", new WeaponData
                    {
                        name = "Autobot Blaster",
                        gripPoints = new Vector3[]
                        {
                            new Vector3(-0.3f, -0.5f, 0.8f),
                            new Vector3(0.3f, -0.5f, 0.8f)
                        },
                        handTransforms = new Transform[]
                        {
                            transform.Find("Weapons/AutobotBlaster/Grip_Left"),
                            transform.Find("Weapons/AutobotBlaster/Grip_Right")
                        }
                    }
                },
                {
                    "DecepticonCannon", new WeaponData
                    {
                        name = "Decepticon Cannon",
                        gripPoints = new Vector3[]
                        {
                            new Vector3(-0.4f, -0.6f, 1.0f),
                            new Vector3(0.4f, -0.6f, 1.0f),
                            new Vector3(0, -0.4f, 0.9f)
                        },
                        handTransforms = new Transform[]
                        {
                            transform.Find("Weapons/DecepticonCannon/Grip_Left"),
                            transform.Find("Weapons/DecepticonCannon/Grip_Right"),
                            transform.Find("Weapons/DecepticonCannon/Grip_Center")
                        }
                    }
                },
                {
                    "TransformorSword", new WeaponData
                    {
                        name = "Transformer Sword",
                        gripPoints = new Vector3[]
                        {
                            new Vector3(-0.2f, -0.4f, 0.7f),
                            new Vector3(0.2f, -0.4f, 0.7f)
                        },
                        handTransforms = new Transform[]
                        {
                            transform.Find("Weapons/TransformorSword/Grip_Left"),
                            transform.Find("Weapons/TransformorSword/Grip_Right")
                        }
                    }
                }
            };

            // Find all weapon transforms
            FindWeaponTransforms();
        }

        private void FindWeaponTransforms()
        {
            // Recursively find all weapon-related transforms
            FindTransformsRecursive(transform, "Weapon");
        }

        private void FindTransformsRecursive(Transform parent, string nameFilter)
        {
            foreach (Transform child in parent)
            {
                if (child.name.Contains(nameFilter, StringComparison.OrdinalIgnoreCase))
                {
                    // Process weapon transform
                }
                FindTransformsRecursive(child, nameFilter);
            }
        }

        private void UpdateWeaponGrabs()
        {
            // Check each active weapon handle
            foreach (var handle in activeHandles)
            {
                UpdateWeaponHandle(handle);
            }

            // Handle weapon pickups
            HandleWeaponPickups();
        }

        private void UpdateWeaponHandle(WeaponHandle handle)
        {
            if (!handle.equipped)
            {
                return;
            }

            // Check if weapon is in grab distance
            var weaponTransform = handle.weaponTransform;
            if (weaponTransform == null)
            {
                return;
            }

            var gripPoint = GetGripPoint(handle.weaponData, handle.handIndex);
            var targetPosition = GetTargetPosition(handle.weaponTransform, gripPoint);

            // Smoothly move to grip point
            handle.currentGripPoint += Vector3.Lerp(
                handle.currentGripPoint - targetPosition,
                targetPosition,
                grabSpeed * Time.deltaTime
            );

            // Check if grip is complete
            if (Vector3.Distance(handle.currentGripPoint, targetPosition) < 0.01f)
            {
                handle.isGripped = true;
            }
        }

        private void HandleWeaponPickups()
        {
            // Check for nearby weapons to pickup
            var pickupDistance = 2.0f;

            foreach (var kvp in weaponData)
            {
                var weaponName = kvp.Key;
                var weaponDataValue = kvp.Value;

                // Check if weapon is on ground
                var weaponGroundTransform = FindWeaponGround(weaponName);
                if (weaponGroundTransform == null)
                {
                    continue;
                }

                // Check if player is close enough
                if (Vector3.Distance(transform.position, weaponGroundTransform.position) < pickupDistance)
                {
                    PickupWeapon(weaponName, weaponGroundTransform);
                }
            }
        }

        private Transform FindWeaponGround(string weaponName)
        {
            // Find weapon on ground
            return transform.Find($"Grounded_Weapons/{weaponName}");
        }

        private void PickupWeapon(string weaponName, Transform groundTransform)
        {
            // Remove from ground
            groundTransform.SetParent(null);

            // Add to active handles
            var handle = new WeaponHandle
            {
                weaponName = weaponName,
                weaponTransform = groundTransform,
                handIndex = 0,
                currentGripPoint = groundTransform.position,
                targetPosition = groundTransform.position,
                isGripped = false,
                equipped = true
            };

            activeHandles.Add(handle);
        }

        private Vector3 GetGripPoint(WeaponData data, int handIndex)
        {
            if (data.gripPoints.Length > handIndex)
            {
                return data.gripPoints[handIndex];
            }
            return Vector3.zero;
        }

        private Vector3 GetTargetPosition(Transform weapon, Vector3 gripPoint)
        {
            // Calculate target position based on weapon transform and grip point
            return weapon.TransformPoint(gripPoint);
        }

        private void HandleAutoReload()
        {
            if (Input.GetKeyDown(KeyCode.R) && Time.time - lastReloadTime >= autoReloadDelay)
            {
                AutoReload();
                lastReloadTime = Time.time;
            }
        }

        private void AutoReload()
        {
            // Trigger auto-reload for equipped weapons
            foreach (var handle in activeHandles)
            {
                if (handle.isGripped)
                {
                    TriggerWeaponReload(handle.weaponName);
                }
            }
        }

        private void TriggerWeaponReload(string weaponName)
        {
            // Send reload command to weapon
            Logger.Log($"Reloading {weaponName}");
        }

        private struct WeaponData
        {
            public string name;
            public Vector3[] gripPoints;
            public Transform[] handTransforms;
        }

        private struct WeaponHandle
        {
            public string weaponName;
            public Transform weaponTransform;
            public int handIndex;
            public Vector3 currentGripPoint;
            public Vector3 targetPosition;
            public bool isGripped;
            public bool equipped;
        }
    }
}