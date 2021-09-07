using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrequencyAnalysisLib
{
    public abstract class Document<T>
    {
        // Путь к файлу
        public string Path { get; protected set; }

        // Содержимое файла
        public T Content { get; protected set; }

        // Проверка, валидный ли файл
        public bool IsValid
        {
            get
            {
                if (Content != null)
                {
                    return true;
                }

                return false;
            }
        }

        // Конструкторы
        public Document(string path)
        {
            Path = path;
        }
        public Document() { }

        // Публичные методы
        public abstract bool ReloadContext();
        public abstract bool ReloadContext(string newPath);
    }
}
