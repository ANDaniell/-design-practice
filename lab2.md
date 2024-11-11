## Диаграмма объектов

![Диаграмма объектов-Страница — 2 drawio](https://github.com/user-attachments/assets/2f6bbb5f-a4fe-49d1-a6ec-adb1040b453b)

> На диаграмме изображена структура объектов, связанных с фильмом, сеансами и билетами в кинотеатре. Основной фильм, для которого проводятся сеансы, связан с конкретными билетами и зрителями, купившими эти билеты. Рассмотрим каждый объект:

#### Основные объекты:

F1: Film

title: "Оглянись" — название фильма.
release_date: "07.11.24" — дата выхода фильма.
genre: "Драма" — жанр фильма.
production: "Киётака Осияма" — продюсер фильма.
length: 58 — продолжительность фильма в минутах.
age_limit: 12 — возрастное ограничение для просмотра фильма.
S1: Seance (Сеанс 1)

date: "09.11.24" — дата проведения сеанса.
time: "19:00" — время начала сеанса.
price: 350 — стоимость билета на данный сеанс.
Связан с двумя билетами (T1 и T2), которые куплены зрителями.
S2: Seance (Сеанс 2)

date: "09.11.24" — дата проведения сеанса.
time: "20:00" — время начала сеанса.
price: 350 — стоимость билета на данный сеанс.
Связан с двумя билетами (T3 и T4), которые куплены зрителями.

Билеты:

T1: Ticket для сеанса S1

row: 3 — ряд.
seat: 53 — место.
seance: "09.11.24 19:00" — дата и время сеанса.
price: 350 — стоимость.
Принадлежит зрителю V1: Васе Пупкину.
T2: Ticket для сеанса S1

row: 4 — ряд.
seat: 71 — место.
seance: "09.11.24 19:00" — дата и время сеанса.
price: 350 — стоимость.
Принадлежит зрителю V2: Кате Ханг.
T3: Ticket для сеанса S2

row: 1 — ряд.
seat: 14 — место.
seance: "09.11.24 20:00" — дата и время сеанса.
price: 350 — стоимость.
Принадлежит зрителю V3: Пете Хатов.
T4: Ticket для сеанса S2

row: 1 — ряд.
seat: 15 — место.
seance: "09.11.24 20:00" — дата и время сеанса.
price: 350 — стоимость.
Принадлежит зрителю V4: Коле Сунину.

Зрители:
- V1: Viewer — Василий Пупкин, владелец билета T1.
- V2: Viewer — Катя Ханг, владелица билета T2.
- V3: Viewer — Пётр Хатов, владелец билета T3.
- V4: Viewer — Коля Сунин, владелец билета T4.

**Общая структура:**
> Диаграмма демонстрирует связь между фильмом, его сеансами и билетами, приобретенными зрителями на эти сеансы. Каждый билет уникален для конкретного сеанса и места в зале, и привязан к определенному зрителю, что позволяет системе отслеживать данные о продажах и месте каждого зрителя на сеансе.

---

## Диаграмма классов

![Диаграмма классов drawio](https://github.com/user-attachments/assets/f10ecd2d-0a76-48f7-848e-0a7aadea5753)

> На данной диаграмме изображена структура классов, связанных с системой кинотеатра, включая классы, их атрибуты и методы. Классы разделены на основные сущности, такие как кинотеатр, фильм, сеанс, билет и другие, которые взаимодействуют друг с другом для реализации функций системы. 

#### Классы и их атрибуты/методы:

1. **ServerApplication** (Серверное приложение)
    
    - **Атрибуты**:
        - `title`: String — название кинотеатра.
        - `address`: String — адрес кинотеатра.
        - `phone_number`: String — номер телефона.
        - `open`: Boolean — статус открытия (открыт/закрыт).
    - **Методы**:
        - `change_status()` — метод для изменения статуса открытия/закрытия кинотеатра.
        - `add_movie(m:Movie)` — метод для добавления фильма.
        - `add_movie_session(ms:MovieSession)` — метод для добавления сеанса фильма.
    - **Ассоциации**:
        - `ticket_windows`: связь с классом **Cashbox** (Касса).
        - `employee`: связь с классом **Worker** (Сотрудник).
        - `halls`: связь с классом **CinemaHall** (Кинозал).
```C#
public class ServerApplication
{
    private List<Movie> movies = new List<Movie>();              // Хранилище фильмов
    private List<MovieSession> sessions = new List<MovieSession>();  // Хранилище сеансов
    private int nextMovieId = 1;                                 // Для генерации уникальных ID фильмов
    private int nextSessionId = 1;                               // Для генерации уникальных ID сеансов
    private int nextTicketId = 1;                                // Для генерации уникальных ID билетов

    // Метод для добавления нового фильма
    public Movie AddMovie(string title, string genre)
    {
        var movie = new Movie(nextMovieId++, title, genre);
        movies.Add(movie);
        return movie;
    }

    // Метод для добавления сеанса к существующему фильму
 public MovieSession AddMovieSession(int movieId, int TicketPrice, DateTime startTime, string hall, List<string> seats)
 {
     // Проверка, существует ли фильм с заданным идентификатором
     var movie = movies.FirstOrDefault(m => m.Id == movieId);
     if (movie == null)
     {
         throw new Exception("Фильм не найден.");
     }

     // Создание сеанса для фильма и добавление его в список сеансов
     var session = new MovieSession(nextSessionId++, movieId, TicketPrice , startTime, hall, new List<string>(seats));
     sessions.Add(session);
     return session;
 }

    // Метод SessionCheck: проверяет наличие сеансов фильма в заданном диапазоне дат и сортирует их по залам
    public List<MovieSession> SessionCheck(int movieId, DateTime startDate, DateTime endDate)
    {
        // Фильтрация сеансов по заданному фильму и диапазону дат
        var filteredSessions = sessions
            .Where(session => session.MovieId == movieId &&
                              session.StartTime >= startDate &&
                              session.StartTime <= endDate)
            .OrderBy(session => session.Hall)         // Сортировка по залу
            .ThenBy(session => session.StartTime)     // Сортировка по времени начала в каждом зале
            .ToList();

        // Возвращаем отсортированный список сеансов
        return filteredSessions;
    }

    // Метод для покупки билета
    public List<Ticket> BuyTicket(int sessionId, List<string> requestedSeats)
    {
        // Находим сеанс по идентификатору
        var session = sessions.FirstOrDefault(s => s.Id == sessionId);
        if (session == null)
        {
            throw new Exception("Сеанс не найден.");
        }

        // Проверяем доступность каждого запрашиваемого места
        var unavailableSeats = requestedSeats.Where(seat => !session.AvailableSeats.Contains(seat)).ToList();
        if (unavailableSeats.Any())
        {
            throw new Exception($"Места {string.Join(", ", unavailableSeats)} недоступны для бронирования.");
        }

        // Создаем билеты для запрашиваемых мест
        var tickets = new List<Ticket>();
        foreach (var seat in requestedSeats)
        {
            var ticket = new Ticket(nextTicketId++, sessionId, seat);
            tickets.Add(ticket);
            session.AvailableSeats.Remove(seat);  // Удаляем забронированное место из доступных
        }

        return tickets;
    }
    public decimal GetSessionPrice(int sessionId)
    {
        // Находим сеанс по его идентификатору
        var session = sessions.FirstOrDefault(s => s.Id == sessionId);

        if (session == null)
        {
            throw new Exception("Сеанс не найден.");
        }

        // Возвращаем цену билета для данного сеанса
        return session.TicketPrice;
    }
}
```
2. **Cashbox** (Касса)
    
    - **Атрибуты**:
        - `number`: Integer {unique, >0} — уникальный номер кассы, больше 0.
        - `working_hours`: String — время работы кассы.
        - `worker`: String — работник, обслуживающий кассу.
    - **Методы**:
        - `get_money()` — метод для получения денег, возможно, подсчета общей суммы.

```C#
public class Cashbox
{
    public int Number { get; private set; }                // Уникальный номер кассы
    public string WorkingHours { get; private set; }       // Время работы кассы
    public string Worker { get; private set; }             // Работник, обслуживающий кассу

    private decimal totalIncome;                           // Общий доход кассы
    private ServerApplication serverApp;                   // Ссылка на объект ServerApplication для взаимодействия с ним
    private List<Ticket> soldTickets;                      // Список проданных через кассу билетов

    public Cashbox(int number, string workingHours, string worker, ServerApplication serverApp)
    {
        if (number <= 0) throw new ArgumentException("Номер кассы должен быть больше 0.");
        
        Number = number;
        WorkingHours = workingHours;
        Worker = worker;
        this.serverApp = serverApp;

        totalIncome = 0;
        soldTickets = new List<Ticket>();
    }

    // Метод для подсчета общего дохода
    public decimal GetMoney()
    {
        return totalIncome;
    }

    // Метод проверки доступности места на сеанс
    public bool CheckSeatAvailability(int sessionId, string seatNumber)
    {
        var session = serverApp.SessionCheck(sessionId, DateTime.Now, DateTime.Now).FirstOrDefault();
        return session != null && session.AvailableSeats.Contains(seatNumber);
    }

    // Метод для покупки билета через кассу
    public List<Ticket> BuyTicket(int sessionId, List<string> requestedSeats)
    {
        try
        {
            // Покупка билетов через ServerApplication
            var tickets = serverApp.BuyTicket(sessionId, requestedSeats);

            // Добавление стоимости каждого билета к общему доходу кассы
            foreach (var ticket in tickets)
            {
                totalIncome += serverApp.GetSessionPrice(sessionId);  // Предполагаем, что ServerApplication имеет метод GetSessionPrice
                soldTickets.Add(ticket);  // Запоминаем купленные билеты через кассу
            }

            return tickets;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при покупке билетов: " + ex.Message);
            return new List<Ticket>();
        }
    }
}

```
3. **Distributor** (Дистрибьютор)
    
    - **Атрибуты**:
        - `name`: String — название организации-дистрибьютора.
        - `TIN`: Integer — идентификационный номер налогоплательщика.
        - `account`: Integer — номер счета организации.
    - **Методы**:
        - `add_movie_rental_contract()` — добавление контракта на аренду фильма.
        - `supply_movie()` — поставка фильма.

```C#
public class Distributor
{
    public string Name { get; set; }
    public List<Contract> Contracts { get; set; } = new List<Contract>();

    public Distributor(string name)
    {
        Name = name;
    }

    // Добавление контракта на прокат фильма
    public void AddMovieRentalContract(decimal monthPayment, string endDate, string conclusionDate)
    {
        var contract = new Contract(monthPayment, endDate, conclusionDate, ContractType.MovieRental);
        Contracts.Add(contract);
    }

    // Поставка фильма кинотеатру
    public void SupplyMovie(ServerApplication serverApp, string title, string genre)
    {
        serverApp.AddMovie(title, genre);
    }
}
```
4. **Contract** (Договор)
    
    - **Атрибуты**:
        - `conclusion_date`: String — дата заключения договора.
        - `end_date`: String — дата окончания действия договора.
        - `month_payment`: Real {≥0} — ежемесячный платеж.
        - `contract_type`: type_of_contract — тип контракта.
    - **Методы**:
        - `conclude_contract(d1:Distributor)` — заключение договора с дистрибьютором.
        - `pay_bill(d2:Distributor)` — оплата по счету дистрибьютору.

```C#
public enum ContractType
{
    MovieRental,       // Прокат фильма
    EquipmentRental,   // Аренда оборудования
    Advertisement,     // Рекламный контракт
    Employment         // Контракт найма сотрудника
}

public class Contract
{
    public decimal MonthPayment { get; set; }    // Ежемесячная плата
    public string EndDate { get; set; }          // Дата окончания
    public string ConclusionDate { get; set; }   // Дата заключения контракта
    public ContractType TypeOfContract { get; set; } // Тип контракта

    public Contract(decimal monthPayment, string endDate, string conclusionDate, ContractType typeOfContract)
    {
        if (monthPayment < 0)
            throw new ArgumentException("Monthly payment must be non-negative");

        MonthPayment = monthPayment;
        EndDate = endDate;
        ConclusionDate = conclusionDate;
        TypeOfContract = typeOfContract;
    }
}
```
5. **Movie** (Фильм)
    
    - **Атрибуты**:
        - `title`: String — название фильма.
        - `release_date`: String — дата выхода фильма.
        - `genre`: String — жанр фильма.
        - `production`: String — продюсер фильма.
        - `length`: Real {≥0} — длительность фильма (в минутах).
        - `age_limit`: Integer — возрастное ограничение.
    - **Методы**:
        - `add_film()` — добавление фильма.

```C#
public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }

    public Movie(int id, string title, string genre)
    {
        Id = id;
        Title = title;
        Genre = genre;
    }
}
```
6. **CinemaHall** (Кинозал)
    
    - **Атрибуты**:
        - `cinema`: String {unique} — название кинотеатра, к которому относится зал (уникально).
        - `number`: Integer {notNull, unique} — уникальный номер зала.
        - `rows_number`: Integer {≥0} — количество рядов в зале.
        - `seats_row_number`: Integer {≥0} — количество мест в ряду.
        - `works`: Boolean — статус работы (функционирует/не функционирует).
    - **Методы**:
        - `check_status()` — проверка статуса работы зала.
```C#
public class CinemaHall
{
    public string Cinema { get; private set; }       // Название кинотеатра (уникальное)
    public int Number { get; private set; }          // Уникальный номер зала
    public int RowsNumber { get; private set; }      // Количество рядов в зале
    public int SeatsRowNumber { get; private set; }  // Количество мест в ряду
    public bool Works { get; private set; }          // Статус работы зала (функционирует/не функционирует)

    // Конструктор класса
    public CinemaHall(string cinema, int number, int rowsNumber, int seatsRowNumber, bool works)
    {
        if (number <= 0)
            throw new ArgumentException("Номер зала должен быть больше 0.");
        if (rowsNumber < 0 || seatsRowNumber < 0)
            throw new ArgumentException("Количество рядов и мест в ряду должно быть неотрицательным.");

        Cinema = cinema;
        Number = number;
        RowsNumber = rowsNumber;
        SeatsRowNumber = seatsRowNumber;
        Works = works;
    }

    // Метод для проверки статуса работы зала
    public string CheckStatus()
    {
        return Works ? "Зал функционирует." : "Зал не функционирует.";
    }
}
```
7. **MovieSession** (Сеанс фильма)
    
    - **Атрибуты**:
        - `date`: String — дата сеанса.
        - `time`: String — время начала сеанса.
        - `price`: Real {≥0} — стоимость билета на сеанс.
    - **Методы**:
        - `get_seances()` — получение списка сеансов фильма.
    - **Ассоциации**:
        - `hall`: связь с классом **CinemaHall** (Кинозал).
     
```C#
public class MovieSession
{
    public int Id { get; set; }
    public int MovieId { get; set; }

    public int TicketPrice { get; set; }
    public DateTime StartTime { get; set; }
    public string Hall { get; set; }
    public List<string> AvailableSeats { get; set; } // Список доступных мест

    public MovieSession(int id, int movieId,int ticketprice, DateTime startTime, string hall, List<string> availableSeats)
    {
        Id = id;
        MovieId = movieId;
        TicketPrice = ticketprice;
        StartTime = startTime;
        Hall = hall;
        AvailableSeats = availableSeats;
    }
}
```
8. **Ticket** (Билет)
    
    - **Атрибуты**:
        - `row`: Integer {≥0} — номер ряда.
        - `seat`: Integer {≥0} — номер места.
        - `seance`: String — идентификатор сеанса.
        - `price`: Integer {≥0} — стоимость билета.
    - **Методы**:
        - `check_ticket(ms:MovieSession)` — проверка билета на указанный сеанс.
        - `sell_ticket(v:Viewer)` — продажа билета указанному зрителю.
```C#     
public class Ticket
{
    public int TicketId { get; set; }
    public int SessionId { get; set; }
    public string Seat { get; set; }

    public Ticket(int ticketId, int sessionId, string seat)
    {
        TicketId = ticketId;
        SessionId = sessionId;
        Seat = seat;
    }
}
```
9. **Customer** (Зритель)
    
    - **Атрибуты**:
        - `name`: String — имя зрителя.
    - **Методы**:
        - `watch_session(v:Viewer)` — просмотр сеанса.
        - `buy_tickets(s:Seance)` — покупка билетов на сеанс.
     
```C#  
public class Customer
{
    public string Name { get; set; }

    public Customer(string name)
    {
        Name = name;
    }

    // Покупка билетов
    public List<Ticket> BuyTickets(ServerApplication serverApp, int sessionId, List<string> seats)
    {
        return serverApp.BuyTicket(sessionId, seats);
    }

    // Просмотр сеанса
    public void WatchSession(MovieSession session)
    {
        Console.WriteLine($"{Name} is watching the movie at session {session.Id} in hall {session.Hall}.");
    }
}
```
10. **Supervisor** (Руководитель)
    
    - **Атрибуты**:
        - `subordinates_number`: Integer {≥0} — количество подчиненных.
    - **Методы**:
        - `assign_task()` — назначение задач.
        - `evaluate_subordinate()` — оценка подчиненного.
```C#     
public class Supervisor : Worker
{
    public int SubordinatesNumber { get; set; }  // Количество подчиненных

    public Supervisor(string name, WorkerRole role, decimal salary, string endDate, string conclusionDate, int subordinatesNumber)
        : base(name, role, salary, endDate, conclusionDate)
    {
        SubordinatesNumber = subordinatesNumber;
    }

    // Дополнительные методы для работы с подчиненными можно добавлять здесь
    public void AssignTask(string task)
    {
        Console.WriteLine($"{Name} assigns task: {task} to their subordinates.");
    }

    public void EvaluateSubordinates()
    {
        Console.WriteLine($"{Name} evaluates {SubordinatesNumber} subordinates.");
    }
}
```
11. **Worker** (Сотрудник)
    
    - **Атрибуты**:
        - `full_name`: String — полное имя сотрудника.
        - `role`: worker_role — роль сотрудника.
```C#  
public enum WorkerRole
{
    Cashier,
    Usher,
    FoodCourtStaff,
    Cleaner,
    Manager
}

public class Worker
{
    public string Name { get; set; }
    public WorkerRole Role { get; set; }
    public Contract EmploymentContract { get; set; }

    public Worker(string name, WorkerRole role, decimal salary, string endDate, string conclusionDate)
    {
        Name = name;
        Role = role;
        EmploymentContract = new Contract(salary, endDate, conclusionDate, ContractType.Employment);
    }
}
```

#### Связи между классами:

1. **ServerApplication - Cashbox (Серверное приложение - Касса)**:
    
    - **Ассоциация (агрегация)**: Связь показывает, что объект `ServerApplication` имеет множество касс (`Cashbox`), которыми он управляет.
    - **Тип**: Агрегация (неплотная связь), поскольку кассы могут существовать независимо от серверного приложения.
2. **ServerApplication - CinemaHall (Серверное приложение - Кинозал)**:
    
    - **Ассоциация (агрегация)**: Связь отражает, что `ServerApplication` управляет несколькими кинозалами (`CinemaHall`).
    - **Тип**: Агрегация, так как залы не зависят от серверного приложения, но принадлежат ему.
3. **ServerApplication - Worker (Серверное приложение - Сотрудник)**:
    
    - **Ассоциация**: Связь указывает на то, что серверное приложение управляет сотрудниками (`Worker`), которые работают в кинотеатре.
    - **Тип**: Агрегация, поскольку сотрудник может быть частью кинотеатра, но также может быть отделен от него.
4. **CinemaHall - MovieSession (Кинозал - Сеанс фильма)**:
    
    - **Ассоциация (композиция)**: Каждый `CinemaHall` может иметь несколько сеансов (`MovieSession`), которые проводятся в зале.
    - **Тип**: Композиция, так как сеансы не могут существовать без кинозала.
5. **Movie - MovieSession (Фильм - Сеанс фильма)**:
    
    - **Ассоциация**: Связь показывает, что объект `Movie` может иметь несколько сеансов (`MovieSession`), в которых демонстрируется данный фильм.
    - **Тип**: Агрегация, так как сеанс связан с фильмом, но может быть организован для других фильмов.
6. **Distributor - Contract (Дистрибьютор - Договор)**:
    
    - **Ассоциация (агрегация)**: `Distributor` может заключать несколько контрактов (`Contract`) для предоставления фильмов кинотеатру.
    - **Тип**: Агрегация, поскольку контракт является временной связью между дистрибьютором и кинотеатром.
7. **Contract - Distributor (Договор - Дистрибьютор)**:
    
    - **Зависимость**: Класс `Contract` зависит от дистрибьютора (`Distributor`) для заключения и оплаты.
    - **Тип**: Зависимость, так как контракт должен ссылаться на объект `Distributor`.
8. **Movie - Distributor (Фильм - Дистрибьютор)**:
    
    - **Ассоциация**: Показано, что фильм (`Movie`) предоставляется дистрибьютором (`Distributor`) в кинотеатр.
    - **Тип**: Агрегация, так как дистрибьютор поставляет фильм, но фильм может существовать независимо от него.
9. **Ticket - MovieSession (Билет - Сеанс фильма)**:
    
    - **Ассоциация**: `Ticket` привязан к конкретному сеансу фильма (`MovieSession`).
    - **Тип**: Агрегация, так как билет связан с определенным сеансом и может быть аннулирован или передан другому сеансу.
10. **Ticket - Customer (Билет - Зритель)**:
    
    - **Ассоциация**: `Ticket` выдается конкретному зрителю (`Customer`), который его покупает.
    - **Тип**: Ассоциация, так как билет связан с покупателем, но может быть передан другому покупателю.
11. **Supervisor - Worker (Руководитель - Сотрудник)**:
    
    - **Ассоциация (агрегация)**: `Supervisor` управляет несколькими `Worker`, являющимися подчиненными.
    - **Тип**: Агрегация, так как подчиненные являются частью команды под руководством.
12. **Movie - Contract (Фильм - Договор)**:
    
    - **Ассоциация (зависимость)**: `Movie` может быть предметом договора, заключенного с дистрибьютором.
    - **Тип**: Зависимость, так как фильм указывается в контракте на поставку.
13. **ServerApplication - Movie (Серверное приложение - Фильм)**:
    
    - **Ассоциация (агрегация)**: `ServerApplication` может управлять фильмами (`Movie`), добавляя их в базу данных.
    - **Тип**: Агрегация, так как фильмы могут существовать независимо от серверного приложения.

> Эти связи между классами отражают основные бизнес-процессы в кинотеатре: управление залами и сеансами, продажа билетов, заключение контрактов на фильмы, координация работы сотрудников и распределение обязанностей.



## Диаграмма последовательностей
---
### Session Mapping - проверка наличия сеансов фильма на выбранную дату
![Диаграмма_последовательности_5-SessionMapping drawio (1)](https://github.com/user-attachments/assets/dfcd7416-5105-4607-bfec-1ba5ddf3c3b5)

#### Участники:

1. интерфейс, через который пользователь или система инициирует проверку.
2. серверное приложение, обрабатывающее запросы.
3. **session**— объект сеанса фильма, содержащий информацию о конкретных показах.

#### Описание процесса:

1. **Запрос SessionCheck** — интерфейс отправляет запрос `SessionCheck(movieId, startDate, endDate)` в `ServerApplication`. Этот запрос инициирует проверку наличия сеансов фильма с указанным `movieId` на диапазон дат от `startDate` до `endDate`.
    
2. **Вызов filterSessions** — `ServerApplication` отправляет запрос `filterSessions(MovieID, startDate, endDate)` объекту `MovieSession`. Этот запрос запрашивает фильтрацию сеансов по указанному идентификатору фильма и диапазону дат.
    
3. **Возвращение member** — объект `MovieSession` возвращает значение `member` обратно в `ServerApplication`. `member` указывает, найден ли сеанс фильма для данного диапазона дат (булевое значение `true`/`false`).
    
4. **Опциональная проверка [member = true]** — если `member` равно `true`, то выполняется добавление `session` в список `filteredSession`. Это условие выполняется только для найденных сеансов.
    
5. **Формирование filteredSession** — после завершения проверки, `ServerApplication` возвращает `filteredSession` обратно в `Interface`, где содержится список сеансов, соответствующих критериям поиска (идентификатору фильма и диапазону дат).

**Результат:** 
> В итоге, `Interface` получает `filteredSession`, который содержит все подходящие сеансы фильма, найденные в заданном интервале дат.

---
### Buying Initialization - инициализация запроса на покупку билет(а/ов)
![Диаграмма_последовательности_5-BuyingInitialization drawio](https://github.com/user-attachments/assets/7e581348-f3a4-42e3-849f-2b60c585abc8)

#### Участники:

1.  **интерфейс**, через который пользователь или система инициирует покупку билета.
2.  **серверное** приложение, которое обрабатывает запросы покупки.
3.  **база данных**, в которой хранится информация о запросах на резервирование мест.
4.  **rr** - объект запроса на резервирование места в базе данных.

#### Описание процесса:

1. **purchaseRequest** — интерфейс отправляет запрос `purchaseRequest(sessionId, hall, place[], paymentInfo)` в `ServerApplication`. Этот запрос содержит идентификатор сеанса, зал, места для бронирования и информацию об оплате.
    
2. **Создание запроса на резервирование** — `ServerApplication` создает новый запрос резервирования, вызывая `new(sessionId, hall, place[])` в `DataBase`. Это инициирует создание объекта `ReservationRequest` с информацией о сеансе и выбранных местах.
    
3. **takeId()** — после создания объекта `ReservationRequest`, он вызывает метод `takeId()` для генерации уникального идентификатора для этого запроса.
    
4. **Присвоение tId** — метод `takeId()` возвращает уникальный идентификатор `tId`, который присваивается запросу на резервирование (`rr.Id = tId`).
    
5. **Резервирование мест** — после получения идентификатора, база данных вызывает `reserving(sessionId, hall, place[], id)`, завершая процесс резервирования указанных мест для данного сеанса с присвоением идентификатора `id`.
    
6. **Возвращение ответа** — `ServerApplication` получает подтверждение о резервировании и возвращает информацию об успешной операции обратно в `Interface`.
    

**Результат:** 
> В итоге интерфейс получает подтверждение о выполнении покупки билета с выбранными местами, и информация о резервировании сохраняется в базе данных с уникальным идентификатором.

---
### Adding Film On Affiche - добавление фильма в афишу
![Диаграмма_последовательности_5-AddingFilmOnAffiche drawio](https://github.com/user-attachments/assets/4364b7eb-7cf8-4302-86e1-67a78c7a43f9)

#### Участники:

1.  **интерфейс**, является промежуточным слоем, который взаимодействует с серверным приложением и базой данных, выполняя задачи по добавлению фильма и постера, а также по инициализации фильма в базе данных.
2.  **серверное** приложение, управляет бизнес-логикой и обработкой основных операций в системе кинотеатра. Оно инициирует и координирует процесс добавления фильма в базу данных и на афишу, связывая интерфейс и базу данных.
3.  **база данных**, хранит информацию о фильмах, постерах и других данных, связанных с кинотеатром. В процессе добавления фильма она отвечает за создание новой записи и присвоение уникального идентификатора (ID) фильму.
#### Описание процесса:

1. **Запрос на добавление фильма**:
    
    - Серверное приложение инициирует процесс добавления фильма, отправляя запрос `addFilm(title, poster)` в интерфейс.
    - Этот запрос содержит параметры `title` (название фильма) и `poster` (изображение постера).
2. **Добавление постера фильма**:
    
    - Интерфейс, получив запрос от серверного приложения, вызывает метод `addPoster(title, poster)`.
    - Метод `addPoster` выполняет добавление или загрузку постера фильма в систему. Это может включать сохранение изображения и связывание его с названием фильма.
    - После успешного добавления постера, интерфейс готов передать данные для создания записи о фильме в базе данных.
3. **Инициализация фильма в базе данных**:
    
    - Интерфейс вызывает метод `initializeFilm(title, poster)` в базе данных, передавая название фильма и постер для создания новой записи о фильме.
    - База данных создает уникальную запись, связывая название фильма с его постером. В процессе создания записи база данных генерирует уникальный идентификатор (ID) для данного фильма, который используется для его идентификации в системе.
    - После успешной инициализации фильма, база данных возвращает этот ID обратно в интерфейс.
4. **Получение ID фильма**:
    
    - Интерфейс получает идентификатор (ID) фильма от базы данных. Этот ID служит для дальнейшего отслеживания и отображения фильма в системе.
5. **Передача ID серверному приложению**:
    
    - Интерфейс отправляет идентификатор (ID) фильма обратно серверному приложению, подтверждая успешное создание и инициализацию фильма в базе данных.
6. **Добавление фильма на афишу**:
    
    - Серверное приложение, получив ID фильма, вызывает метод `affiche.add(Id)` для добавления фильма в афишу.
    - Этот шаг означает, что фильм с указанным ID теперь отображается на афише, и пользователи могут видеть его название и постер среди доступных фильмов.

**Результат**:

> Фильм успешно добавлен на афишу кинотеатра. Пользователи могут просматривать его название и постер, а также выбирать его для просмотра.

