# SafeCrack Docker Run

This project is containerized by serving the Unity WebGL build with `nginx`.

## 1) Create WebGL build in Unity

1. Open `File -> Build Settings`.
2. Select `WebGL` and click `Switch Platform`.
3. Build to:

```text
Build/WebGL
```

After this step, `Build/WebGL/index.html` must exist.

## 2) Build Docker image

```bash
docker build -t safecrack-web .
```

## 3) Run container

```bash
docker run --rm -p 8080:80 safecrack-web
```

Open:

- http://localhost:8080

## Notes

- If Docker build fails with `COPY Build/WebGL/ ... not found`, create the WebGL build first.
- Unity `Build/` is ignored in Git by default. If you must version build artifacts for submission, add them explicitly.
