# 🦾 Transformers War For Cybertron VR Mod

**Full OpenXR VR support for Transformers War For Cybertron - Modeled after TF2VR**

---

## 📋 Overview

This VR mod brings full virtual reality support to Transformers War For Cybertron, similar to the successful TF2VR mod by CircuitLord. It uses OpenXR for cross-platform VR support and BepInEx for seamless game integration.

### Features

- ✅ **Full OpenXR Support** - Works with Oculus Quest 2/3, Valve Index, HTC Vive, Windows Mixed Reality
- ✅ **IK Body Control** - Transformer-specific inverse kinematics for natural poses
- ✅ **Weapon Grab System** - Proper grip points for transformers and weapons
- ✅ **Camera Fixes** - VR-safe camera positioning and vignette
- ✅ **Model Scaling** - Adjustable transformer model sizes for VR comfort
- ✅ **BepInEx Integration** - No game file modifications required
- ✅ **Settings Persistence** - VR preferences saved between sessions

---

## 📦 Installation

### Prerequisites

1. **Transformers War For Cybertron** - Installed via Steam or Epic Games
2. **BepInEx IL2CPP** - Version 6.0.755 (included in mod)
3. **OpenXR Runtime** - Comes with most VR headsets

### Installation Steps

#### Option 1: Manual Installation

1. **Download the release** from GitHub releases
2. **Extract** the contents to your game directory:
   ```
   C:\Games\Transformers War For Cybertron\
   ├── BepInEx\
   │   └── plugins\
   │       └── TFCybertronVR\
   └── Data\
       └── Models\
   ```

3. **Launch the game** once (unmodded) to create profile folder
4. **Move the mod folder** to the new profile location

#### Option 2: Using the Installer (Coming Soon)

A dedicated installer will be released that handles:
- Game detection
- BepInEx installation
- Mod deployment
- Auto-updates

---

## ⚙️ Configuration

### VR_Settings.json

Located in `BepInEx/plugins/TFCybertronVR/`

```json
{
    "modelScale": 0.75,
    "fov": 110,
    "cameraHeight": 1.7,
    "autoReload": true,
    "weaponGrabStyle": "standard",
    "enableAudioSpatialization": true,
    "enableMotionBlur": true
}
```

### Settings Explained

| Setting | Default | Description |
|---------|---------|-------------|
| `modelScale` | 0.75 | Scale down transformer models (0.5-1.0) |
| `fov` | 110 | Field of view in degrees |
| `cameraHeight` | 1.7 | Eye height in meters |
| `autoReload` | true | Auto-reload weapons on R key |
| `weaponGrabStyle` | standard | Grab point style |
| `enableAudioSpatialization` | true | 3D positional audio |
| `enableMotionBlur` | true | VR-friendly motion blur |

---

## 🎮 Controls

### Standard VR Controls

| Action | Controller |
|--------|------------|
| Look | Head Tracking |
| Move | VR Gamepad (Default) |
| Reload | R |
| Interact | A / X (Grab) |
| Reload Auto | Hold R |
| Cycle Weapon | D-Pad Up/Down |

### Weapon Grab Points

Each weapon has optimized grab points:
- **Blaster** - Dual grip, shoulder-ready
- **Cannon** - Three-point grip for stability
- **Sword** - Two-point edge grip

---

## 🔧 Troubleshooting

### Common Issues

#### Game Crashes on Launch

**Solution:**
1. Verify game files via Steam/Epic
2. Ensure BepInEx is in the correct profile folder
3. Check antivirus isn't blocking BepInEx

#### VR Hands Not Showing

**Solution:**
1. Enable "VR Hand Rendering" in Unity Inspector
2. Check shader compatibility
3. Update GPU drivers

#### Weapon Models Clipping

**Solution:**
- Reduce `modelScale` in settings
- Adjust weapon grab distances in `WeaponGrab.cs`

#### Audio Issues

**Solution:**
- Enable `enableAudioSpatialization: true`
- Check Windows spatial sound settings

---

## 📝 Build Instructions

### For Developers

```bash
# Build the C# plugins
dotnet build src/VRLoader.cs -c Release

# Package for release
dotnet build -c Release --output packages/release
```

### Manifest Updates

When updating the mod, update `packages/manifest-v3.json`:
- Update `version` to new version
- Generate new SHA-256 hash for ZIP
- Add entry to `changes` array

---

## 📄 License

```
MIT License

Copyright (c) 2024 DeltaMan197

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

---

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Submit a pull request

---

## 📞 Support

- **Discord:** Join the TF Cybertron VR community
- **Issues:** Report bugs on GitHub
- **Feature Requests:** Submit via GitHub Issues

---

## 📜 Changelog

### Version 1.0.0 (2024-01-15)

**Initial Release**
- Full OpenXR VR support
- IK body control system
- Weapon grab points
- Camera positioning fix
- BepInEx integration
- Model scaling (0.75x default)

---

**Made with ❤️ for the Transformers VR Community**