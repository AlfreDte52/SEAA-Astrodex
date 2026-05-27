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

    const material = new THREE.MeshPhysicalMaterial({
        color: 0x4f8dfd,

        roughness: 0.65,
        metalness: 0.08,

        clearcoat: 0.35,
        clearcoatRoughness: 0.5,

        emissive: 0x0f172a,
        emissiveIntensity: 0.08
    });

    const planet = new THREE.Mesh(
        geometry,
        material
    );

    scene.add(planet);

    // Glow del planeta

    /*
    const glowGeometry = new THREE.SphereGeometry(
        2.25,
        64,
        64
    );

    const glowMaterial = new THREE.MeshBasicMaterial({
        color: 0x60a5fa,
        transparent: true,
        opacity: 0.18
    });

    const glow = new THREE.Mesh(
        glowGeometry,
        glowMaterial
    );

    scene.add(glow);

    */


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

    // ===== ESTRELLAS MULTICAPA =====

    function createStars(count, size, distance) {

        const geometry =
            new THREE.BufferGeometry();

        const positions = [];

        for (let i = 0; i < count; i++) {

            positions.push(
                (Math.random() - 0.5) * distance,
                (Math.random() - 0.5) * distance,
                (Math.random() - 0.5) * distance
            );
        }

        geometry.setAttribute(
            'position',
            new THREE.Float32BufferAttribute(
                positions,
                3
            )
        );

        const material =
            new THREE.PointsMaterial({
                color: 0xffffff,
                size: size,
                sizeAttenuation: true
            });

        return new THREE.Points(
            geometry,
            material
        );
    }

    const starsFar =
        createStars(
            2500,
            0.35,
            1200
        );

    const starsMid =
        createStars(
            1800,
            0.6,
            900
        );

    const starsNear =
        createStars(
            900,
            1,
            700
        );

    scene.add(starsFar);
    scene.add(starsMid);
    scene.add(starsNear);

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
       

        starsFar.rotation.y += 0.00015;

        starsMid.rotation.y += 0.0004;

        starsNear.rotation.y += 0.0008;

        renderer.render(
            scene,
            camera
        );
    }

    animate();
};