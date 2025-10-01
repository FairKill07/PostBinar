# PostBinar API - Документация для фронтенда

## Базовая информация

- **API версия:** 1.0
- **Базовый URL:** `https://localhost:32773` (для разработки) или тот который пропишешь в Docker при запуске
- **Формат данных:** JSON
- **Кодировка:** UTF-8

## Аутентификация

Все запросы должны содержать соответствующие заголовки аутентификации (токены или cookies), если не указано иное.

---

## API Endpoints

### 👤 User (Управление пользователями)

#### Регистрация пользователя
**POST** `/api/User/Register`

Создает новую учетную запись пользователя в системе.

**Тело запроса:**
```json
{
  "firstName": "string",
  "lastName": "string", 
  "email": "string",
  "password": "string",
  "specializationId": 0
}
```

**Описание полей:**
- `firstName` - Имя пользователя
- `lastName` - Фамилия пользователя
- `email` - Email адрес (используется для входа)
- `password` - Пароль пользователя
- `specializationId` - ID специализации (число)

**Ответ:** `200 OK`

#### Авторизация пользователя
**POST** `/api/User/Login`

Авторизует пользователя в системе.

**Тело запроса:**
```json
{
  "email": "string",
  "password": "string"
}
```

**Описание полей:**
- `email` - Email адрес пользователя
- `password` - Пароль пользователя

**Ответ:** `200 OK`

---

### 📁 Project (Управление проектами)

#### Создать проект
**POST** `/api/Project/Create`

Создает новый проект в системе.

**Тело запроса:**
```json
{
  "name": "string",
  "description": "string",
  "ownerId": "uuid"
}
```

**Описание полей:**
- `name` - Название проекта
- `description` - Описание проекта
- `ownerId` - UUID владельца проекта

**Ответ:** `200 OK`

#### Обновить проект
**PUT** `/api/Project/Update`

Обновляет существующий проект.

**Тело запроса:**
```json
{
  "ownerId": "uuid",
  "projectId": "uuid",
  "name": "string",
  "description": "string"
}
```

**Описание полей:**
- `ownerId` - UUID владельца проекта
- `projectId` - UUID проекта для обновления
- `name` - Новое название проекта
- `description` - Новое описание проекта

**Ответ:** `200 OK`

#### Получить все проекты пользователя
**GET** `/api/Project/GetAllProjects`

Возвращает список всех проектов определенного пользователя.

**Параметры запроса:**
- `userId` (query, UUID) - ID пользователя

**Пример запроса:**
```
GET /api/Project/GetAllProjects?userId=0b2a1236-7030-4bdb-b3b2-a081dccbfc8c
```

**Пример ответа:**
```json
{
  "projects": [
    {
      "projectId": "6d2f6690-adc8-495f-b439-b48d5284ad6e",
      "name": "string",
      "description": "string",
      "createdAt": "2025-09-22T12:22:03.744958+00:00"
    },
    {
      "projectId": "b4e4819f-b487-41ba-a081-ef09d42c6c76",
      "name": "string",
      "description": "string",
      "createdAt": "2025-09-22T12:22:06.669512+00:00"
    }
  ]
}
```

#### Получить проект по ID
**GET** `/api/Project/GetProjectById`

Возвращает детальную информацию о конкретном проекте.

**Параметры запроса:**
- `projectId` (query, UUID) - ID проекта

**Пример запроса:**
```
GET /api/Project/GetProjectById?projectId=de8f07d1-a8b4-4a31-9a07-8ef9dc2e7b5d
```

**Пример ответа:**
```json
{
  "name": "string",
  "description": "string",
  "ownerId": {
    "value": "0b2a1236-7030-4bdb-b3b2-a081dccbfc8c"
  },
  "createdAt": "2025-09-23T19:09:26.271743+00:00",
  "updatedAt": "0001-01-01T00:00:00+00:00",
  "isActive": true,
  "projectMemberships": [],
  "tasks": [],
  "notes": [],
  "id": {
    "value": "de8f07d1-a8b4-4a31-9a07-8ef9dc2e7b5d"
  }
}
```

