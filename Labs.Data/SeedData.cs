using Labs.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Labs.Data.SeedData;

public static class DatabaseSeeder
{
    public static void SeedData(ModelBuilder modelBuilder)
    {
        SeedTrainTypes(modelBuilder);
        SeedWagonTypes(modelBuilder);
        SeedDestinations(modelBuilder);
        SeedPassengers(modelBuilder);
        SeedTrains(modelBuilder);
        SeedWagons(modelBuilder);
        SeedTickets(modelBuilder);
    }

    private static void SeedTrainTypes(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TrainType>().HasData(
            new TrainType
            {
                TrainTypeId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                TypeName = "Швидкісний"
            },
            new TrainType
            {
                TrainTypeId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                TypeName = "Інтерсіті"
            },
            new TrainType
            {
                TrainTypeId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                TypeName = "Пасажирський"
            }
        );
    }

    private static void SeedWagonTypes(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WagonType>().HasData(
            new WagonType
            {
                WagonTypeId = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                WagonTypeName = "Загальний",
                Surcharge = 0m
            },
            new WagonType
            {
                WagonTypeId = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                WagonTypeName = "Плацкартний",
                Surcharge = 50m
            },
            new WagonType
            {
                WagonTypeId = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                WagonTypeName = "Купе",
                Surcharge = 150m
            },
            new WagonType
            {
                WagonTypeId = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                WagonTypeName = "Люкс",
                Surcharge = 300m
            }
        );
    }

