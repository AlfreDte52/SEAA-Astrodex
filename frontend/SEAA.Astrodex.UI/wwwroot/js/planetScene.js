window.initPlanetScene =
    (
        planetTexture,
        gravity,
        planetId
    ) => {

        const container =
            document.getElementById(
                "planet-detail-scene"
            );

        if (!container) return;

        container.innerHTML = "";

        // Renderer
        const renderer =
            new THREE.WebGLRenderer({
                antialias: true,
                alpha: true
            });

        renderer.setSize(
            container.clientWidth,
            container.clientHeight
        );

        renderer.setPixelRatio(
            window.devicePixelRatio
        );

        container.appendChild(
            renderer.domElement
        );

        // Escena
        const scene =
            new THREE.Scene();

        // Texture Loader
        const textureLoader =
            new THREE.TextureLoader();

        // Fondo espacial
        const backgroundTexture =
            textureLoader.load(
                "/textures/stars_milky.jpg"
            );

        scene.background =
            backgroundTexture;

        // Cámara
        const camera =
            new THREE.PerspectiveCamera(
                75,
                container.clientWidth /
                container.clientHeight,
                0.1,
                1000
            );

        camera.position.z = 5;

        // Geometría planeta
        const geometry =
            new THREE.SphereGeometry(
                2,
                64,
                64
            );

        // Textura
        const texture =
            textureLoader.load(
                `/textures/${planetTexture}`
            );

        // Material
        const material =
            new THREE.MeshStandardMaterial({
                map: texture
            });

        // Planeta
        const planet =
            new THREE.Mesh(
                geometry,
                material
            );

        scene.add(planet);

        // Referencia global del anillo
        let ring = null;

        // Glow Sol
        if (planetId === "soleil") {
            const glowGeometry =
                new THREE.SphereGeometry(
                    2.3,
                    64,
                    64
                );

            const glowMaterial =
                new THREE.MeshBasicMaterial({

                    color: 0xffdd66,

                    transparent: true,

                    opacity: 0.35
                });

            const glowMesh =
                new THREE.Mesh(
                    glowGeometry,
                    glowMaterial
                );

            scene.add(glowMesh);
        }

        // Saturno
        if (planetId === "saturne") {
            const ringGeometry =
                new THREE.RingGeometry(
                    2.4,
                    3.2,
                    128
                );

            const ringTexture =
                textureLoader.load(
                    "/textures/Anillo_Saturno.png"
                );

            const ringMaterial =
                new THREE.MeshStandardMaterial({

                    map: ringTexture,

                    side: THREE.DoubleSide,

                    transparent: true,

                    opacity: 0.9
                });

            ring =
                new THREE.Mesh(
                    ringGeometry,
                    ringMaterial
                );

            // inclinación
            ring.rotation.x =
                Math.PI * 0.48;

            ring.rotation.y =
                0.15;

            scene.add(ring);
        }

        // Luz principal
        const light =
            new THREE.PointLight(
                0xffffff,
                2
            );

        light.position.set(
            5,
            5,
            5
        );

        scene.add(light);

        // Luz ambiental
        const ambient =
            new THREE.AmbientLight(
                0xffffff,
                0.5
            );

        scene.add(ambient);

        // Animación
        function animate() {
            requestAnimationFrame(
                animate
            );

            const rotationSpeed =
                Math.max(
                    gravity * 0.00015,
                    0.0015
                );

            // planeta
            planet.rotation.y +=
                rotationSpeed;

            // anillos
            if (ring) {
                ring.rotation.z +=
                    0.0005;
            }

            renderer.render(
                scene,
                camera
            );
        }

        animate();
    };