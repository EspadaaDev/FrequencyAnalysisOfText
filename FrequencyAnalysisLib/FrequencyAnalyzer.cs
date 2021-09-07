using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace FrequencyAnalysisLib
{
    public class FrequencyAnalyzer
    {



        public Dictionary<string, int> GetDictionaryOfTheTripletsNumber(Document<string> document)
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

                // Получаем количество всех триплетов
                var numOfTriplets = document.Content.Length - 2;
                // Получаем нужное количество потоков по количеству триплетов
                int numOfThreads = (numOfTriplets / 100) + 1;


                // Организуем многопоточность
                Thread[] threads = new Thread[numOfThreads];


                // Добавляем триплеты в словарь
                for (int i = 0; i < numOfThreads; i++)
                {
                    threads[i] = new Thread(new ParameterizedThreadStart(AddAllTripletsFromString));
                    threads[i].Start();
                }

                return new Dictionary<string, int>(frequencyOfValues);
            }

            return null;
        }

        private void AddAllTripletsFromString(object str)
        {


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


