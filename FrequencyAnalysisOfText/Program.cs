using FrequencyAnalysisLib;
using System;
using System.Diagnostics;

namespace FrequencyAnalysisOfText
{
    class Program
    {
        static void Main()
        {
            // Создаем экземпляр таймера
            Stopwatch stopWatch = new Stopwatch();

            Console.WriteLine("Введите путь к текстовому документу: ");

            var path = Console.ReadLine();
            // Запуск таймера
            stopWatch.Start();

            // Создаем экземпляр TextDocument
            var document = new TextDocument();
            // Передаем TextDocument пусть к текстовому файлу
            document.ReloadContent(path);

            // Если документ невалиден, запрашиваем путь еще раз
            while (!document.IsValid)
            {
                // Сбрасываем таймер
                stopWatch.Reset();

                Console.WriteLine("Путь указан неверно, повторите: ");

                path = Console.ReadLine();
                stopWatch.Start();
                document.ReloadContent(path);
            }

            // Получаем отсортированный словарь триплетов из контента документа
            var tripletsNumber = document.AnalyzeTripletFrequency();

            Console.WriteLine();

            // Объявляем счетчик
            int index = 0;
            // Выводим максимум 10 триплетов и их повторений в консоль в порядке убывания
            foreach (var item in tripletsNumber)
            {
                Console.Write($"{item.Key}");

                index++;
                if (index == 10)
                {
                    Console.Write("\n ");
                    break;
                }
                Console.Write(", ");
            }

            // Останавливаем таймер выполнения программы
            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;

            // Задаем формат отображаемого времени
            string elapsedTime = String.Format("{0}:{1}:{2}.{3} мс",
            ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);

            Console.WriteLine("\nВремя выполнения программы: " + elapsedTime);
            Console.ReadKey();
        }
    }
}

