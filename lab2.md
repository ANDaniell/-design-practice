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

![Диаграмма классов drawio (1)](https://github.com/user-attachments/assets/62ee448b-ca42-4efa-93df-b331d42c9cc3)

> На данной диаграмме изображена структура классов, связанных с системой кинотеатра, включая классы, их атрибуты и методы. Классы разделены на основные сущности, такие как кинотеатр, фильм, сеанс, билет и другие, которые взаимодействуют друг с другом для реализации функций системы. 

#### Классы и их атрибуты/методы:

1. **ServerApplication** (Серверное приложение)
    
    - **Атрибуты**:
        - `title`: String — название кинотеатра.
        - `address`: String — адрес кинотеатра.
        - `phone_number`: String — номер телефона.
        - `open`: Boolean — статус открытия (открыт/закрыт).
    - **Методы**:
        - `AddMovie(t:title, g:genre)` — метод для добавления фильма.
        - `AddMovieSession(mid:movie.Id, tp:TicketPrice, st:startTime, h:hall, s:seats)` — метод для добавления сеанса фильма.
        - `BuyTicket(sid:session.Id, rs:requestedSeats)` — метод для покупки билета.
        - `GetSessionPrice(sid:sessionId)` — метод для получения цены билета на сеанс.
        - `SessionCheck(*id:*Id, sd:startDate, ed:endDate)` — мктод для проверки сеансов по фильму.
    - **Ассоциации**:
        - `session`: связь с классом **MovieSession** (Сеанс).
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
        var session = new MovieSession(nextSessionId++, movieId, startTime, hall, new List<string>(seats));
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
        var unavailableSeats = requestedSeats.Where(seat => !session.AvailableTickets.Contains(seat)).ToList();
        if (unavailableSeats.Any())
        {
            throw new Exception($"Места {string.Join(", ", unavailableSeats)} недоступны для бронирования.");
        }

        // Создаем список билетов для запрашиваемых мест
        var now_tickets = new List<Ticket>();
        foreach (var seat in requestedSeats)
        {
            var ticket = session.AvailableTickets.Where(seat == session.AvailableTickets.Contains(seat));
            now_tickets.Add(ticket);
            ticket.Valid = false;
            session.AvailableTickets.Remove(seat);  // Удаляем забронированное место из доступных
        }

        return now_tickets;
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
        - `serverApp`: ServerApplication - ссылка на объект ServerApplication для взаимодействия с ним
        - `totalIncome`: Decimal - общий заработок.
        - `serverApp`: ServerApplication - общий заработок.
    - **Методы**:
        - `get_money()` — метод для получения денег, возможно, подсчета общей суммы.
        - `CheckSeatAvailability(sid:sessionId,sn:seatNumber)` — метод проверки доступности места на сеанс

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
            var now_tickets = serverApp.BuyTicket(sessionId, requestedSeats);

            // Добавление стоимости каждого билета к общему доходу кассы
            foreach (var ticket in now_tickets)
            {
                totalIncome += serverApp.GetSessionPrice(sessionId);  // Предполагаем, что ServerApplication имеет метод GetSessionPrice
                soldTickets.Add(ticket);  // Запоминаем купленные билеты через кассу
            }

            return now_tickets;
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
    - **Методы**:
        - `AddMovieRentalContract(mp:monthPayment, ed:endDate, cd:conclusionDate)` — добавление контракта на аренду фильма.
        - `SupplyMovie(sa:serverApp, t:title, g:genre)` — поставка фильма.

```C#
public class Distributor
{
    public string Name { get; set; }
    public List<Contract> Contracts { get; set; } = new List<Contract>();

    public Distributor(string name) { Name = name; }

    // Добавление контракта на прокат фильма
    public void AddMovieRentalContract(decimal monthPayment, string endDate, string conclusionDate)
    {
        var contract = new Contract(monthPayment, endDate, conclusionDate, ContractType.MovieRental);
        Contracts.Add(contract);
    }

    // Поставка фильма кинотеатру
    public void SupplyMovie(ServerApplication serverApp, string title, string genre){ serverApp.AddMovie(title, genre); }
}

```
4. **Contract** (Договор)
    
    - **Атрибуты**:
        - `monthPayment`: Decimal — ежемесячный платеж.
        - `+ end_date`: String — дата окончания действия договора.
        - `conclusionDate`: String — дата заключения договора.
        - `typeOfContract`: enum ContractType — тип контракта.
    - **Ассоциации**:
        - `EmploymentContract`: связь с классом **Worker** (Работник).

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
        - `id`: integer — id фильма.
        - `title`: String — название фильма.
        - `genre`: String — жанр фильма.
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
        - `rowsNumber`: Integer {≥0} — количество рядов в зале.
        - `seatsRowNumber`: Integer {≥0} — количество мест в ряду.
        - `works`: Boolean — статус работы (функционирует/не функционирует).
    - **Методы**:
        - `CheckStatus()` — проверка статуса работы зала.
        - `GenerateSeats()` — метод для генерации и присвоения каждому месту уникальный номер
```C#
public class CinemaHall
{
    public string Cinema { get; private set; }       // Название кинотеатра
    public int Number { get; private set; }          // Уникальный номер зала
    public int RowsNumber { get; private set; }      // Количество рядов в зале
    public int SeatsRowNumber { get; private set; }  // Количество мест в ряду
    public bool Works { get; private set; }          // Статус работы зала (функционирует/не функционирует)
    public Dictionary<int, (int Row, int Seat)> Seats { get; private set; } // Словарь с местами: номер билета -> (ряд, место)


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
        Seats = GenerateSeats(); 

    }

    private Dictionary<int, (int Row, int Seat)> GenerateSeats()
    {
        var seats = new Dictionary<int, (int Row, int Seat)>();
        int ticketId = 1; // Уникальный номер для каждого места

        for (int row = 1; row <= RowsNumber; row++)
        {
            for (int seat = 1; seat <= SeatsRowNumber; seat++)
            {
                seats[ticketId++] = (row, seat); // Добавляем в словарь: номер билета -> (ряд, место)
            }
        }

        return seats;
    }

    // Метод для проверки статуса работы зала
    public string CheckStatus() { return Works ? "Зал функционирует." : "Зал не функционирует."; }
}

```
7. **MovieSession** (Сеанс фильма)
    - **Атрибуты**:
        - `id`: Integer — id сеанса.
        - `movieId`: Integer — id фильма.
        - `ticketprice`: Integer — цена билета на фильм.
        - `startTime`: DateTime — время начала сеанса.
        - `hall`: CinemaHall — зал, в котором фильм.
    - **Ассоциации**:
        - `sessions`: связь с классом **Ticket** (Билет).
     
```C#
public class MovieSession
{
    public int Id { get; set; }
    public int MovieId { get; set; }

    public int TicketPrice { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public CinemaHall Hall { get; set; }
    public List<Ticket> AvailableTickets { get; private set; } // Список доступных мест

    public MovieSession(int id, int movieId,int ticketprice, DateTime startTime, CinemaHall hall)
    {
        Id = id;
        MovieId = movieId;
        TicketPrice = ticketprice;
        StartTime = startTime;
        Hall = hall.id;
        AvailableTickets = new List<Ticket>();
        foreach (var seat in hall.Seats)
        {
            var tid = (Id << 16) | (row << 8) | seat; // создаем id для билета из id сеанса и места в зале
            var ticket = new Ticket(tid, seat.Value.Seat,seat.Value.Row,hall.Number);
            AvailableTickets.Add(ticket);
        }
    }
}
```
8. **Ticket** (Билет)
    
    - **Атрибуты**:
        - `ticketId`: Integer — id номер билета.
        - `sessionId`: Integer — id номер сеанса.
        - `seat`: Integer {≥0} — номер места.
        - `row`: Integer {≥0} — номер ряда.
        - `hallnum`: Integer — номер зада.
```C#     
public class Ticket
{
    public int TicketId { get; set; }
    public int SessionId { get; set; }
    public int HallNumber {get; set;}
    public int Row { get; set; }
    public int Seat { get; set; }
    public bool Valid {get; set;}
    public string BuyingTime {get; set;}

    public Ticket(int ticketId, int sessionId, int seat,int row, int hallnum)
    {
        TicketId = ticketId;
        SessionId = sessionId;
        HallNumber = hallnum;
        Row = row;
        Seat = seat;
        Valid = true;
        BuyingTime = None;
    }
}
```
9. **Customer** (Зритель)
    
    - **Атрибуты**:
        - `name`: String — имя зрителя.
    - **Методы**:
        - `BuyTickets(sa:serverApp, sid:sessionId, s:seats)` — покупка билетов на сеанс.
        - `WatchSession(s:session)` — просмотр сеанса.
      
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
        - `subordinatesNumber`: Integer {≥0} — количество подчиненных.
    - **Методы**:
        - `AssignTask(t:task)` — назначение задач.
        - `EvaluateSubordinates()` — оценка подчиненного.
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
        - `name`: String — полное имя сотрудника.
        - `role`: enum WorkerRole — роль сотрудника.
        - `salary`: Decimal — зарплата сотрудника.
        - `endDate`: String — конечная дата.
        - `conclusionDate`: String — это дата завершения договора.
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

2. **ServerApplication - Contract (Серверное приложение - Контракт)**:
    
    - **Ассоциация**: Связь указывает на то, что серверное приложение управляет контрактами (`Contract`).
    - **Тип**: Агрегация, поскольку контракт может быть частью кинотеатра, но также может быть отделен от него.
3. **CinemaHall - MovieSession (Кинозал - Сеанс фильма)**:
    
    - **Ассоциация (композиция)**: Каждый `CinemaHall` может иметь несколько сеансов (`MovieSession`), которые проводятся в зале.
    - **Тип**: Композиция, так как сеансы не могут существовать без кинозала.
4. **Movie - MovieSession (Фильм - Сеанс фильма)**:
    
    - **Ассоциация**: Связь показывает, что объект `Movie` может иметь несколько сеансов (`MovieSession`), в которых демонстрируется данный фильм.
    - **Тип**: Агрегация, так как сеанс связан с фильмом, но может быть организован для других фильмов.
5. **Distributor - Contract (Дистрибьютор - Договор)**:
    
    - **Ассоциация (агрегация)**: `Distributor` может заключать несколько контрактов (`Contract`) для предоставления фильмов кинотеатру.
    - **Тип**: Агрегация, поскольку контракт является временной связью между дистрибьютором и кинотеатром.
6. **Contract - Distributor (Договор - Дистрибьютор)**:
    
    - **Зависимость**: Класс `Contract` зависит от дистрибьютора (`Distributor`) для заключения и оплаты.
    - **Тип**: Зависимость, так как контракт должен ссылаться на объект `Distributor`.
7. **Movie - Distributor (Фильм - Дистрибьютор)**:
    
    - **Ассоциация**: Показано, что фильм (`Movie`) предоставляется дистрибьютором (`Distributor`) в кинотеатр.
    - **Тип**: Агрегация, так как дистрибьютор поставляет фильм, но фильм может существовать независимо от него.
8. **Ticket - MovieSession (Билет - Сеанс фильма)**:
    
    - **Ассоциация**: `Ticket` привязан к конкретному сеансу фильма (`MovieSession`).
    - **Тип**: Агрегация, так как билет связан с определенным сеансом и может быть аннулирован или передан другому сеансу.
9. **Ticket - Customer (Билет - Зритель)**:
    
    - **Ассоциация**: `Ticket` выдается конкретному зрителю (`Customer`), который его покупает.
    - **Тип**: Ассоциация, так как билет связан с покупателем, но может быть передан другому покупателю.
10. **Supervisor - Worker (Руководитель - Сотрудник)**:
    
    - **Ассоциация (агрегация)**: `Supervisor` управляет несколькими `Worker`, являющимися подчиненными.
    - **Тип**: Агрегация, так как подчиненные являются частью команды под руководством.
11. **Movie - Contract (Фильм - Договор)**:
    
    - **Ассоциация (зависимость)**: `Movie` может быть предметом договора, заключенного с дистрибьютором.
    - **Тип**: Зависимость, так как фильм указывается в контракте на поставку.
12. **ServerApplication - Movie (Серверное приложение - Фильм)**:
    
    - **Ассоциация (агрегация)**: `ServerApplication` может управлять фильмами (`Movie`), добавляя их в базу данных.
    - **Тип**: Агрегация, так как фильмы могут существовать независимо от серверного приложения.

> Эти связи между классами отражают основные бизнес-процессы в кинотеатре: управление залами и сеансами, продажа билетов, заключение контрактов на фильмы, координация работы сотрудников и распределение обязанностей.



## Диаграмма последовательностей
---
### Session Mapping - проверка наличия сеансов фильма на выбранную дату
![Диаграмма_последовательности_5-SessionMapping drawio (3)](https://github.com/user-attachments/assets/b9a3bf09-2a78-43ca-a73e-d52c34d9d0ed)

### Участники (объекты)

1. **Cashbox** — объект, инициирующий процесс вызова метода `SessionCheck`.
2. **ServerApplication** — основной сервер приложения, который обрабатывает запрос.
3. **MovieSession** — объект, представляющий конкретный сеанс фильма.

### Описание последовательности действий

1. **Инициация вызова**:
    
    - Объект `Cashbox` вызывает метод `SessionCheck` у объекта `ServerApplication`, передавая параметры:
        - `movieId` — идентификатор фильма.
        - `startDate` — начальная дата поиска.
        - `endDate` — конечная дата поиска.
2. **Обработка на стороне `ServerApplication`**:
    
    - Метод `SessionCheck` начинает выполнение. Внутри создается цикл `loop`, который проходит по каждому сеансу в списке всех доступных сеансов (`sessions`).
3. **Фильтрация сеансов**:
    
    - Для каждого сеанса выполняется проверка (`opt [filter]`):
        - Сеанс должен соответствовать фильтрам:
            - `movieId` совпадает с идентификатором фильма.
            - Время начала сеанса находится в указанном диапазоне дат (`startDate` ≤ `session.StartTime` ≤ `endDate`).
    - Если фильтр выполнен, сеанс добавляется в результирующий список `filteredSession`.
4. **Возврат результата**:
    
    - После завершения цикла метод возвращает список `filteredSession` в вызывающий объект (`Cashbox`).

### Итог

Диаграмма иллюстрирует логику работы метода `SessionCheck`:

- Перебор всех сеансов (`loop`).
- Проверка условий фильтрации (`opt [filter]`).
- Формирование и возврат списка подходящих сеансов.


---
### Buying Initialization - инициализация запроса на покупку билет(а/ов)
![Диаграмма_последовательности_5-BuyingInitialization drawio](https://github.com/user-attachments/assets/7e581348-f3a4-42e3-849f-2b60c585abc8)
![Uploading Диаграмма_последовательности_5-BuyingInitialization.drawio (1).png…]()

### Участники (объекты)

1. **Customer** — клиент, который инициирует покупку билетов.
2. **ServerApplication** — основной сервер приложения, обрабатывающий запрос.
3. **Ticket** — объект, представляющий билет на сеанс.
4. **Sessions** — объект, представляющий конкретный сеанс фильма.

### Описание последовательности действий:

1. **Инициация покупки билета**:
    
    - Пользователь (`Customer`) вызывает метод `BuyTicket` у объекта `ServerApplication`, передавая:
        - `sessionId` — идентификатор сеанса.
        - `requestedSeats` — список запрашиваемых мест.
2. **Поиск сеанса**:
    
    - В `ServerApplication` выполняется поиск объекта сеанса по `sessionId`:
        - Если сеанс не найден (`session == null`), вызывается исключение **"Сеанс не найден"**, и выполнение прерывается.
3. **Проверка доступности мест**:
    
    - Создается цикл `loop`, проходящий по каждому запрашиваемому месту (`seat`) в `requestedSeats`.
    - Проверяется доступность каждого места в `AvailableTickets`:
        - Если место недоступно, оно добавляется в список `unavailableSeats`.
    - Если есть хотя бы одно недоступное место, выполнение прерывается с исключением **"Места недоступны для бронирования"**.
4. **Создание билетов**:
    
    - После успешной проверки доступности создается второй цикл, где для каждого идентификатора запрашиваемого места (`seatId`) выполняется:
        - Поиск соответствующего билета в списке `AvailableTickets`.
        - Если билет найден, он добавляется в список `now_tickets`:
            - Устанавливаются атрибуты:
                - `BuyingTime = DateTime.UtcNow` — время покупки.
                - `Valid = false` — билет становится недоступным для повторного бронирования.
            - Билет удаляется из списка доступных мест (`AvailableTickets`).
5. **Завершение**:
    
    - Список купленных билетов (`now_tickets`) возвращается пользователю (`Customer`).

---

**Результат:** 
> В итоге интерфейс получает подтверждение о выполнении покупки билета с выбранными местами, и информация о резервировании сохраняется в базе данных с уникальным идентификатором.

---
### Adding Session - добавление фильма в афишу
![Диаграмма_последовательности_5-AddingFilmOnAffiche drawio](https://github.com/user-attachments/assets/4364b7eb-7cf8-4302-86e1-67a78c7a43f9)

![Диаграмма_последовательности_5-AddingMovieSession drawio](https://github.com/user-attachments/assets/4205c0a4-50c0-458f-86fc-33401ee26991)

#### Участники:

1.  **интерфейс**, является промежуточным слоем, который взаимодействует с серверным приложением и базой данных, выполняя задачи по добавлению фильма и постера, а также по инициализации фильма в базе данных.
2.  **серверное** приложение, управляет бизнес-логикой и обработкой основных операций в системе кинотеатра. Оно инициирует и координирует процесс добавления фильма в базу данных и на афишу, связывая интерфейс и базу данных.
3.  **база данных**, хранит информацию о фильмах, постерах и других данных, связанных с кинотеатром. В процессе добавления фильма она отвечает за создание новой записи и присвоение уникального идентификатора (ID) фильму.
#### Описание процесса:

### Описание последовательности:

1. **Admin** вызывает метод `AddMovieSession`, передавая параметры:
    
    - `movieId` – идентификатор фильма.
    - `TicketPrice` – стоимость билета.
    - `startTime` – время начала сеанса.
    - `hallNumber` – номер зала.
    - `seats` – список мест.
2. **MovieService** выполняет проверку:
    
    - Обращается к **MoviesRepository** для поиска фильма с указанным `movieId`.
    - Если фильм не найден, выбрасывается исключение `"Фильм не найден"`.
3. **MovieService** проверяет наличие зала:
    
    - Обращается к **HallsRepository** для поиска зала по `hallNumber`.
4. При успешной проверке:
    
    - Создается объект **MovieSession** с переданными данными:
        - Уникальный идентификатор сеанса.
        - Ссылка на фильм.
        - Цена билета.
        - Время начала.
        - Номер зала.
5. Новый сеанс добавляется в **SessionsRepository**.
    
6. Метод возвращает созданный объект **MovieSession**.

**Результат**:

> Фильм успешно добавлен на афишу кинотеатра. Пользователи могут просматривать его название и постер, а также выбирать его для просмотра.

