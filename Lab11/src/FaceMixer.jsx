import React, { useState } from 'react';
import { Camera, CameraResultType, CameraSource } from '@capacitor/camera';

function FaceMixer() {
    const [photo1, setPhoto1] = useState(null);
    const [photo2, setPhoto2] = useState(null);

    const takePhoto = async (setPhotoState) => {
        try {
            const photo = await Camera.getPhoto({
                quality: 90,
                allowEditing: false, // Для простоты не разрешаем редактирование
                resultType: CameraResultType.Uri, // Получаем URI изображения
                source: CameraSource.Camera // Используем камеру
            });
            setPhotoState(photo.webPath);
        } catch (error) {
            console.error('Ошибка при съемке фото:', error);
        }
    };

    const mixFaces = () => {
        // TODO: Логика смешивания изображений на Canvas
        // Получите photo1 и photo2 из состояния
        // Создайте новый Canvas элемент или получите ссылку на существующий
        // Используйте Canvas 2D API для отрисовки частей photo1 и photo2
        // После отрисовки, вы можете получить Data URL из Canvas для отображения, если нужно,
        // или просто отобразить сам Canvas элемент.
        console.log('Начинаем смешивать...');
        // Здесь будет код для смешивания
        // setMixedPhoto(...) // Установить результат, если вы конвертируете Canvas в изображение
    };

    return (
        <div>
            <h1>Смешиватель лиц</h1>

            <button onClick={() => takePhoto(setPhoto1)}>Сделать фото 1</button>
            {photo1 && <img src={photo1} alt="Фото 1" style={{ width: '100px', height: '100px', objectFit: 'cover' }} />}

            <button onClick={() => takePhoto(setPhoto2)}>Сделать фото 2</button>
            {photo2 && <img src={photo2} alt="Фото 2" style={{ width: '100px', height: '100px', objectFit: 'cover' }} />}

            {photo1 && photo2 && (
                <button onClick={mixFaces}>Смешать лица!</button>
            )}

            {/* Здесь будет Canvas для отображения смешанного лица */}
            {/* Или img, если вы конвертируете Canvas в изображение */}
            {/* <canvas id="faceMixCanvas" width="300" height="300"></canvas> */}
            {/* {mixedPhoto && <img src={mixedPhoto} alt="Смешанное фото" />} */}

        </div>
    );
}

export default FaceMixer;