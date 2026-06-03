window.initPlanetarySystem = () => {

    const container =
        document.getElementById(
            "planetary-system"
        );

    if (!container) return;

    console.log(
        "Planetary System iniciado",
        container.clientWidth,
        container.clientHeight
    )

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

    // Fondo
    const textureLoader =
        new THREE.TextureLoader();

    scene.background =
        textureLoader.load(
            "/textures/stars_milky.jpg"
        );

    // Cámara
    const camera =
        new THREE.PerspectiveCamera(
            60,
            container.clientWidth /
            container.clientHeight,
            0.1,
            1000
        );

    camera.position.set(
        0,
        20,
        40
    );

    camera.lookAt(
        0,
        0,
        0
    );

    // Luces
    const ambient =
        new THREE.AmbientLight(
            0xffffff,
            0.6
        );

    scene.add(ambient);

    const sunLight =
        new THREE.PointLight(
            0xffffff,
            3
        );

    scene.add(sunLight);

    // SOL
    const sunGeometry =
        new THREE.SphereGeometry(
            4,
            64,
            64
        );

    const sunTexture =
        textureLoader.load(
            `/textures/sun.jpg`
        );

    const sunMaterial =
        new THREE.MeshBasicMaterial({
            map: sunTexture
        });

    const sun =
        new THREE.Mesh(
            sunGeometry,
            sunMaterial
        );

    scene.add(sun);



    //  planetas prueba     A
    // PLANETAS
    const planetas = [

        {
            textura: "mercury.jpg",
            tamaño: 0.4,
            distancia: 7,
            velocidad: 0.004
        },

        {
            textura: "venus.jpg",
            tamaño: 0.7,
            distancia: 10,
            velocidad: 0.003
        },

        {
            textura: "earth.jpg",
            tamaño: 0.8,
            distancia: 13,
            velocidad: 0.0025
        },

        {
            textura: "mars.jpg",
            tamaño: 0.6,
            distancia: 16,
            velocidad: 0.002
        },

        {
            textura: "jupiter.jpg",
            tamaño: 1.8,
            distancia: 21,
            velocidad: 0.0014
        },

        {
            textura: "saturn.jpg",
            tamaño: 1.5,
            distancia: 27,
            velocidad: 0.001,
            anillos: true
        },

        {
            textura: "uranus.jpg",
            tamaño: 1.2,
            distancia: 33,
            velocidad: 0.0007
        },

        {
            textura: "neptune.jpg",
            tamaño: 1.1,
            distancia: 39,
            velocidad: 0.0005
        }
    ];

    const meshesPlanetas = [];


    // CREAR PLANETAS
    planetas.forEach(p => {

        // órbita visual
        const orbitGeometry =
            new THREE.RingGeometry(
                p.distancia - 0.03,
                p.distancia + 0.03,
                128
            );

        const orbitMaterial =
            new THREE.MeshBasicMaterial({

                color: 0x555577,

                side: THREE.DoubleSide,

                transparent: true,

                opacity: 0.35
            });

        const orbit =
            new THREE.Mesh(
                orbitGeometry,
                orbitMaterial
            );

        orbit.rotation.x =
            Math.PI / 2;

        scene.add(orbit);

        // planeta
        const geometry =
            new THREE.SphereGeometry(
                p.tamaño,
                48,
                48
            );

        const texture =
            textureLoader.load(
                `/textures/${p.textura}`
            );

        const material =
            new THREE.MeshStandardMaterial({
                map: texture
            });

        const mesh =
            new THREE.Mesh(
                geometry,
                material
            );

        scene.add(mesh);

        // Saturno
        if (p.anillos) {

            const innerRadius =
                p.tamaño * 1.4;

            const outerRadius =
                p.tamaño * 2.2;

            const ringGeometry =
                new THREE.RingGeometry(
                    innerRadius,
                    outerRadius,
                    128
                );

            // UV corregidos
            const pos =
                ringGeometry.attributes.position;

            const uv =
                ringGeometry.attributes.uv;

            for (
                let i = 0;
                i < pos.count;
                i++
            ) {
                const x =
                    pos.getX(i);

                const y =
                    pos.getY(i);

                const r =
                    Math.sqrt(
                        x * x +
                        y * y
                    );

                const v =
                    (r - innerRadius) /
                    (outerRadius - innerRadius);

                uv.setXY(
                    i,
                    0.5,
                    v
                );
            }

            uv.needsUpdate =
                true;

            const ringTexture =
                textureLoader.load(
                    "/textures/Anillo_Saturno.png"
                );

            ringTexture.wrapS =
                THREE.RepeatWrapping;

            ringTexture.wrapT =
                THREE.ClampToEdgeWrapping;

            ringTexture.center.set(
                0.5,
                0.5
            );

            ringTexture.rotation =
                Math.PI / 2;

            const ringMaterial =
                new THREE.MeshBasicMaterial({

                    map: ringTexture,

                    side: THREE.DoubleSide,

                    transparent: true,

                    opacity: 1
                });

            const ring =
                new THREE.Mesh(
                    ringGeometry,
                    ringMaterial
                );

            ring.rotation.x =
                Math.PI * 0.48;

            ring.rotation.z =
                0.15;

            mesh.add(ring);
        }

        meshesPlanetas.push({

            mesh,

            distancia: p.distancia,

            velocidad: p.velocidad,

            angulo:
                Math.random() *
                Math.PI * 2
        });
    });

    // Animación
    function animate() {

        requestAnimationFrame(
            animate
        );

        sun.rotation.y +=
            0.002;
        meshesPlanetas.forEach(p => {

            p.angulo +=
                p.velocidad;

            p.mesh.position.x =
                Math.cos(
                    p.angulo
                ) * p.distancia;

            p.mesh.position.z =
                Math.sin(
                    p.angulo
                ) * p.distancia;

            p.mesh.rotation.y +=
                0.01;
        });

        renderer.render(
            scene,
            camera
        );
    }

    animate();
};