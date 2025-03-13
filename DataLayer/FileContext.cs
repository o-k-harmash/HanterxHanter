namespace DataLayer
{
    // Класс для работы с контекстом файлов, который поддерживает транзакционные операции
    // (создание/удаление файлов) с возможностью отката изменений.
    // возможно добавить поддержку паралельной записи файлов паралельно с нагрузкой других контекстов
    // возможно добавить поддержру стримов и асинхронных операций
    public class FileContext
    {
        // Стек для хранения выполненных шагов, которые могут быть отменены (для компенсации)
        private readonly Stack<ctxStep> _executedSteps = new();

        // Список шагов контекста, которые нужно выполнить
        private readonly List<ctxStep> _steps = new();

        // Метод для добавления нового шага в список шагов
        private void _addStep(ctxStep step) => _steps.Add(step);

        // Метод для сохранения изменений (выполнение шагов)
        public void SaveChanges()
        {
            try
            {
                // Выполнение всех шагов и добавление их в стек для компенсации
                foreach (var step in _steps)
                {
                    step.execute();
                    _executedSteps.Push(step); // Добавляем шаг в стек для возможной компенсации
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong: {0}", ex.Message);
                throw; // Пробрасываем исключение для дальнейшей обработки
            }
        }

        // Метод для отката изменений (компенсация выполненных шагов)
        public void Rollback()
        {
            // Компенсируем выполненные шаги
            while (_executedSteps.Count > 0)
            {
                try
                {
                    var step = _executedSteps.Pop();
                    step.compensate(); // Выполняем операцию компенсации
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something went wrong during rollback: {0}", ex.Message);
                }
            }
        }

        // Структура для хранения шагов: операция выполнения и операция компенсации
        private struct ctxStep
        {
            public Action execute { get; set; }    // Операция, которая будет выполнена
            public Action compensate { get; set; }  // Операция компенсации (откат операции)
        }

        // Конструктор класса FileContext
        public FileContext() { }

        // Метод для создания файла с указанным путем и содержимым
        public void CreateFile(string path, byte[] content)
        {
            // Операция для создания файла
            Action executeAction = () => _createFile(path, content);

            // Операция компенсации для удаления файла
            Action compensateAction = () => _deleteFile(path);

            // Добавляем шаг с операцией выполнения и компенсации
            _addStep(new ctxStep
            {
                execute = executeAction,
                compensate = compensateAction
            });
        }

        public void CreateFile(string path, Stream stream)
        {
            // Операция для создания файла
            Action executeAction = () => _writeFile(path, stream);

            // Операция компенсации для удаления файла
            Action compensateAction = () => _deleteFile(path);

            // Добавляем шаг с операцией выполнения и компенсации
            _addStep(new ctxStep
            {
                execute = executeAction,
                compensate = compensateAction
            });
        }

        // Метод для удаления файла с указанным путем
        public void DeleteFile(string path)
        {
            // Считываем содержимое файла, чтобы в случае отмены создать его заново
            byte[] rollback = _readFile(path);

            // Операция для удаления файла
            Action executeAction = () => _deleteFile(path);

            // Операция компенсации для восстановления файла
            Action compensateAction = () => _createFile(path, rollback);

            // Добавляем шаг с операцией выполнения и компенсации
            _addStep(new ctxStep
            {
                execute = executeAction,
                compensate = compensateAction
            });
        }

        // Метод для создания файла на диске
        private void _createFile(string path, byte[] content)
        {
            // Проверка на существование файла и игнорирование операции, если файл уже существует
            if (FileExists(path))
            {
                Console.WriteLine("File {0} already exists.", path);
                return; // Игнорируем операцию
            }

            // Записываем файл на диск
            System.IO.File.WriteAllBytes(path, content);
        }

        // Метод для записи потока в файл на диске
        private void _writeFile(string path, Stream stream)
        {
            // Проверка на существование файла и игнорирование операции, если файл уже существует
            if (FileExists(path))
            {
                Console.WriteLine("File {0} already exists.", path);
                return; // Игнорируем операцию
            }

            // Создание файла и копирование данных из потока
            using (var fileStream = System.IO.File.Create(path))
            {
                stream.CopyTo(fileStream);
            }
        }

        // Метод для удаления файла с диска
        private void _deleteFile(string path)
        {
            // Проверка на существование файла, чтобы избежать исключения при попытке удалить несуществующий файл
            if (!FileExists(path))
            {
                Console.WriteLine("File {0} doesn't exist.", path);
                return; // Игнорируем операцию
            }

            // Удаляем файл с диска
            System.IO.File.Delete(path);
        }

        // Метод для чтения содержимого файла с диска
        private byte[] _readFile(string path)
        {
            // Проверка на существование файла
            if (!FileExists(path))
            {
                return Array.Empty<byte>(); // Возвращаем пустой массив, если файл не существует
            }

            // Читаем содержимое файла
            return System.IO.File.ReadAllBytes(path);
        }

        // Метод для чтения содержимого файла как поток
        private Stream _streamFile(string path)
        {
            // Проверка на существование файла
            if (!FileExists(path))
            {
                return Stream.Null; // Возвращаем пустой поток, если файл не существует
            }

            // Открываем файл для чтения
            return System.IO.File.OpenRead(path);
        }

        // Метод для проверки существования файла
        private bool FileExists(string path)
        {
            return System.IO.File.Exists(path); // Возвращаем результат проверки существования файла
        }
    }
}
