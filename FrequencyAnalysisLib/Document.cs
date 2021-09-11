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
        public Document() { }
        public Document(string path)
        {
            Path = path;
        }

        // Публичные методы
        public abstract bool ReloadContent();
        public abstract bool ReloadContent(string newPath);
        public abstract bool SetContent(string content);
    }
}
