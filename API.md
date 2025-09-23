# PostBinar API - Документация для фронтенда

## Базовая информация

- **API версия:** 1.0
- **Базовый URL:** `https://localhost:32773` (для разработки)
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

