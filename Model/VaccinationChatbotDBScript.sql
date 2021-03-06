USE [master]
GO
/****** Object:  Database [VaccinationChatbot]    Script Date: 5 Dec 2021 11:57:53 pm ******/
CREATE DATABASE [VaccinationChatbot]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'VaccinationChatbot', FILENAME = N'D:\Program Files\MSSQL15.SQLEXPRESS\MSSQL\DATA\VaccinationChatbot.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'VaccinationChatbot_log', FILENAME = N'D:\Program Files\MSSQL15.SQLEXPRESS\MSSQL\DATA\VaccinationChatbot_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [VaccinationChatbot] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [VaccinationChatbot].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [VaccinationChatbot] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [VaccinationChatbot] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [VaccinationChatbot] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [VaccinationChatbot] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [VaccinationChatbot] SET ARITHABORT OFF 
GO
ALTER DATABASE [VaccinationChatbot] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [VaccinationChatbot] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [VaccinationChatbot] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [VaccinationChatbot] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [VaccinationChatbot] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [VaccinationChatbot] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [VaccinationChatbot] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [VaccinationChatbot] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [VaccinationChatbot] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [VaccinationChatbot] SET  DISABLE_BROKER 
GO
ALTER DATABASE [VaccinationChatbot] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [VaccinationChatbot] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [VaccinationChatbot] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [VaccinationChatbot] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [VaccinationChatbot] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [VaccinationChatbot] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [VaccinationChatbot] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [VaccinationChatbot] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [VaccinationChatbot] SET  MULTI_USER 
GO
ALTER DATABASE [VaccinationChatbot] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [VaccinationChatbot] SET DB_CHAINING OFF 
GO
ALTER DATABASE [VaccinationChatbot] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [VaccinationChatbot] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [VaccinationChatbot] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [VaccinationChatbot] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [VaccinationChatbot] SET QUERY_STORE = OFF
GO
USE [VaccinationChatbot]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 5 Dec 2021 11:57:53 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[AccountID] [int] IDENTITY(1,1) NOT NULL,
	[AccountType] [varchar](20) NOT NULL,
	[Fname] [varchar](50) NOT NULL,
	[Mname] [varchar](50) NOT NULL,
	[Lname] [varchar](50) NOT NULL,
	[Birthdate] [date] NOT NULL,
	[City] [varchar](50) NOT NULL,
	[PostalCode] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Booking]    Script Date: 5 Dec 2021 11:57:53 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[BookingID] [int] IDENTITY(1,1) NOT NULL,
	[AccountID] [int] NOT NULL,
	[ReferenceNumber] [varchar](50) NOT NULL,
	[ClinicID] [int] NOT NULL,
	[DoseType] [int] NOT NULL,
	[AppointmentDate] [date] NOT NULL,
	[AppointmentTime] [time](7) NOT NULL,
 CONSTRAINT [PK_Booking] PRIMARY KEY CLUSTERED 
(
	[BookingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clinic]    Script Date: 5 Dec 2021 11:57:53 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clinic](
	[ClinicID] [int] IDENTITY(1,1) NOT NULL,
	[LocationName] [varchar](50) NOT NULL,
	[PostalCode] [varchar](50) NOT NULL,
	[Capacity] [int] NULL,
 CONSTRAINT [PK_Clinic] PRIMARY KEY CLUSTERED 
(
	[ClinicID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([AccountID], [AccountType], [Fname], [Mname], [Lname], [Birthdate], [City], [PostalCode]) VALUES (1, N'User', N'Mike', N'Vin', N'Wilson', CAST(N'2002-10-15' AS Date), N'Toronto', N'M5V0A1')
INSERT [dbo].[Account] ([AccountID], [AccountType], [Fname], [Mname], [Lname], [Birthdate], [City], [PostalCode]) VALUES (2, N'Admin', N'Jim', N'Harl', N'Smith', CAST(N'1990-05-20' AS Date), N'Toronto', N'M5G0A8')
INSERT [dbo].[Account] ([AccountID], [AccountType], [Fname], [Mname], [Lname], [Birthdate], [City], [PostalCode]) VALUES (3, N'User', N'mike', N'g', N'smith', CAST(N'1984-01-22' AS Date), N'Woodstock', N'N4T0H1')
INSERT [dbo].[Account] ([AccountID], [AccountType], [Fname], [Mname], [Lname], [Birthdate], [City], [PostalCode]) VALUES (4, N'User', N'john', N'c', N'smart', CAST(N'1996-01-25' AS Date), N'woodstock', N'N4T0H1')
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
SET IDENTITY_INSERT [dbo].[Booking] ON 

INSERT [dbo].[Booking] ([BookingID], [AccountID], [ReferenceNumber], [ClinicID], [DoseType], [AppointmentDate], [AppointmentTime]) VALUES (2, 4, N'4F320E', 1, 1, CAST(N'2021-12-17' AS Date), CAST(N'09:00:00' AS Time))
SET IDENTITY_INSERT [dbo].[Booking] OFF
GO
SET IDENTITY_INSERT [dbo].[Clinic] ON 

INSERT [dbo].[Clinic] ([ClinicID], [LocationName], [PostalCode], [Capacity]) VALUES (1, N'Southwestern Clinic', N'N4T0H1', 78)
INSERT [dbo].[Clinic] ([ClinicID], [LocationName], [PostalCode], [Capacity]) VALUES (2, N'Toronto Clinic', N'M4VA01', 250)
INSERT [dbo].[Clinic] ([ClinicID], [LocationName], [PostalCode], [Capacity]) VALUES (3, N'Guelph', N'L7HT21', 150)
SET IDENTITY_INSERT [dbo].[Clinic] OFF
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Account] FOREIGN KEY([AccountID])
REFERENCES [dbo].[Account] ([AccountID])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Account]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Clinic] FOREIGN KEY([ClinicID])
REFERENCES [dbo].[Clinic] ([ClinicID])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Clinic]
GO
USE [master]
GO
ALTER DATABASE [VaccinationChatbot] SET  READ_WRITE 
GO
