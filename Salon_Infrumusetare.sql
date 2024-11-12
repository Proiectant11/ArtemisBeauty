--creare baza de date--
DROP DATABASE IF EXISTS SALON
CREATE DATABASE SALON 
ON PRIMARY
(
	Name = SALON1,
	FileName = 'C:\Baza_Date\SALON.mdf',
	size = 10MB,
	maxsize = unlimited,
	filegrowth = 1GB
	),
	(
	Name = SALON2,
	FileName = 'C:\Baza_Date\SALON.ndf',
	size = 10MB,
	maxsize = unlimited,
	filegrowth = 1GB
	)
	LOG ON
	(
	Name = SALON_log,
	FileName = 'C:\Proiect_BD\SALON.ldf',
	size = 10MB,
	maxsize = unlimited,
	filegrowth = 1024MB
	)

	--creare tabele--
DROP TABLE IF EXISTS AngajatFunctiiDepartamente;
DROP TABLE IF EXISTS Programari;
DROP TABLE IF EXISTS Preferinte;
DROP TABLE IF EXISTS Istoric_Clienti;
DROP TABLE IF EXISTS Departamente;
DROP TABLE IF EXISTS Functii;
DROP TABLE IF EXISTS Clienti;
DROP TABLE IF EXISTS Angajati;
DROP TABLE IF EXISTS Servicii;
DROP TABLE IF EXISTS Produse_Cosmetice;


CREATE TABLE Departamente
(
	ID_departament int PRIMARY KEY IDENTITY(1,1),
	Denumire varchar(50) NOT NULL
);

CREATE TABLE Functii
(
	ID_Functie int PRIMARY KEY IDENTITY(1,1),
	Denumire varchar(50) NOT NULL
);

CREATE TABLE Angajati
(
	ID_Angajat int PRIMARY KEY IDENTITY(1,1),
	Nume varchar(30) NOT NULL,
	Prenume varchar(20) NOT NULL,
	Data_Nasterii datetime,
	Data_Angajarii datetime,
	Adresa nvarchar(60),
	Oras nvarchar(50)
);

CREATE TABLE Angajati_Functii_Departamente
(
	ID_Angajat int FOREIGN KEY REFERENCES Angajati(ID_Angajat),
	ID_Functie int FOREIGN KEY REFERENCES Functii(ID_Functie),
	ID_Departament int FOREIGN KEY REFERENCES Departamente(ID_Departament)
);

CREATE TABLE Clienti 
(
	ID_Client int PRIMARY KEY IDENTITY(1,1),
	Nume varchar(30) NOT NULL,
	Prenume varchar(20) NOT NULL,
	Telefon varchar(10),
	Email varchar(30),
	Parola varchar(30)
);

CREATE TABLE Servicii
(
	ID_Serviciu int PRIMARY KEY IDENTITY(1,1),
	Denumire varchar(200) NOT NULL,
	Durata_aprox int NOT NULL,
	Pret float NOT NULL,
	Categorie varchar(50)
);

CREATE TABLE Programari
(
	ID_Programare int PRIMARY KEY IDENTITY(1,1),
	ID_Client int FOREIGN KEY REFERENCES Clienti(ID_Client),
	ID_Angajat int FOREIGN KEY REFERENCES Angajati(ID_Angajat),
	ID_Serviciu int FOREIGN KEY REFERENCES Servicii(ID_Serviciu),
	Data_programare datetime NOT NULL,
	Durata int NOT NULL,
	Status varchar(50)
);

CREATE TABLE Preferinte
(
	ID_Preferinta int PRIMARY KEY IDENTITY(1,1),
	ID_Programare int FOREIGN KEY REFERENCES Programari(id_Programare),
	Detalii_Preferinta varchar(255)
);

CREATE TABLE Produse_Cosmetice
(
	ID_Produs int PRIMARY KEY IDENTITY(1,1),
	Denumire varchar(50) NOT NULL,
	CantitaeInStoc int NOT NULL,
	PretBucata float,
	Furnizor varchar(30)
);

CREATE TABLE Istoric_Clienti
(	
	ID_Istoric int PRIMARY KEY IDENTITY(1,1),
	ID_Client int FOREIGN KEY REFERENCES Clienti(ID_Client),
	ID_Programare int FOREIGN KEY REFERENCES Programari(ID_Programare)

);

INSERT INTO Servicii(Denumire,Pret,Categorie) Values
('Ends Trim',90, 'Coafor'),
('Shape Change Haircut',120, 'Coafor'),
('Bangs Trim',40, 'Coafor'),
('Short Haircut Package',170, 'Coafor'),
('Medium Haircut Package',200, 'Coafor'),
('Long Haircut Package',250, 'Coafor'),
('Wash',50, 'Coafor'),
('Extension wash',70, 'Coafor'),
('Scalp Detox',40, 'Coafor'),
('Treatment Ampoule',65, 'Coafor'),
('Anti-hairloss Ampoule',65, 'Coafor'),
('Styling with Curling Iron or Straightener-short hair', 50, 'Coafor'),
('Styling with Curling Iron or Straightener-medium hair', 70, 'Coafor'),
('Styling with Curling Iron or Straightener-long hair', 90, 'Coafor'),
('Styling with Curling Iron or Straightener-very long hair', 110, 'Coafor'),
('Short hair coloring package(includes application, dye, wash and styling', 350, 'Coafor'),
('Medium hair coloring package', 385, 'Coafor'),
('Long hair coloring package', 385, 'Coafor'),
('Extra long hair coloring package', 385, 'Coafor'),
('Semi-permanent manicure', 130, 'Manicure'),
('Semi-permanent manicure with apex', 150, 'Manicure'),
('Manicure with gel protection on natural nail', 170, 'Manicure'),
('Classic manicure', 70, 'Manicure'),
('Spa(scrub, cream and massage)', 50, 'Manicure'),
('Removal of semi-permanet polish', 30, 'Manicure'),
('Complex nail design', 15, 'Manicure'),
('French', 30, 'Manicure'),
('Nail reconstruction', 15, 'Manicure'),
('Baby Boomer', 30, 'Manicure'),
('Semi-permanent pedicure', 170, 'Pedicure'),
('Classic pedicure', 130, 'Pedicure'),
('Semi-permanent pedicure-maintenance', 175, 'Pedicure'),
('Removal of semi-permanent pedicure', 45, 'Pedicure'),
('Paraffin pedicure', 50, 'Pedicure'),
('Heel cleaning', 30, 'Pedicure'),
('Eyebrow styling', 50, 'Beauty'),
('Eyebrow tinting', 50, 'Beauty'),
('Eyebrow lamination', 200, 'Beauty'),
('Permanent Eyelashes', 150, 'Beauty'),
('Eyelash tinting', 20, 'Beauty'),
('Facial Massage', 120, 'Beauty'),
('Skin hydration', 150, 'Beauty'),
('Deep cleansing treatment', 420, 'Beauty'),
('Therapeutic massage 60 minutes', 200, 'Massage'),
('Back massage 40 minutes', 180, 'Massage'),
('Lymphatic Drainage 50 minutes', 200, 'Massage'),
('Relaxation massage 60 minutes', 200, 'Massage'),
('Mixed massage 60 minutes', 200, 'Massage'),
('Anti-cellulite massage 60 minutes', 200, 'Massage'),
('Facial massage 60 minutes', 50, 'Massage'),
('Full body peeling with massage and hydration', 280, 'Massage')
