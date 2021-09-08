using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrequencyAnalysisLib
{
    public class TextDocument : Document<string>
    {
        public TextDocument(string path) : base(path) { }

        public override bool ReloadContext()
        {
            return LoadContextFromFile(Path);
        }

        public override bool ReloadContext(string newPath)
        {
            Path = newPath;
            return LoadContextFromFile(Path);
        }

        // Загрузка контента из текстового файла
        private bool LoadContextFromFile(string pathToFile)
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
