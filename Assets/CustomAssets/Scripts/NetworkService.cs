using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class NetworkService : MonoBehaviour
{
    Material maintexture; // ссылка на текстуру экрана
    private void Start()
    {
        maintexture = GetComponent<Renderer>().material; // инициализируем ссылку на текстуру
        StartCoroutine(ShowImages()); // запускаем корутину, сменяющую изображения
    }
    //массив из 10 изображений для загрузки, замените ссылки на свои!
    private string[] webImages = { "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c0/Cerusite_Les_Frages.jpg/200px-Cerusite_Les_Frages.jpg",
        "https://kor.ill.in.ua/m/610x385/1890677.jpg",
        "https://ic.pics.livejournal.com/world_jewellery/72610970/57054/57054_600.jpg",
        "https://wow.zamimg.com/uploads/screenshots/small/518006.jpg",
        "https://images11.popmeh.ru/upload/article/1ff/1ff384da64f1c39d53abb89f9455f279.jpeg",
        "https://wow.zamimg.com/uploads/screenshots/small/618279.jpg",
        "https://drag-zoloto.ru/wp-content/uploads/2019/09/kvarcevye-kristally-dlja-iscelenija-i-zdorovja.jpg",
        "https://st.depositphotos.com/1010263/4933/i/600/depositphotos_49330349-stock-photo-abstract-blue-background-of-crystal.jpg",
        "https://www.alto-lab.ru/wp-content/uploads/2015/11/kristall-740x391.jpg",
        "https://wow.zamimg.com/uploads/screenshots/small/551524.jpg"
    };
    private Texture[] Images = new Texture[10]; // массив из загруженных изображений
    int i = 0; // счетчик, чтобы знать какое изображение показывается
    IEnumerator ShowImages() // корутина смены изображений
    {
        while (true)
        {
            if (Images[i] == null) // если требуемой текстуры нет в массиве
            {
                WWW www = new WWW(webImages[i]); // загружаем изображение по ссылке       
                yield return www; // ждем когда изображение загрузится
                Images[i] = www.texture; // записываем загруженную текстуру в массив
            }
            maintexture.mainTexture = Images[i]; // устанавливаем текстуру из массива изображений
            i++; // увеличиваем счетчик
            if (i == 10) i = 0; // если загрузили уже 9, возвращаемся к первому
            yield return new WaitForSeconds(3f); // ждем 3 секунды между сменой изображений
        }
    }

}
