console.log("threeScene.js cargado");

window.initSpaceScene = () => {

    console.log("initSpaceScene ejecutado");

    const container = document.getElementById("space-scene");

    if (!container) return;

    // Escena
    const scene = new THREE.Scene();

    // Cámara
    const camera = new THREE.PerspectiveCamera(
        75,
        container.clientWidth / container.clientHeight,
        0.1,
        1000
    );

    // Renderer
    const renderer = new THREE.WebGLRenderer({
        antialias: true
    });

    renderer.setSize(
        container.clientWidth,
        container.clientHeight
    );

    renderer.setPixelRatio(window.devicePixelRatio);

    container.appendChild(renderer.domElement);

    // Fondo espacial
    scene.background = new THREE.Color(0x000000);

    // Planeta
    const geometry = new THREE.SphereGeometry(
        2,
        64,
        64
    );

    const material = new THREE.MeshStandardMaterial({
        color: 0x3b82f6,
        roughness: 0.8,
        metalness: 0.1
    });

    const planet = new THREE.Mesh(
        geometry,
        material
    );

    scene.add(planet);

    // Luz principal
    const light = new THREE.PointLight(
        0xffffff,
        2
    );

    light.position.set(
        10,
        10,
        10
    );

    scene.add(light);

    // Luz ambiental
    const ambient = new THREE.AmbientLight(
        0x404040,
        1
    );

    scene.add(ambient);

    // Estrellas
    const starGeometry = new THREE.BufferGeometry();
    const starCount = 3000;

    const positions = [];

    for (let i = 0; i < starCount; i++) {

        positions.push(
            (Math.random() - 0.5) * 1000,
            (Math.random() - 0.5) * 1000,
            (Math.random() - 0.5) * 1000
        );
    }

    starGeometry.setAttribute(
        'position',
        new THREE.Float32BufferAttribute(
            positions,
            3
        )
    );

    const starMaterial = new THREE.PointsMaterial({
        color: 0xffffff,
        size: 1
    });

    const stars = new THREE.Points(
        starGeometry,
        starMaterial
    );

    scene.add(stars);

    camera.position.z = 8;

    // Resize
    window.addEventListener(
        'resize',
        () => {

            camera.aspect =
                container.clientWidth /
                container.clientHeight;

            camera.updateProjectionMatrix();

            renderer.setSize(
                container.clientWidth,
                container.clientHeight
            );
        }
    );

    // Animación
    function animate() {

        requestAnimationFrame(animate);

        planet.rotation.y += 0.003;

        stars.rotation.y += 0.0005;

        renderer.render(
            scene,
            camera
        );
    }

    animate();
};