    private static void SeedDestinations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Destination>().HasData(
            new Destination
            {
                DestinationId = Guid.Parse("88888888-8888-8888-8888-888888888888"),
                DestinationName = "Київ",
                Distance = 0,
                BasePrice = 0m
            },
            new Destination
            {
                DestinationId = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                DestinationName = "Львів",
                Distance = 540,
                BasePrice = 450m
            },
            new Destination
            {
                DestinationId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                DestinationName = "Одеса",
                Distance = 440,
                BasePrice = 380m
            },
            new Destination
            {
                DestinationId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                DestinationName = "Харків",
                Distance = 480,
                BasePrice = 400m
            }
        );
    }

    private static void SeedPassengers(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Passenger>().HasData(
            new Passenger
            {
                PassengerId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                FirstName = "Іван",
                LastName = "Петренко",
                MiddleName = "Васильович",
                Address = "вул. Хрещатик, 1, Київ",
                PhoneNumber = "+380501234567"
            },
            new Passenger
            {
                PassengerId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                FirstName = "Марія",
                LastName = "Коваленко",
                MiddleName = "Олександрівна",
                Address = "вул. Шевченка, 5, Львів",
                PhoneNumber = "+380672345678"
            },
            new Passenger
            {
                PassengerId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                FirstName = "Петро",
                LastName = "Сидоренко",
                MiddleName = "Іванович",
                Address = "вул. Дерибасівська, 10, Одеса",
                PhoneNumber = "+380633456789"
            },
            new Passenger
            {
                PassengerId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                FirstName = "Ольга",
                LastName = "Тимошенко",
                MiddleName = "Михайлівна",
                Address = "вул. Сумська, 15, Харків",
                PhoneNumber = "+380994567890"
            },
            new Passenger
            {
                PassengerId = Guid.Parse("10101010-1010-1010-1010-101010101010"),
                FirstName = "Андрій",
                LastName = "Мельник",
                MiddleName = "Сергійович",
                Address = "вул. Independece, 20, Київ",
                PhoneNumber = "+380955678901"
            }
        );
    }

    private static void SeedTrains(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Train>().HasData(
            new Train
            {
                TrainId = Guid.Parse("20202020-2020-2020-2020-202020202020"),
                TrainNumber = "001IC",
                TrainTypeId = Guid.Parse("22222222-2222-2222-2222-222222222222")
            },
            new Train
            {
                TrainId = Guid.Parse("30303030-3030-3030-3030-303030303030"),
                TrainNumber = "123",
                TrainTypeId = Guid.Parse("33333333-3333-3333-3333-333333333333")
            },
            new Train
            {
                TrainId = Guid.Parse("40404040-4040-4040-4040-404040404040"),
                TrainNumber = "777",
                TrainTypeId = Guid.Parse("11111111-1111-1111-1111-111111111111")
            }
        );
    }

    private static void SeedWagons(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Wagon>().HasData(
            // Поїзд 001IC (Інтерсіті)
            new Wagon
            {
                WagonId = Guid.Parse("50505050-5050-5050-5050-505050505050"),
                TrainId = Guid.Parse("20202020-2020-2020-2020-202020202020"),
                WagonNumber = "1",
                WagonTypeId = Guid.Parse("66666666-6666-6666-6666-666666666666") // Купе
            },
            new Wagon
            {
                WagonId = Guid.Parse("60606060-6060-6060-6060-606060606060"),
                TrainId = Guid.Parse("20202020-2020-2020-2020-202020202020"),
                WagonNumber = "2",
                WagonTypeId = Guid.Parse("77777777-7777-7777-7777-777777777777") // Люкс
            },

            // Поїзд 123 (Пасажирський)
            new Wagon
            {
                WagonId = Guid.Parse("70707070-7070-7070-7070-707070707070"),
                TrainId = Guid.Parse("30303030-3030-3030-3030-303030303030"),
                WagonNumber = "1",
                WagonTypeId = Guid.Parse("55555555-5555-5555-5555-555555555555") // Плацкарт
            },
            new Wagon
            {
                WagonId = Guid.Parse("80808080-8080-8080-8080-808080808080"),
                TrainId = Guid.Parse("30303030-3030-3030-3030-303030303030"),
                WagonNumber = "2",
                WagonTypeId = Guid.Parse("44444444-4444-4444-4444-444444444444") // Загальний
            },

            // Поїзд 777 (Швидкісний)
            new Wagon
            {
                WagonId = Guid.Parse("90909090-9090-9090-9090-909090909090"),
                TrainId = Guid.Parse("40404040-4040-4040-4040-404040404040"),
                WagonNumber = "1",
                WagonTypeId = Guid.Parse("66666666-6666-6666-6666-666666666666") // Купе
            }
        );
    }

    private static void SeedTickets(ModelBuilder modelBuilder)
    {
        var baseDate = new DateTime(2025, 12, 1, 8, 0, 0);

        // at least 2 tickets for each of the 5 passengers
        modelBuilder.Entity<Ticket>().HasData(
            // Пасажир 1: Іван Петренко - 2 квитки
            new Ticket
            {
                TicketId = Guid.Parse("a1111111-1111-1111-1111-111111111111"),
                PassengerId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                TrainId = Guid.Parse("20202020-2020-2020-2020-202020202020"),
                WagonId = Guid.Parse("50505050-5050-5050-5050-505050505050"),
                DestinationId = Guid.Parse("99999999-9999-9999-9999-999999999999"), // Львів
                DepartureDateTime = baseDate,
                ArrivalDateTime = baseDate.AddHours(6),
                UrgencySurcharge = 20m,
                TotalPrice = 620m
            },
            new Ticket
            {
                TicketId = Guid.Parse("a2222222-2222-2222-2222-222222222222"),
                PassengerId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                TrainId = Guid.Parse("30303030-3030-3030-3030-303030303030"),
                WagonId = Guid.Parse("70707070-7070-7070-7070-707070707070"),
                DestinationId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), // Одеса
                DepartureDateTime = baseDate.AddDays(7),
                ArrivalDateTime = baseDate.AddDays(7).AddHours(7),
                UrgencySurcharge = 0m,
                TotalPrice = 430m
            },

            // Пасажир 2: Марія Коваленко - 2 квитки
            new Ticket
            {
                TicketId = Guid.Parse("b1111111-1111-1111-1111-111111111111"),
                PassengerId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                TrainId = Guid.Parse("40404040-4040-4040-4040-404040404040"),
                WagonId = Guid.Parse("90909090-9090-9090-9090-909090909090"),
                DestinationId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), // Харків
                DepartureDateTime = baseDate.AddDays(2),
                ArrivalDateTime = baseDate.AddDays(2).AddHours(5),
                UrgencySurcharge = 50m,
                TotalPrice = 600m
            },
            new Ticket
            {
                TicketId = Guid.Parse("b2222222-2222-2222-2222-222222222222"),
                PassengerId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                TrainId = Guid.Parse("20202020-2020-2020-2020-202020202020"),
                WagonId = Guid.Parse("60606060-6060-6060-6060-606060606060"),
                DestinationId = Guid.Parse("99999999-9999-9999-9999-999999999999"), // Львів
                DepartureDateTime = baseDate.AddDays(14),
                ArrivalDateTime = baseDate.AddDays(14).AddHours(6),
                UrgencySurcharge = 0m,
                TotalPrice = 750m
            },

            // Пасажир 3: Петро Сидоренко - 2 квитки
            new Ticket
            {
                TicketId = Guid.Parse("c1111111-1111-1111-1111-111111111111"),
                PassengerId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                TrainId = Guid.Parse("30303030-3030-3030-3030-303030303030"),
                WagonId = Guid.Parse("80808080-8080-8080-8080-808080808080"),
                DestinationId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), // Одеса
                DepartureDateTime = baseDate.AddDays(3),
                ArrivalDateTime = baseDate.AddDays(3).AddHours(7),
                UrgencySurcharge = 20m,
                TotalPrice = 400m
            },
            new Ticket
            {
                TicketId = Guid.Parse("c2222222-2222-2222-2222-222222222222"),
                PassengerId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                TrainId = Guid.Parse("40404040-4040-4040-4040-404040404040"),
                WagonId = Guid.Parse("90909090-9090-9090-9090-909090909090"),
                DestinationId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), // Харків
                DepartureDateTime = baseDate.AddDays(20),
                ArrivalDateTime = baseDate.AddDays(20).AddHours(5),
                UrgencySurcharge = 0m,
                TotalPrice = 550m
            },

            // Пасажир 4: Ольга Тимошенко - 2 квитки
            new Ticket
            {
                TicketId = Guid.Parse("d1111111-1111-1111-1111-111111111111"),
                PassengerId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                TrainId = Guid.Parse("20202020-2020-2020-2020-202020202020"),
                WagonId = Guid.Parse("50505050-5050-5050-5050-505050505050"),
                DestinationId = Guid.Parse("99999999-9999-9999-9999-999999999999"), // Львів
                DepartureDateTime = baseDate.AddDays(5),
                ArrivalDateTime = baseDate.AddDays(5).AddHours(6),
                UrgencySurcharge = 20m,
                TotalPrice = 620m
            },
            new Ticket
            {
                TicketId = Guid.Parse("d2222222-2222-2222-2222-222222222222"),
                PassengerId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                TrainId = Guid.Parse("30303030-3030-3030-3030-303030303030"),
                WagonId = Guid.Parse("70707070-7070-7070-7070-707070707070"),
                DestinationId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), // Харків
                DepartureDateTime = baseDate.AddDays(25),
                ArrivalDateTime = baseDate.AddDays(25).AddHours(7),
                UrgencySurcharge = 0m,
                TotalPrice = 450m
            },

            // Пасажир 5: Андрій Мельник - 2 квитки
            new Ticket
            {
                TicketId = Guid.Parse("e1111111-1111-1111-1111-111111111111"),
                PassengerId = Guid.Parse("10101010-1010-1010-1010-101010101010"),
                TrainId = Guid.Parse("40404040-4040-4040-4040-404040404040"),
                WagonId = Guid.Parse("90909090-9090-9090-9090-909090909090"),
                DestinationId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), // Одеса
                DepartureDateTime = baseDate.AddDays(6),
                ArrivalDateTime = baseDate.AddDays(6).AddHours(5),
                UrgencySurcharge = 20m,
                TotalPrice = 530m
            },
            new Ticket
            {
                TicketId = Guid.Parse("e2222222-2222-2222-2222-222222222222"),
                PassengerId = Guid.Parse("10101010-1010-1010-1010-101010101010"),
                TrainId = Guid.Parse("20202020-2020-2020-2020-202020202020"),
                WagonId = Guid.Parse("60606060-6060-6060-6060-606060606060"),
                DestinationId = Guid.Parse("99999999-9999-9999-9999-999999999999"), // Львів
                DepartureDateTime = baseDate.AddDays(30),
                ArrivalDateTime = baseDate.AddDays(30).AddHours(6),
                UrgencySurcharge = 0m,
                TotalPrice = 750m
            }
        );
    }
}