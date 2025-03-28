# HanterxHanter

# Запуск базы данных и админ панели

```bash
    cd ./HanterxHanter
    docker compose up
```

# Запуск сервера

```bash
    cd ./HanterxHanter/HanterxHanterApp
    dotnet run
```

# Запуск клиента

```bash
    cd ./HanterxHanter/client
    npm run start
```

# Создание миграций и обновление схемы базы данных

```bash
    #Create migration for moduled project example
    dotnet ef migrations add InitialMigration --startup-project ./HanterxHanterApp/HanterxHanterApp.csproj --project ./DataLayer/DataLayer.csproj --output-dir Migrations

    #Apply migration for moduled project example
    dotnet ef database update --startup-project ./HanterxHanterApp/HanterxHanterApp.csproj --project ../DataLayer/DataLayer.csproj
```

# Описание подходов и примеров работы с файлами и сущностями

## 1. Преобразование DTO

DTO (Data Transfer Object) используется для передачи данных между слоями приложения. Преобразование данных между DTO и сущностями — важный процесс, особенно при работе с такими типами данных, как файлы.

### Преобразование данных

При загрузке файлов из формы (`IFormFile`) или других данных, полученных от пользователя, их нужно преобразовать в DTO и соответствующие сущности. Например, преобразуем загруженный файл в объект `FileDto`, который затем будет преобразован в сущность `File` для хранения в базе данных.

```csharp
    // Инициализация файлов
    foreach (var formFile in FormFiles)
    {
        // Создание DTO для файла
        var fileDto = new FileDto
        {
            FormFile = formFile
        };

        // Инициализация данных в DTO
        fileDto.BeforeExecute(bufferizeFiles);

        // Добавление DTO в список
        FilesDto.Add(fileDto);

        // Создание сущности File и добавление в список
        var file = new File
        {
            FileId = fileDto.SafeFileName
        };

        Files.Add(file);
    }
```

### Важные моменты:

- Важно инициализировать DTO в методах сервисов.
- Преобразовать каждое поле или свойство из FormFile в нужный формат или сущность.

```csharp
    public async Task<ProfileDto> CreateProfileAsync(ProfileDto newProfile)
        {
            try
            {
                // Инициализируем вычисляемые свойства InterestLinks перед сохранением пользователя
                newProfile.BeforeExecute();
    //...
```

## 2. Сохранение файлов и сущностей

После преобразования данных в DTO, необходимо сохранить файлы и связанные сущности. Сущности, такие как `File`, могут быть записаны в базу данных, а сами файлы могут быть сохранены в файловой системе.

```csharp
    // Инициализируем вычисляемые свойства InterestLinks перед сохранением пользователя
    newProfile.BeforeExecute();

    // Маппируем данные из ProfileDto в сущность Profile
    var Profile = _mapper.Map<Profile>(newProfile);
    _uow.Ctx.Profiles.Add(Profile); // Добавляем нового пользователя в контекст

    // Обрабатываем и добавляем файлы пользователя
    foreach (var fileDto in newProfile.FilesDto)
    {
        // Сохраняем файл в файловой системе
        // Внимание: Слепки файлов не проверяются, просто перезаписываем файл.
        // Файловый контекст гарантирует атомарность операций.
        _uow.FileContext.CreateFile(Path.Combine(_rootPath, fileDto.SafeFileName), fileDto.Stream);
        //fileDto.Stream или fileDto.fileBuffer если флаг парсинга bool bufferizeFiles = true;
    }

    // Сохраняем изменения в базе данных в рамках одной транзакции
    await _uow.SaveChangesAsync();
```

### Важные моменты:

- Файлы записываются в файловую систему с помощью метода, который проверяет наличие файла и если он не существует, сохраняет его.
- Для каждого файла создается отдельная сущность в базе данных, которая хранит информацию о файле, такую как имя и путь.

## 3. Использование Disposing для управления ресурсами

Работа с потоками (например, `Stream`) и другими ресурсами, такими как файлы, требует правильного освобождения ресурсов после использования. Это необходимо, чтобы предотвратить утечку памяти и других системных ресурсов.

### Пример использования:

- В классе `FileDto` при работе с потоком нужно удостовериться, что потоки корректно закрыты после завершения работы.

