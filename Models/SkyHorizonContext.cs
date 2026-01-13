using Microsoft.EntityFrameworkCore;

namespace SkyHorizon_2223262.Models
{
    public class SkyHorizonContext : DbContext
    {
        public SkyHorizonContext(DbContextOptions<SkyHorizonContext> options) : base(options) { }

        public DbSet<Destination> Destinations { get; set; } = null!;
        public DbSet<Booking> Bookings { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Seed data for Destinations based on the original JS array
            modelBuilder.Entity<Destination>().HasData(
                new Destination { Id = 1, Name = "Paris", Country = "França", Description = "A Cidade da Luz, conhecida pela Torre Eiffel, museus de classe mundial e culinária excelente.", Image = "y34Q4sYi9OZt.jpeg", DistanceFromPortugal = 1500 },
                new Destination { Id = 2, Name = "Barcelona", Country = "Espanha", Description = "Uma cidade vibrante com arquitetura única de Gaudí, praias e vida noturna animada.", Image = "OG3SilvlAOi7.jpg", DistanceFromPortugal = 800 },
                new Destination { Id = 3, Name = "Roma", Country = "Itália", Description = "A capital histórica com o Coliseu, Vaticano e uma riqueza de arte e cultura.", Image = "4upKqfzcjuOq.jpeg", DistanceFromPortugal = 2000 },
                new Destination { Id = 4, Name = "Amesterdão", Country = "Holanda", Description = "Canais pitorescos, museus renomados e uma atmosfera acolhedora e descontraída.", Image = "Som3MyMD31GN.jpg", DistanceFromPortugal = 1800 },
                new Destination { Id = 5, Name = "Berlim", Country = "Alemanha", Description = "Uma cidade histórica com arte moderna, vida noturna vibrante e história rica.", Image = "hgYoXazpqTwj.jpg", DistanceFromPortugal = 2200 },
                new Destination { Id = 6, Name = "Londres", Country = "Reino Unido", Description = "Capital britânica com Big Ben, Palácio de Buckingham e museus fascinantes.", Image = "1WygKbuiacbx.jpg", DistanceFromPortugal = 1600 },
                new Destination { Id = 7, Name = "Istambul", Country = "Turquia", Description = "Uma cidade mágica entre dois continentes, com história, cultura e gastronomia.", Image = "9fkKFRmZZIau.jpg", DistanceFromPortugal = 2800 },
                new Destination { Id = 8, Name = "Dubai", Country = "Emirados Árabes", Description = "Luxo moderno no deserto, com arranha-céus impressionantes e compras de classe mundial.", Image = "ilDnCFg2XPsH.jpg", DistanceFromPortugal = 5000 },
                new Destination { Id = 9, Name = "Tóquio", Country = "Japão", Description = "Uma metrópole futurista com tradição, tecnologia e uma cena gastronômica incrível.", Image = "Lbk9FXlL2RUF.jpg", DistanceFromPortugal = 10000 },
                new Destination { Id = 10, Name = "Sydney", Country = "Austrália", Description = "Praias paradisíacas, a Ópera icônica e um estilo de vida ao ar livre.", Image = "PvYDr2VJB0fq.jpg", DistanceFromPortugal = 12000 },
                new Destination { Id = 11, Name = "Nova Iorque", Country = "EUA", Description = "A cidade que nunca dorme, com Broadway, Central Park e uma energia incomparável.", Image = "VYCtrNDLAMCM.jpg", DistanceFromPortugal = 5800 },
                new Destination { Id = 12, Name = "Bangkok", Country = "Tailândia", Description = "Templos dourados, mercados flutuantes e comida de rua deliciosa.", Image = "Gn0AkmQJu0iH.jpeg", DistanceFromPortugal = 8500 }
            );

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "admin", Password = "admin", Email = "admin@skyhorizon.com", Role = "Admin" }
            );
        }
    }
}
