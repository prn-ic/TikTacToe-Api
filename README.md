# TikTacToe-Api
TikTakToe-Api - WebApi для игры в крестики-нолики
# Описание
Данное Api реализовано в RESTful стиле, также была соблюдена 3 нормальная форма базы данных, и проект был разбит на 3 составляющие: <br/>
- _Модели_ (TikTakToe.Domain)
- _Абстракция-доступ к базе данных_ (TikTakToe.DataAccess.EntityFramework)
- _Api, соответственно_ (TikTakToe.Api) <br/>
_ER - диаграмма_<br/>
![alt-text](https://sun9-44.userapi.com/impg/Av8cxJ7fINHUtJr6DUXCFKe7IrOdXwk4JqhJNw/cFVLaHLAiKw.jpg?size=583x404&quality=96&sign=354c820be3e3d6dfa201505a5f775d14&type=album)
### Как API определяет победителя?
Все очень просто. В играх существуют некие комбинации действий, по которым можно определить, выиграл ли тот или иной человек или же нет. В крестиках-ноликах также присутстуют комбинации, по которым Api и проверяет результат.

# Endpoint-ы
## Game
- _https://localhost:7241/api/Game_ - Получение всех игр (method: GET)
- _https://localhost:7241/api/Game_ - Создание игровой сессии (method: POST)

- _https://localhost:7241/api/Game/?id={id}_ - Получение определенной игровой сессии по Id (method: GET, params: id - int)

- _https://localhost:7241/api/Game/?id={id}_ - Изменение определенной игровой сессии по Id (method: PUT, params: id - int)

- _https://localhost:7241/api/Game/?id={id}_ - Удаление определенной игровой сессии по Id (method: DELETE, params: id - int)

- _https://localhost:7241/api/Game/step/?userId={userId}&gameId={gameId}&step={step}_ - Сделать шаг в игровой сессии (method: POST, params: userId - int, gameId - int, step - int)

## Step
- _https://localhost:7241/api/Step_ - Получение всех шагов (method: GET)

- _https://localhost:7241/api/Step_ - Создание шага (method: POST)

- _https://localhost:7241/api/Step/?id={id}_ - Получение определенного шага по Id (method: GET, params: id - int)

- _https://localhost:7241/api/Step/?id={id}_ - Изменение определенного шага по Id (method: PUT, params: id - int)

- _https://localhost:7241/api/Step/?id={id}_ - Удаление определенного шага по Id (method: DELETE, params: id - int)

## User

- _https://localhost:7241/api/User_ - Получение всех пользователей (method: GET)

- _https://localhost:7241/api/User_ - Создание пользователя (method: POST)

- _https://localhost:7241/api/User/?id={id}_ - Получение определенного пользователя по Id (method: GET, params: id - int)

- _https://localhost:7241/api/User/?id={id}_ - Изменение определенного пользователя по Id (method: PUT, params: id - int)

- _https://localhost:7241/api/User/?id={id}_ - Удаление определенного пользователя по Id (method: DELETE, params: id - int)