```csharp
    // Реализация IDisposable для освобождения ресурсов
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    // Основной метод Dispose, который освобождает ресурсы
    protected virtual void Dispose(bool disposing)
    {
        // Проверяем, был ли объект уже освобожден
        if (_disposed)
            return;

        // Если disposing == true, это означает, что метод вызван явно, и можно освободить управляемые ресурсы
        if (disposing)
        {
            // Закрытие потока, если он открыт
            if (_stream != null)
            {
                _stream.Dispose();
                _stream = null;
            }

            // Если был скопирован буфер, нужно также освободить память
            if (_fileBuffer != null)
            {
                Array.Clear(_fileBuffer, 0, _fileBuffer.Length); // Очистка буфера
                _fileBuffer = null;
            }

            // Если был передан поток, тоже очищаем
            if (FormFile != null)
            {
                FormFile = null;
            }
        }

        // Отметим, что объект освобожден
        _disposed = true;
    }

    // Финализатор (сработает только если Dispose не был вызван)
    ~FileDto()
    {
        Dispose(false);
    }
```

### Важные моменты:

- Использование интерфейса IDisposable для освобождения ресурсов, таких как потоки.
- Закрытие потоков и других ресурсов после завершения их использования.

## 4. Синхронизация контекста (Unit of Work)

Unit of Work паттерн используется для обеспечения целостности данных. Этот паттерн позволяет синхронизировать операции с базой данных, чтобы все изменения были выполнены в одном транзакционном контексте (Saga).

### Пример использования Unit of Work:

- В сервисах нужно использовать Unit of Work для обработки операций с файлам.

```csharp
    public void Rollback()
    {
        try
        {
            // Откатываем изменения в контексте файловой системы
            FileContext.Rollback();

            // Откатываем транзакцию в контексте базы данных
            Ctx.RollbackTransaction();
        }
        catch (Exception ex)
        {
            // Логируем ошибку отката
            Console.WriteLine("Something went wrong during rollback: {0}", ex.Message);
        }
    }

    public async Task SaveChangesAsync()
    {
        try
        {
            // Начинаем транзакцию в контексте базы данных
            await Ctx.BeginTransactionAsync();

            // Сохраняем изменения в базе данных
            await Ctx.SaveChangesAsync();

            // Сохраняем изменения в файловом хранилище
            FileContext.SaveChanges();

            // Коммитим транзакцию базы данных, завершая ее
            await Ctx.CommitTransactionAsync(Ctx.GetCurrentTransaction());
        }
        catch (Exception ex)
        {
            // Логируем ошибку и откатываем все изменения
            Console.WriteLine("Something went wrong during SaveChangesAsync: {0}", ex.Message);

            Rollback();

            throw;  // Пробрасываем исключение дальше
        }
    }
```

### Важные моменты:

- Использование Unit of Work для управления транзакциями.
- Гарантирует, что все изменения (например, файл в базе данных) сохранятся или откатятся при ошибке.

## Файловый контекст

Файловый контекст - простая реализация базовых файловых операций в виде отложенных шагов с возможностью отката, реализованного по принципу Saga.

### Пример реализации базового интерфейса и поддержка саги:

```csharp
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
               ///execute and add to stack
            }
        }
        catch (Exception ex)
        {
           //...
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


    //публичные методы пользователя
    public void CreateFile(string path, byte[] content)
    {
        //инициализация и добавление в steps ctxStep структуру
    }

    public void CreateFile(string path, Stream stream)
    {
        //инициализация и добавление в steps ctxStep структуру
    }

    // Метод для удаления файла с указанным путем
    public void DeleteFile(string path)
    {
        // Считываем содержимое файла, чтобы в случае отмены создать его заново
        byte[] rollback = _readFile(path);

        //инициализация и добавление в steps ctxStep структуру
    }

    // Метод для проверки существования файла
    private bool FileExists(string path)
    {
        // Возвращаем результат проверки существования файла
    }
```

## Заключение

- Работа с файлами в системе требует внимательного подхода к обработке данных, сохранению файлов и освобождению ресурсов.
- Использование паттернов, таких как Unit of Work, Disposing, и асинхронной обработки, помогает повысить производительность и надежность приложения.
- Необходимо внедрить валидацию данных, логирование и обработку ошибок, чтобы система работала стабильно и предсказуемо.
