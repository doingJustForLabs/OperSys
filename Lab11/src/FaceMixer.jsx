import React, { useState, useRef } from 'react';
import { Camera, CameraResultType, CameraSource } from '@capacitor/camera';

function FaceMixer() {
    const [photo1, setPhoto1] = useState(null);
    const [photo2, setPhoto2] = useState(null);
    const [mixedPhoto, setMixedPhoto] = useState(null);
    const [effectName, setEffectName] = useState('');
    const canvasRef = useRef(null);

    const takePhoto = async (setPhoto) => {
        try {
            const photo = await Camera.getPhoto({
                quality: 90,
                allowEditing: false,
                resultType: CameraResultType.Uri,
                source: CameraSource.Camera
            });
            setPhoto(photo.webPath);
        } catch (error) {
            console.error('Ошибка при съемке фото:', error);
        }
    };

    const mixFaces = () => {
        const canvas = canvasRef.current;
        const ctx = canvas.getContext('2d');

        // Очищаем canvas
        ctx.clearRect(0, 0, canvas.width, canvas.height);

        const img1 = new Image();
        const img2 = new Image();

        // Случайный выбор эффекта
        const effect = Math.floor(Math.random() * 5); // 0-4

        img1.onload = () => {
            img2.onload = () => {
                // Эффект 0: Классическое смешивание половинок
                if(effect === 0) {
                    setEffectName("Классический гибрид");
                    // Левая половина первого изображения
                    ctx.drawImage(img1, 0, 0, img1.width/2, img1.height,
                        0, 0, canvas.width/2, canvas.height);
                    // Правая половина второго изображения
                    ctx.drawImage(img2, img2.width/2, 0, img2.width/2, img2.height,
                        canvas.width/2, 0, canvas.width/2, canvas.height);
                }
                // Эффект 1: Шахматное смешивание
                else if(effect === 1) {
                    setEffectName("Шахматный мутант");
                    const tileSize = 30;
                    for(let y = 0; y < canvas.height; y += tileSize) {
                        for(let x = 0; x < canvas.width; x += tileSize) {
                            const useFirst = (x/tileSize + y/tileSize) % 2 === 0;
                            const img = useFirst ? img1 : img2;
                            ctx.drawImage(img,
                                x/img1.width*img.width,
                                y/img1.height*img.height,
                                tileSize, tileSize,
                                x, y,
                                tileSize, tileSize);
                        }
                    }
                }
                // Эффект 2: Вертикальные полосы
                else if(effect === 2) {
                    setEffectName("Полосатый монстр");
                    const stripWidth = 20;
                    for(let x = 0; x < canvas.width; x += stripWidth) {
                        const useFirst = (x/stripWidth) % 2 === 0;
                        const img = useFirst ? img1 : img2;
                        ctx.drawImage(img,
                            x/img1.width*img.width, 0,
                            stripWidth, img.height,
                            x, 0,
                            stripWidth, canvas.height);
                    }
                }
                // Эффект 3: Половинки с наложением (прозрачность)
                else if(effect === 3) {
                    setEffectName("Призрачное смешение");
                    // Рисуем первое изображение полностью
                    ctx.drawImage(img1, 0, 0, canvas.width, canvas.height);
                    // Устанавливаем прозрачность
                    ctx.globalAlpha = 0.5;
                    // Рисуем второе изображение поверх
                    ctx.drawImage(img2, 0, 0, canvas.width, canvas.height);
                    // Возвращаем прозрачность
                    ctx.globalAlpha = 1.0;
                }
                // Эффект 4: Кривое зеркало (искажение)
                else if(effect === 4) {
                    setEffectName("Кривое зеркало");
                    // Левая половина - нормальная
                    ctx.drawImage(img1, 0, 0, img1.width/2, img1.height,
                        0, 0, canvas.width/2, canvas.height);
                    // Правая половина - искаженная
                    for(let y = 0; y < canvas.height; y += 10) {
                        const wave = Math.sin(y/20) * 10;
                        ctx.drawImage(img2,
                            img2.width/2 + wave, y/img2.height*img2.height,
                            img2.width/2, 10,
                            canvas.width/2, y,
                            canvas.width/2, 10);
                    }
                }

                // Добавляем забавную надпись
                ctx.fillStyle = '#ff00ff';
                ctx.font = 'bold 20px Comic Sans MS';
                ctx.textAlign = 'center';
                ctx.fillText('МУТАНТ v1.0', canvas.width/2, 30);

                // Сохраняем результат
                setMixedPhoto(canvas.toDataURL());
            };
            img2.src = photo2;
        };
        img1.src = photo1;
    };

    return (
        <div className="face-mixer-app">
            <h1 className="app-title">🤪 Смешиватель лиц 🤪</h1>
            <p className="app-subtitle">Создай своего мутанта!</p>

            <div className="instructions">
                Сделай два фото и нажми "Смешать" чтобы создать гибридное лицо!
            </div>

            <div className="photos-container">
                <div className="photo-box">
                    <button className="btn btn-primary" onClick={() => takePhoto(setPhoto1)}>
                        📸 Сделать фото 1
                    </button>
                    {photo1 && <img src={photo1} alt="Фото 1" className="photo-preview" />}
                </div>

                <div className="photo-box">
                    <button className="btn btn-primary" onClick={() => takePhoto(setPhoto2)}>
                        📸 Сделать фото 2
                    </button>
                    {photo2 && <img src={photo2} alt="Фото 2" className="photo-preview" />}
                </div>
            </div>

            {photo1 && photo2 && (
                <button className="btn btn-mix" onClick={mixFaces}>
                    🧬 СМЕШАТЬ ЛИЦА! 🧬
                </button>
            )}

            <div className="canvas-container">
                <canvas
                    ref={canvasRef}
                    width="300"
                    height="300"
                    className="canvas-style"
                ></canvas>
            </div>

            {mixedPhoto && (
                <div className={mixedPhoto ? "bounce-animation" : ""}>
                    <h2 className="result-title">🎉 Ваш мутант готов! 🎉</h2>
                    <div className="effect-name">{effectName}</div>
                    <img
                        src={mixedPhoto}
                        alt="Смешанное фото"
                        className="result-image"
                    />
                </div>
            )}
        </div>
    );
}

export default FaceMixer;