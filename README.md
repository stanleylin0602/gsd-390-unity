# GSD 390 - Empty Unity Project

A minimal, empty Unity project for the GSD 390 GitHub setup assignment.

## Open the project

1. Clone this repository.
2. In Unity Hub, choose **Add project from disk** and select the repository folder.
3. Open it with Unity **6000.0.62f1**, or the Unity version required by your course.

The project intentionally contains no game assets, scenes, or gameplay scripts.
Unity generates its default settings and local caches on first open.

## Version control

- `Assets/`, `Packages/`, and `ProjectSettings/` are included in Git.
- `.gitignore` uses [GitHub's Unity template](https://github.com/github/gitignore/blob/main/Unity.gitignore).
- Temporary folders such as `Library/`, `Temp/`, `Logs/`, and `UserSettings/` are excluded.
- Asset serialization is set to text, with visible `.meta` files.

This project structure was prepared without running the Unity Editor; its first
import has not been verified in Unity.