**Описание полей ответа:**
- `name` - Название проекта
- `description` - Описание проекта
- `ownerId.value` - UUID владельца проекта
- `createdAt` - Дата создания (ISO 8601)
- `updatedAt` - Дата последнего обновления (ISO 8601)
- `isActive` - Статус активности проекта
- `projectMemberships` - Список участников проекта
- `tasks` - Список задач проекта
- `notes` - Список заметок проекта
- `id.value` - UUID проекта

#### Удалить проект
**DELETE** `/api/Project/Delete/{id}`

Удаляет проект из системы.

**Параметры пути:**
- `id` (path, UUID, обязательный) - ID проекта для удаления

**Пример запроса:**
```
DELETE /api/Project/Delete/6d2f6690-adc8-495f-b439-b48d5284ad6e
```

**Ответ:** `200 OK`

---

### 👥 ProjectMemberships (Управление участниками проекта)

#### Добавить участника в проект
**POST** `/api/ProjectMemberships/AddMember`

Добавляет пользователя в качестве участника проекта.

**Тело запроса:**
```json
{
  "projectId": "uuid",
  "userId": "uuid",
  "role": "string"
}
```

**Описание полей:**
- `projectId` - UUID проекта
- `userId` - UUID пользователя
- `role` - Роль пользователя в проекте (опционально)

**Ответ:** `200 OK`

---

### 🎯 Specialization (Управление специализациями)

#### Создать специализацию
**POST** `/api/Specialization/Create`

Создает новую специализацию в системе.

**Тело запроса:**
```json
{
  "name": "string",
  "colorCode": "string"
}
```

**Описание полей:**
- `name` - Название специализации
- `colorCode` - Цветовой код специализации (например, "#FF5733")

**Ответ:** `200 OK`

#### Получить все специализации
**GET** `/api/Specialization/GetAllSpecializations`

Возвращает список всех доступных специализаций.

**Пример ответа:**
```json
{
  "specializations": [
    {
      "id": 1,
      "name": "Frontend Developer",
      "colorCode": "#3498db"
    },
    {
      "id": 2,
      "name": "Backend Developer", 
      "colorCode": "#e74c3c"
    },
    {
      "id": 3,
      "name": "UI/UX Designer",
      "colorCode": "#9b59b6"
    },
    {
      "id": 4,
      "name": "DevOps Engineer",
      "colorCode": "#f39c12"
    },
    {
      "id": 5,
      "name": "QA Engineer",
      "colorCode": "#2ecc71"
    },
    {
      "id": 6,
      "name": "Product Manager",
      "colorCode": "#34495e"
    },
    {
      "id": 7,
      "name": "Data Scientist",
      "colorCode": "#1abc9c"
    }
  ]
}
```

**Описание полей ответа:**
- `id` - Уникальный ID специализации (число)
- `name` - Название специализации
- `colorCode` - Цветовой код для визуального отображения

#### Удалить специализацию
**DELETE** `/api/Specialization/Delete/{id}`

Удаляет специализацию из системы.

**Параметры пути:**
- `id` (path, integer, обязательный) - ID специализации для удаления

**Пример запроса:**
```
DELETE /api/Specialization/Delete/5
```

**Ответ:** `200 OK`

---


### 📎 FileStorages (Управление файлами)

#### Загрузить файл для проекта
**POST** `/api/FileStorages/UploadFileForProject`

Загружает файл, связанный с проектом.

**Тело запроса:** `multipart/form-data`
```
ProjectId.Value: uuid
ObjectId: uuid
file: binary
```

**Описание полей:**
- `ProjectId.Value` - UUID проекта
- `ObjectId` - UUID объекта (самого проекта)
- `file` - Бинарный файл для загрузки

**Ответ:** `200 OK`

#### Загрузить файл для заметки
**POST** `/api/FileStorages/UploadFileForNote`

Загружает файл, связанный с заметкой.

**Тело запроса:** `multipart/form-data`
```
ProjectId.Value: uuid
ObjectId: uuid
file: binary
```

**Описание полей:**
- `ProjectId.Value` - UUID проекта
- `ObjectId` - UUID заметки
- `file` - Бинарный файл для загрузки

**Ответ:** `200 OK`

#### Загрузить файл для задачи
**POST** `/api/FileStorages/UploadFileForTask`

