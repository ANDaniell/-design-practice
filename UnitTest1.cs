using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CinemaApp.Tests
{
    public class Tests
    {
        private ServerApplication serverApp;

        [SetUp]
        public void Setup()
        {
            serverApp = new ServerApplication();
        }

        // Test case for adding a movie to the server application
        [Test]
        public void AddMovie_ShouldAddMovieSuccessfully()
        {
            var movie = serverApp.AddMovie("Inception", "Sci-Fi");
            Assert.AreEqual("Inception", movie.Title);
            Assert.AreEqual("Sci-Fi", movie.Genre);
        }

        // Test case for adding a session for a movie
        [Test]
        public void AddMovieSession_ShouldAddSessionSuccessfully()
        {
            var movie = serverApp.AddMovie("Inception", "Sci-Fi");
            var seatIds = new List<int> { 1, 2, 3, 4, 5 };
            var session = serverApp.AddMovieSession(movie.Id, DateTime.Now.AddHours(1), "Main Hall", seatIds);

            Assert.AreEqual(movie.Id, session.MovieId);
            Assert.AreEqual("Main Hall", session.Hall);
            Assert.AreEqual(5, session.AvailableSeats.Count);
        }

        // Test case for purchasing tickets
        [Test]
        public void BuyTicket_ShouldReserveSeatsSuccessfully()
        {
            var movie = serverApp.AddMovie("Inception", "Sci-Fi");
            var seatIds = new List<int> { 1, 2, 3, 4, 5 };
            var session = serverApp.AddMovieSession(movie.Id, DateTime.Now.AddHours(1), "Main Hall", seatIds);

            var requestedSeats = new List<int> { 1, 3 };
            var tickets = serverApp.BuyTicket(session.Id, requestedSeats);

            Assert.AreEqual(2, tickets.Count);
            Assert.IsFalse(session.AvailableSeats.Contains(1));
            Assert.IsFalse(session.AvailableSeats.Contains(3));
        }

        // Test case for a distributor supplying a movie to the server
        [Test]
        public void Distributor_ShouldSupplyMovieSuccessfully()
        {
            var distributor = new Distributor("Warner Bros.");
            distributor.SupplyMovie(serverApp, "Inception", "Sci-Fi");

            var movie = serverApp.AddMovie("Inception", "Sci-Fi");
            Assert.AreEqual("Inception", movie.Title);
            Assert.AreEqual("Sci-Fi", movie.Genre);
        }

        // Test case for Customer buying tickets and session status
        [Test]
        public void Customer_BuyTickets_ShouldReduceAvailableSeats()
        {
            var customer = new Customer("John Doe");
            var movie = serverApp.AddMovie("Interstellar", "Sci-Fi");
            var seatIds = new List<int> { 1, 2, 3, 4 };
            var session = serverApp.AddMovieSession(movie.Id, DateTime.Now.AddHours(1), "Main Hall", seatIds);

            var requestedSeats = new List<int> { 2, 3 };
            var tickets = customer.BuyTickets(serverApp, session.Id, requestedSeats);

            Assert.AreEqual(2, tickets.Count);
            Assert.IsFalse(session.AvailableSeats.Contains(2));
            Assert.IsFalse(session.AvailableSeats.Contains(3));
        }

        // Test case for checking movie session attributes
        [Test]
        public void MovieSession_ShouldInitializeWithCorrectData()
        {
            var hall = new CinemaHall("CinemaOne", 1, 5, 10, true);
            var session = new MovieSession(1, 1, 10, DateTime.Now, hall);

            Assert.AreEqual(50, hall.Seats.Count);  // Assuming 5 rows and 10 seats per row
            Assert.AreEqual("CinemaOne", hall.Cinema);
            Assert.IsTrue(hall.Works);
        }
    }
}

