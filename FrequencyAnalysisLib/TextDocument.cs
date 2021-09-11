using System.IO;

namespace FrequencyAnalysisLib
{
    public class TextDocument : Document<string>
    {
        public TextDocument(string path) : base(path)
        {
            ReloadContent(path);
        }
        public TextDocument() { }

        public override bool ReloadContent()
        {
            return LoadContentFromFile(Path);
        }

        public override bool ReloadContent(string newPath)
        {
            Path = newPath;
            return LoadContentFromFile(Path);
        }

        public override bool SetContent(string content)
        {
            if (content != null)
            {
                Content = content;
                return true;
            }
            return false;
        }

        // Загрузка контента из текстового файла
        private bool LoadContentFromFile(string pathToFile)
        {
            Content = null;

            try
            {
                // Передаем путь к файлу и имя файла конструктору StreamReader.
                using (StreamReader sr = new(pathToFile))
                {
                    // Записываем контент текстового файла
                    Content = sr.ReadToEnd();
                }
            }
            catch
            {
                Content = null;
                return IsValid;
            }

            return IsValid;
        }
    }
}
