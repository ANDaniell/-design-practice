using System;
using System.Collections.Generic;
using System.Linq;


var serverApp = new ServerApplication();

// Добавляем фильм
var movie = serverApp.AddMovie("Inception", "Sci-Fi");

// Добавляем сеанс для фильма
var session = serverApp.AddMovieSession(movie.Id, new DateTime(2024, 11, 10, 14, 0, 0), "Main Hall", new List<string> { "A1", "A2", "A3", "A4" });

// Покупаем билеты на сеанс
try
{
    var now_tickets = serverApp.BuyTicket(session.Id, new List<string> { "A1", "A2" });
    foreach (var ticket in now_tickets)
    {
        Console.WriteLine($"Ticket ID: {ticket.TicketId}, Session ID: {ticket.SessionId}, Seat: {ticket.Seat}");
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
var distributor = new Distributor("Universal Pictures");
distributor.AddMovieRentalContract(5000, "2025-12-31", "2024-01-01");
distributor.SupplyMovie(serverApp, "Inception", "Sci-Fi");

var worker = new Worker("John Doe", WorkerRole.Cashier, 3000, "2025-01-01", "2024-01-01");

var customer = new Customer("Alice");

try
{
    var now_tickets = customer.BuyTickets(serverApp, session.Id, new List<string> { "A1", "A2" });
    customer.WatchSession(session);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}





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
