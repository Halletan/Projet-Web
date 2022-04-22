USE [master]
GO
/****** Object:  Database [SpaceAdventures]    Script Date: 22/04/2022 19:09:18 ******/
CREATE DATABASE [SpaceAdventures]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SpaceAdventures', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\SpaceAdventures.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SpaceAdventures_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\SpaceAdventures_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [SpaceAdventures] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SpaceAdventures].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SpaceAdventures] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SpaceAdventures] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SpaceAdventures] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SpaceAdventures] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SpaceAdventures] SET ARITHABORT OFF 
GO
ALTER DATABASE [SpaceAdventures] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SpaceAdventures] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SpaceAdventures] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SpaceAdventures] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SpaceAdventures] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SpaceAdventures] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SpaceAdventures] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SpaceAdventures] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SpaceAdventures] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SpaceAdventures] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SpaceAdventures] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SpaceAdventures] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SpaceAdventures] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SpaceAdventures] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SpaceAdventures] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SpaceAdventures] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SpaceAdventures] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SpaceAdventures] SET RECOVERY FULL 
GO
ALTER DATABASE [SpaceAdventures] SET  MULTI_USER 
GO
ALTER DATABASE [SpaceAdventures] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SpaceAdventures] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SpaceAdventures] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SpaceAdventures] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SpaceAdventures] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SpaceAdventures] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'SpaceAdventures', N'ON'
GO
ALTER DATABASE [SpaceAdventures] SET QUERY_STORE = OFF
GO
USE [SpaceAdventures]
GO
/****** Object:  Table [dbo].[Aircraft]    Script Date: 22/04/2022 19:09:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Aircraft](
	[IdAircraft] [int] IDENTITY(1,1) NOT NULL,
	[Manufacturer] [nvarchar](50) NOT NULL,
	[Model] [nvarchar](50) NOT NULL,
	[NumberOfSeats] [int] NOT NULL,
 CONSTRAINT [PK_Aircraft] PRIMARY KEY CLUSTERED 
(
	[IdAircraft] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AircraftSeat]    Script Date: 22/04/2022 19:09:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AircraftSeat](
	[IdAircraftSeat] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Price] [float] NOT NULL,
	[IdBooking] [int] NOT NULL,
	[PassengerLastName] [nvarchar](50) NOT NULL,
	[PassengerFirstName] [nvarchar](50) NOT NULL,
	[IdFlight] [int] NOT NULL,
 CONSTRAINT [PK_AircraftSeat] PRIMARY KEY CLUSTERED 
(
	[IdAircraftSeat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Airport]    Script Date: 22/04/2022 19:09:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Airport](
	[IdAirport] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[IdPlanet] [int] NOT NULL,
 CONSTRAINT [PK_Airport] PRIMARY KEY CLUSTERED 
(
	[IdAirport] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_Airport_Code_Name_City] UNIQUE NONCLUSTERED 
(
	[Name] ASC,
	[IdPlanet] ASC,
	[IdAirport] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Booking]    Script Date: 22/04/2022 19:09:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[IdBooking] [int] IDENTITY(1,1) NOT NULL,
	[IdFlight] [int] NOT NULL,
	[IdClient] [int] NOT NULL,
	[BookingAmount] [float] NOT NULL,
 CONSTRAINT [PK_Booking] PRIMARY KEY CLUSTERED 
(
	[IdBooking] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 22/04/2022 19:09:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[IdClient] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[IdMemberShipType] [int] NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[IdClient] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Flight]    Script Date: 22/04/2022 19:09:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Flight](
	[IdFlight] [int] IDENTITY(1,1) NOT NULL,
	[FlightStatus] [int] NOT NULL,
	[IdAircraft] [int] NOT NULL,
	[Price] [float] NOT NULL,
	[IdItinerary] [int] NOT NULL,
	[DepartureTime] [datetime] NOT NULL,
	[ArrivalTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Flight] PRIMARY KEY CLUSTERED 
(
	[IdFlight] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Itinerary]    Script Date: 22/04/2022 19:09:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Itinerary](
	[IdItinerary] [int] NOT NULL,
	[IdAirport1] [int] NOT NULL,
	[IdAiport2] [int] NOT NULL,
	[Rate] [float] NOT NULL,
 CONSTRAINT [PK_Itinerary] PRIMARY KEY CLUSTERED 
(
	[IdItinerary] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MembershipType]    Script Date: 22/04/2022 19:09:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MembershipType](
	[IdMemberShipType] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[DiscountFactor] [float] NOT NULL,
 CONSTRAINT [PK_MembershipType] PRIMARY KEY CLUSTERED 
(
	[IdMemberShipType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Planet]    Script Date: 22/04/2022 19:09:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Planet](
	[IdPlanet] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Planet] PRIMARY KEY CLUSTERED 
(
	[IdPlanet] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AircraftSeat_Name]    Script Date: 22/04/2022 19:09:18 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_AircraftSeat_Name] ON [dbo].[AircraftSeat]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AircraftSeat]  WITH CHECK ADD  CONSTRAINT [FK_AircraftSeat_Booking] FOREIGN KEY([IdBooking])
REFERENCES [dbo].[Booking] ([IdBooking])
GO
ALTER TABLE [dbo].[AircraftSeat] CHECK CONSTRAINT [FK_AircraftSeat_Booking]
GO
ALTER TABLE [dbo].[AircraftSeat]  WITH CHECK ADD  CONSTRAINT [FK_AircraftSeat_Flight] FOREIGN KEY([IdFlight])
REFERENCES [dbo].[Flight] ([IdFlight])
GO
ALTER TABLE [dbo].[AircraftSeat] CHECK CONSTRAINT [FK_AircraftSeat_Flight]
GO
ALTER TABLE [dbo].[Airport]  WITH CHECK ADD  CONSTRAINT [FK_Airport_Planet] FOREIGN KEY([IdPlanet])
REFERENCES [dbo].[Planet] ([IdPlanet])
GO
ALTER TABLE [dbo].[Airport] CHECK CONSTRAINT [FK_Airport_Planet]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Client] FOREIGN KEY([IdClient])
REFERENCES [dbo].[Client] ([IdClient])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Client]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Flight] FOREIGN KEY([IdFlight])
REFERENCES [dbo].[Flight] ([IdFlight])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Flight]
GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_Client_MembershipType] FOREIGN KEY([IdMemberShipType])
REFERENCES [dbo].[MembershipType] ([IdMemberShipType])
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_Client_MembershipType]
GO
ALTER TABLE [dbo].[Flight]  WITH CHECK ADD  CONSTRAINT [FK_Flight_Aircraft] FOREIGN KEY([IdAircraft])
REFERENCES [dbo].[Aircraft] ([IdAircraft])
GO
ALTER TABLE [dbo].[Flight] CHECK CONSTRAINT [FK_Flight_Aircraft]
GO
ALTER TABLE [dbo].[Flight]  WITH CHECK ADD  CONSTRAINT [FK_Flight_Itinerary] FOREIGN KEY([IdItinerary])
REFERENCES [dbo].[Itinerary] ([IdItinerary])
GO
ALTER TABLE [dbo].[Flight] CHECK CONSTRAINT [FK_Flight_Itinerary]
GO
ALTER TABLE [dbo].[Itinerary]  WITH CHECK ADD  CONSTRAINT [FK_Itinerary_Airport1] FOREIGN KEY([IdAirport1])
REFERENCES [dbo].[Airport] ([IdAirport])
GO
ALTER TABLE [dbo].[Itinerary] CHECK CONSTRAINT [FK_Itinerary_Airport1]
GO
ALTER TABLE [dbo].[Itinerary]  WITH CHECK ADD  CONSTRAINT [FK_Itinerary_Airport2] FOREIGN KEY([IdAiport2])
REFERENCES [dbo].[Airport] ([IdAirport])
GO
ALTER TABLE [dbo].[Itinerary] CHECK CONSTRAINT [FK_Itinerary_Airport2]
GO
USE [master]
GO
ALTER DATABASE [SpaceAdventures] SET  READ_WRITE 
GO
