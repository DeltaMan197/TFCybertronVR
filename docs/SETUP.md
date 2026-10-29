# 🦾 Transformers War For Cybertron VR Mod
# Complete Setup Guide

## 📋 Table of Contents

1. [System Requirements](#system-requirements)
2. [Installation](#installation)
3. [First-Time Setup](#first-time-setup)
4. [In-Game Configuration](#in-game-configuration)
5. [Troubleshooting](#troubleshooting)
6. [Advanced Configuration](#advanced-configuration)

---

## System Requirements

### Hardware

| Component | Minimum | Recommended |
|-----------|---------|-------------|
| **PC VR** | GTX 1060 6GB | RTX 3060 or higher |
| **Standalone** | 8GB RAM | 16GB RAM |
| **VR Headset** | Any OpenXR compatible | Quest 3, Index, Vive Pro 2 |
| **Storage** | 20GB free | 50GB SSD |

### Software

- Transformers War For Cybertron (Steam/Epic)
- OpenXR Runtime (included with headset)
- .NET Framework 4.8 (for installer, if applicable)

---

## Installation

### Step 1: Install Base Game

1. Purchase and install **Transformers War For Cybertron** via Steam or Epic Games
2. Launch the game once to create the base profile folder

### Step 2: Locate Game Directory

The game creates profiles in:
```
%LOCALAPPDATA%\Transformers War For Cybertron\
```

Find the profile folder (e.g., `Profile_001`)

### Step 3: Deploy Mod Files

Extract the mod package to:
```
%LOCALAPPDATA%\Transformers War For Cybertron\Profile_001\BepInEx\plugins\TFCybertronVR\
```

### Step 4: Verify Installation

The mod structure should look like:
```
TFCybertronVR/
├── VRLoader.dll
├── IKController.dll
├── WeaponGrab.dll
├── CameraFix.dll
└── VR_Settings.json
```

---

## First-Time Setup

### 1. Launch the Game

**Important:** Launch the game **once without the mod** to let it create the base profile structure.

### 2. Enable VR Mode

1. Start SteamVR or your VR runtime
2. Launch the game through SteamVR launcher
3. The VR Loader will auto-initialize on startup

### 3. Verify VR Initialization

When the game launches, you should see a quick console message:
```
[VR Loader] TF Cybertron VR v1.0.0 initializing...
[VR Loader] OpenXR initialized successfully
[VR Loader] Applying VR patches...
```

---

## In-Game Configuration

### Accessing Settings

**Menu Location:** Main Menu → Options → VR Settings

### Quick Settings

| Setting | Default | Suggested |
|---------|---------|-----------|
| Model Scale | 0.75x | 0.6x-0.75x |
| FOV | 110° | 100°-110° |
| Camera Height | 1.7m | 1.6m-1.7m |
| Auto Reload | ON | ON |

---

## Troubleshooting

### Game Crashes on Launch

**Diagnosis:**
1. Check if BepInEx is properly installed
2. Verify game files via Steam/Epic
3. Check antivirus isn't blocking BepInEx

**Fix:**
```
1. Navigate to: %LOCALAPPDATA%\Transformers War For Cybertron\Profile_XXX\BepInEx\
2. Ensure BepInEx.dll exists
3. If missing, reinstall BepInEx IL2CPP v6.0.755
```

### VR Hands Not Rendering

**Diagnosis:**
- Shader compatibility issue
- GPU driver outdated

**Fix:**
1. Update GPU drivers
2. In Unity (if dev): Enable "VR Hand Rendering" in Inspector
3. Try different render pipeline

### Weapon Models Clipping

**Diagnosis:**
- Model scale too high for VR distance

**Fix:**
1. Reduce `modelScale` in `VR_Settings.json`
2. Try values: 0.5, 0.6, 0.65
3. Adjust weapon grab distances in `WeaponGrab.cs` if developing

### Audio Issues

**Diagnosis:**
- Spatial audio disabled or Windows setting interfering

**Fix:**
1. Set `enableAudioSpatialization: true` in settings
2. Check Windows: Settings → System → Spatial Sound → Off
3. Verify audio device is set to VR headset

### Camera Clipping Through Walls

**Diagnosis:**
- Camera culling mask misconfigured

**Fix:**
1. The CameraFix module handles this automatically
2. If persistent, reduce `cameraDistance` in CameraFix.cs
3. Report as bug with screenshots

---

## Advanced Configuration

### For Developers

**Build from Source:**
```bash
# Build plugins
dotnet build src/VRLoader.cs -c Release

# Package for release
dotnet build -c Release --output packages/release
```

**Manifest Updates:**
When updating the mod, update `packages/manifest-v3.json`:
```json
{
    "version": "1.0.1",
    "url": "https://github.com/.../releases/download/v1.0.1/...",
    "sha256": "NEW_HASH_HERE",
    "size": 45678901
}
```

---

## Changelog

### v1.0.0 (2024-01-15)
- ✅ Initial release
- ✅ Full OpenXR support
- ✅ IK body control
- ✅ Weapon grab system
- ✅ Camera positioning fix

### v1.1.0 (Upcoming)
- 📅 Planned: Multiplayer support
- 📅 Planned: Improved IK weights
- 📅 Planned: Enhanced audio spatialization

---

## Support

- **GitHub Issues:** Report bugs
- **Discord:** Join community server
- **Wiki:** Extended documentation

---

**Made with ❤️ for the Transformers VR Community**