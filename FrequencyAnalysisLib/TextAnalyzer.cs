using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrequencyAnalysisLib
{
    public static class TextAnalyzer
    {
        public static Dictionary<string, int> AnalyzeTripletFrequency(this Document<string> document)
        {
            if (document.IsValid)
            {
                // Создаем словарь. Ключ - триплет, значение - количество повторений в документе
                ConcurrentDictionary<string, int> frequencyOfValues = new ConcurrentDictionary<string, int>();

                var content = SplitStringIntoWords(document.Content);


                // Локальный метод, добавляющий или обновляющий триплеты из переданной строки в словаре
                void AddAllTripletsFromString(string str)
                {
                    for (int i = 0; i < str.Length - 2; i++)
                    {
                        string triplet = (str[i].ToString() + str[i + 1].ToString() + str[i + 2].ToString());
                        frequencyOfValues.AddOrUpdate(triplet, 1, (key, oldValue) => oldValue + 1);
                    }
                }

                // Организуем многопоточность
                Task[] tasks = new Task[content.Count];

                // Добавляем триплеты в словарь
                for (int i = 0; i < tasks.Length; i++)
                {
                    var index = i;
                    tasks[index] = Task.Run(() => AddAllTripletsFromString(content[index]));
                }

                // Ожидание завершения всех задач
                Task.WaitAll(tasks);

                return frequencyOfValues.SortDictionaryByKeys();
            }

            throw new InvalidDocumentException();
        }

        // Метод, сортирующий словарь по значению ключей целочисленного типа в порядке убывания
        private static Dictionary<D, int> SortDictionaryByKeys<D>(this IDictionary<D, int> dictionary) {

            List<KeyValuePair<D, int>> tempList = new List<KeyValuePair<D, int>>(dictionary);
            Dictionary<D, int> sortedDictionary = new Dictionary<D, int>();

            // Сортировка по значению ключей (убывание)
            tempList.Sort((next, first) => {
                return first.Value.CompareTo(next.Value);
            });

            // Запись ключей в словарь
            foreach (var item in tempList)
            {
                sortedDictionary.Add(item.Key, item.Value);
            }

            return sortedDictionary;
        }

        // Выделение всех слов из строки в список (собирает только буквенные слова)
        private static List<string> SplitStringIntoWords(string text)
        {
            List<string> words = new List<string>();

            string temp = "";
            foreach (var symbol in text)
            {
                if (Char.IsLetter(symbol))
                {
                    temp += symbol;
                }
                else
                {
                    if (temp.Length > 2)
                    {
                        words.Add(temp);
                    }
                    temp = "";
                }
            }

            if (temp.Length > 2)
            {
                words.Add(temp);
            }

            return words;
        }
    }
}


