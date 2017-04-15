# MatEdit for Unity3D
## Introduction
MatEdit is an open-source script which you can use to make good looking custom editors for shaders very fast and without any affort.


**Basic Steps:**
1. Create a new custom editor script for a shader of your choice and put it into an editor folder somewhere in your project.
2. Add `CustomEditor "<CustomEditorScript>"` in front of the last brace of your shader.
3. Use the namespace `Northwind.Editor.Shader` to access MatEdit
4. In the `OnGUI` function access the target material and set it as scope material in MatEdit by using: `MatEdit.SetScope(<TargetMaterial>);`
5. Use one of the included MatEdit functions to set a value in your shader


## Features
**Groups**
- [x] Static Group
- [x] Toggle Group
- [x] Fold Group

**Texture Fields**
- [x] Texture Field
- [x] Normal Map Field
- [x] Tiling Field // Basically Vector Field
- [x] Offset Field // Basically Vector Field

**Simple Fields**
- [x] Int Field
- [x] Float Field
- [x] Slider Field
- [x] Toggle Field
- [x] Vector Field

**Special Fields**
- [x] Animation Curve Field
- [ ] Gradient Field
- [ ] Color Curves Field

**Tools**
- [ ] Context Menu: `Create Custom Editor`
- [ ] Context Menu: `Create Custom Editor (AUTO)` - converts property block into custom editor

**Maybe on Roadmap**
- [ ] Float Array Field
- [ ] Int Array Field
- [ ] Vector Array Field
- [ ] Group Reset Button
- [ ] Group Context Menu


## Description Key
| Symbol | Meaning            |
| :----: | ------------------ |
| **+**  | Added to Project   |
| **&**  | Change of function |
| **!**  | Marked as obsolet  |
| **-**  | Removed            |
