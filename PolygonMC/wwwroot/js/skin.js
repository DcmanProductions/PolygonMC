/*
    PolygonMC - LFInteractive LLC. 2021-2024
    PolygonMC is a free and open source Minecraft Launcher implementing various modloaders, mod platforms, and minecraft authentication.
    PolygonMC is protected under GNU GENERAL PUBLIC LICENSE version 3.0 License
    https://www.gnu.org/licenses/gpl-3.0.en.html#license-text
    https://github.com/DcmanProductions/PolygonMC
*/
function initializeSkinCanvas(canvasRef, base64) {
    const canvas = canvasRef;

    const skinCanvas = new SkinCanvas(canvas);
    skinCanvas.LoadSkin(base64);
    skinCanvas.Render();
}

class SkinCanvas {
    constructor(canvas) {
        this.canvas = canvas;
        this.scene = new THREE.Scene();
        this.camera = new THREE.PerspectiveCamera(75, canvas.clientWidth / canvas.clientHeight, 0.1, 1000);
        this.renderer = new THREE.WebGLRenderer({ canvas });
    }

    LoadSkin(base64) {
        // Convert the Base64 encoded skin to a data URI
        const skinDataUri = `data:image/png;base64,${base64}`;

        // Load the Minecraft skin image
        this.skinTexture = new THREE.TextureLoader().load(skinDataUri);

        // Apply the UV mapping for the Minecraft skin
        this.skinGeometry.faceVertexUvs[0] = [];

        // Front face (head)
        this.skinGeometry.faceVertexUvs[0].push([
            new THREE.Vector2(8 / 64, 8 / 32),
            new THREE.Vector2(16 / 64, 8 / 32),
            new THREE.Vector2(16 / 64, 16 / 32)
        ]);
        this.skinGeometry.faceVertexUvs[0].push([
            new THREE.Vector2(8 / 64, 8 / 32),
            new THREE.Vector2(16 / 64, 16 / 32),
            new THREE.Vector2(8 / 64, 16 / 32)
        ]);

        // Back face (head)
        this.skinGeometry.faceVertexUvs[0].push([
            new THREE.Vector2(24 / 64, 8 / 32),
            new THREE.Vector2(32 / 64, 8 / 32),
            new THREE.Vector2(32 / 64, 16 / 32)
        ]);
        this.skinGeometry.faceVertexUvs[0].push([
            new THREE.Vector2(24 / 64, 8 / 32),
            new THREE.Vector2(32 / 64, 16 / 32),
            new THREE.Vector2(24 / 64, 16 / 32)
        ]);

        // Right face (body)
        this.skinGeometry.faceVertexUvs[0].push([
            new THREE.Vector2(20 / 64, 20 / 32),
            new THREE.Vector2(28 / 64, 20 / 32),
            new THREE.Vector2(28 / 64, 32 / 32)
        ]);
        this.skinGeometry.faceVertexUvs[0].push([
            new THREE.Vector2(20 / 64, 20 / 32),
            new THREE.Vector2(28 / 64, 32 / 32),
            new THREE.Vector2(20 / 64, 32 / 32)
        ]);

        // Left face (body)
        this.skinGeometry.faceVertexUvs[0].push([
            new THREE.Vector2(44 / 64, 20 / 32),
            new THREE.Vector2(36 / 64, 20 / 32),
            new THREE.Vector2(36 / 64, 32 / 32)
        ]);
        this.skinGeometry.faceVertexUvs[0].push([
            new THREE.Vector2(44 / 64, 20 / 32),
            new THREE.Vector2(36 / 64, 32 / 32),
            new THREE.Vector2(44 / 64, 32 / 32)
        ]);

        // Top face (head and body)
        this.skinGeometry.faceVertexUvs[0].push([
            new THREE.Vector2(20 / 64, 8 / 32),
            new THREE.Vector2(28 / 64, 8 / 32),
            new THREE.Vector2(28 / 64, 16 / 32)
        ]);
        this.skinGeometry.faceVertexUvs[0].push([
            new THREE.Vector2(20 / 64, 8 / 32),
            new THREE.Vector2(28 / 64, 16 / 32),
            new THREE.Vector2(20 / 64, 16 / 32)
        ]);

        // Bottom face (body)
        this.skinGeometry.faceVertexUvs[0].push([
            new THREE.Vector2(32 / 64, 20 / 32),
            new THREE.Vector2(40 / 64, 20 / 32),
            new THREE.Vector2(40 / 64, 32 / 32)
        ]);
        this.skinGeometry.faceVertexUvs[0].push([
            new THREE.Vector2(32 / 64, 20 / 32),
            new THREE.Vector2(40 / 64, 32 / 32),
            new THREE.Vector2(32 / 64, 32 / 32)
        ]);

        // Right leg (front)
        this.skinGeometry.faceVertexUvs[0].push([
            new THREE.Vector2(4 / 64, 20 / 32),
            new THREE.Vector2(8 / 64, 20 / 32),
            new THREE.Vector2(8 / 64, 32 / 32)
        ]);
        this.skinGeometry.faceVertexUvs[0].push([
            new THREE.Vector2(4 / 64, 20 / 32),
            new THREE.Vector2(8 / 64, 32 / 32),
            new THREE.Vector2(4 / 64, 32 / 32)
        ]);

        // Left leg (front)
        this.skinGeometry.faceVertexUvs[0].push([
            new THREE.Vector2(20 / 64, 20 / 32),
            new THREE.Vector2(24 / 64, 20 / 32),
            new THREE.Vector2(24 / 64, 32 / 32)
        ]);
        this.skinGeometry.faceVertexUvs[0].push([
            new THREE.Vector2(20 / 64, 20 / 32),
            new THREE.Vector2(24 / 64, 32 / 32),
            new THREE.Vector2(20 / 64, 32 / 32)
        ]);

        // Right leg (back)
        this.skinGeometry.faceVertexUvs[0].push([
            new THREE.Vector2(44 / 64, 20 / 32),
            new THREE.Vector2(48 / 64, 20 / 32),
            new THREE.Vector2(48 / 64, 32 / 32)
        ]);
        this.skinGeometry.faceVertexUvs[0].push([
            new THREE.Vector2(44 / 64, 20 / 32),
            new THREE.Vector2(48 / 64, 32 / 32),
            new THREE.Vector2(44 / 64, 32 / 32)
        ]);

        // Left leg (back)
        this.skinGeometry.faceVertexUvs[0].push([
            new THREE.Vector2(28 / 64, 20 / 32),
            new THREE.Vector2(32 / 64, 20 / 32),
            new THREE.Vector2(32 / 64, 32 / 32)
        ]);
        this.skinGeometry.faceVertexUvs[0].push([
            new THREE.Vector2(28 / 64, 20 / 32),
            new THREE.Vector2(32 / 64, 32 / 32),
            new THREE.Vector2(28 / 64, 32 / 32)
        ]);

        this.skinMaterial.map = this.skinTexture;

        // Create the skin mesh
        this.skinMaterial = new THREE.MeshBasicMaterial();
        this.skinMesh = new THREE.Mesh(this.skinGeometry, this.skinMaterial);

        // Add the skin mesh to the scene
        this.scene.add(this.skinMesh);

        // Position the camera to see the skin
        this.camera.position.z = 5;
    }

    Render() {
        // Animate the 3D scene
        const animate = () => {
            requestAnimationFrame(animate);

            // Rotate the skin mesh to make it 3D visible
            this.skinMesh.rotation.x += 0.01;
            this.skinMesh.rotation.y += 0.01;

            // Render the scene
            this.renderer.render(this.scene, this.camera);
        };

        // Start the animation loop
        animate();
    }
}