Загружает файл, связанный с задачей.

**Тело запроса:** `multipart/form-data`
```
ProjectId.Value: uuid
ObjectId: uuid
file: binary
```

**Описание полей:**
- `ProjectId.Value` - UUID проекта
- `ObjectId` - UUID задачи
- `file` - Бинарный файл для загрузки

**Ответ:** `200 OK`

#### Получить файлы проекта
**GET** `/api/FileStorages/GetFilesByProject`

Возвращает список всех файлов, связанных с проектом.

**Параметры запроса:**
- `projectId` (query, UUID) - ID проекта

**Пример запроса:**
```
GET /api/FileStorages/GetFilesByProject?projectId=de8f07d1-a8b4-4a31-9a07-8ef9dc2e7b5d
```

**Пример ответа:**
```json
{
  "files": [
    {
      "fileSorageId": "a3c2e1f4-5b6a-4c8d-9e0f-1a2b3c4d5e6f",
      "fileName": "document.pdf",
      "mimeType": "application/pdf",
      "size": 1048576,
      "createdAt": "2025-09-23T15:30:00.000000+00:00"
    },
    {
      "fileSorageId": "b4d3e2f5-6c7b-5d9e-0f1a-2b3c4d5e6f7g",
      "fileName": "image.png",
      "mimeType": "image/png",
      "size": 524288,
      "createdAt": "2025-09-23T16:45:00.000000+00:00"
    }
  ]
}
```

**Описание полей ответа:**
- `fileSorageId` - UUID файла в хранилище
- `fileName` - Имя файла
- `mimeType` - MIME тип файла
- `size` - Размер файла в байтах
- `createdAt` - Дата загрузки файла (ISO 8601)

#### Получить файлы заметки
**GET** `/api/FileStorages/GetFilesByNote`

Возвращает список всех файлов, связанных с заметкой.

**Параметры запроса:**
- `noteId` (query, UUID) - ID заметки

**Пример запроса:**
```
GET /api/FileStorages/GetFilesByNote?noteId=c5e4f3a6-7d8c-6e9f-1a2b-3c4d5e6f7g8h
```

**Ответ:** Аналогичен GetFilesByProject

#### Получить файлы задачи
**GET** `/api/FileStorages/GetFilesByTask`

Возвращает список всех файлов, связанных с задачей.

**Параметры запроса:**
- `taskId` (query, UUID) - ID задачи

**Пример запроса:**
```
GET /api/FileStorages/GetFilesByTask?taskId=d6f5g4b7-8e9d-7f0a-2b3c-4d5e6f7g8h9i
```

**Ответ:** Аналогичен GetFilesByProject

#### Получить URL для скачивания файла
**GET** `/api/FileStorages/GetFileDownloadUrl`

Возвращает URL для скачивания конкретного файла.

**Параметры запроса:**
- `fileStorageId` (query, UUID) - ID файла в хранилище

**Пример запроса:**
```
GET /api/FileStorages/GetFileDownloadUrl?fileStorageId=a3c2e1f4-5b6a-4c8d-9e0f-1a2b3c4d5e6f
```

**Пример ответа:**
```json
{
  "downloadUrl": "https://storage.example.com/files/a3c2e1f4-5b6a-4c8d-9e0f-1a2b3c4d5e6f",
  "expiresAt": "2025-10-01T20:00:00.000000+00:00"
}
```

**Описание полей ответа:**
- `downloadUrl` - Прямая ссылка для скачивания файла
- `expiresAt` - Время истечения ссылки (опционально)

---

## Коды ответов

| Код | Описание |
|-----|----------|
| 200 | OK - Запрос выполнен успешно |
| 400 | Bad Request - Некорректные данные запроса |
| 401 | Unauthorized - Требуется аутентификация |
| 403 | Forbidden - Доступ запрещен |
| 404 | Not Found - Ресурс не найден |
| 500 | Internal Server Error - Внутренняя ошибка сервера |

## Форматы данных

### UUID
Все ID в системе используют формат UUID (например: `6d2f6690-adc8-495f-b439-b48d5284ad6e`)

### Даты
Все даты передаются в формате ISO 8601 с timezone (например: `2025-09-22T12:22:03.744958+00:00`)
