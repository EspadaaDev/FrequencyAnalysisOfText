using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace FrequencyAnalysisLib
{
    public static class TextAnalyzer
    {
        public static Dictionary<string, int> AnalyzeTripletFrequency(this Document<string> document)
        {
            if (document.IsValid)
            {
                // Проверка, можно ли составить хотя бы один триплет
                if (document.Content.Length < 3)
                {
                    return null;
                }

                // Создаем словарь. Ключ - триплет, значение - количество повторений в документе
                ConcurrentDictionary<string, int> frequencyOfValues = new ConcurrentDictionary<string, int>();

                var charContent = document.Content.ToCharArray();
                // Получаем количество всех триплетов
                var numOfTriplets = charContent.Length - 2;
                // Получаем нужное количество потоков по количеству триплетов
                int numOfTasks = (numOfTriplets / 100) + 1;


                // Организуем многопоточность
                int CountofWorkThreads; // Переменная для хранения значения максимального количества потоков в пуле.
                int CountofImputOutputThreads; // Переменная для хранения количества потоков ввода-вывода в пуле.
                ThreadPool.GetMaxThreads(out CountofWorkThreads, out CountofImputOutputThreads); // Инициируем переменные.
                Thread[] threads = new Thread[numOfTasks];

                void AddAllTripletsFromString(string str)
                {
                    for (int i = 0; i < str.Length - 2; i++)
                    {
                        string triplet = (str[i] + str[i + 1] + str[i + 2]).ToString();
                        frequencyOfValues.AddOrUpdate(triplet, 1, (key, oldValue) => oldValue + 1);
                    }
                    
                }

                Task[] tasks = new Task[numOfTasks];
                // Добавляем триплеты в словарь
                for (int i = 0; i < tasks.Length; i++)
                {
                    tasks[i] = Task.Run(() => AddAllTripletsFromString("123"));
                }

                Task.WaitAll(tasks);

                return new Dictionary<string, int>(frequencyOfValues);
            }

            return null;
        }


        // Класс - параметр для передачи в поток
        private class DictionaryAndString
        {
            public readonly ConcurrentDictionary<string, int> FrequencyOfValues;
            public readonly string Str;
            public DictionaryAndString(string str, ConcurrentDictionary<string, int> dictionary)
            {
                FrequencyOfValues = dictionary;
                Str = str;
            }
        }
    }
}


