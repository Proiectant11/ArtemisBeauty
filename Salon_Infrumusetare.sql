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
DROP TABLE Preferinte;
DROP TABLE Istoric_Clienti;
DROP TABLE Programari;
DROP TABLE Angajati_Servicii;
DROP TABLE Produse_Cosmetice;
DROP TABLE Angajati_Functii_Departamente;
DROP TABLE Clienti;
DROP TABLE Servicii;
DROP TABLE Angajati;
DROP TABLE Functii;
DROP TABLE Departamente;


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
	Oras varchar(50),
	Email varchar(30),
	Parola varchar(30)
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
	Parola varchar(30),
	Gen varchar(1)
);

CREATE TABLE Servicii
(
	ID_Serviciu int PRIMARY KEY IDENTITY(1,1),
	Denumire varchar(200) NOT NULL,
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
	Ora TIME not null,
	Stare varchar(50)
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
	ID_Programare int FOREIGN KEY REFERENCES Programari(ID_Programare),
	Recenzie varchar(200),
	Nota int
);

CREATE TABLE Angajati_Servicii
(
	ID_relatie int PRIMARY KEY IDENTITY(1,1),
	ID_Angajat int FOREIGN KEY REFERENCES Angajati(ID_Angajat),
    ID_Serviciu int FOREIGN KEY REFERENCES Servicii(ID_Serviciu)
);
 
ALTER TABLE Angajati_Functii_Departamente
ADD ID_Relatie INT IDENTITY(1,1) PRIMARY KEY;

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
('Styling short hair', 50, 'Coafor'),
('Styling medium hair', 70, 'Coafor'),
('Styling long hair', 90, 'Coafor'),
('Styling very long hair', 110, 'Coafor'),
('Short hair coloring package(includes application, dye, wash and styling)', 350, 'Coafor'),
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

INSERT INTO Angajati(Nume, Prenume, Data_Nasterii, Data_Angajarii, Adresa, Oras, Email, Parola) VALUES 
('Steopoae', 'Anamaria', '2003-02-15','2010-01-13', 'Strada Lalelelor 20','Bistrita', 'admin@ab.ro', 'admin'),
('Stan', 'Sabin','2003-02-16', '2014-05-23', 'Strada Pinului 7','Borlesti', 'sabin@ab.ro', 'angajat'),
('Popescu', 'Ion', '1985-03-15', '2010-06-01', 'Strada Lalelelor 12', 'Bucuresti', 'popescu@ab.ro', 'angajat'),
('Ionescu', 'Maria', '1990-07-20', '2015-02-18', 'Bulevardul Victoriei 44', 'Cluj-Napoca','ionescu@ab.ro', 'angajat'),
('Georgescu', 'Andrei', '1982-11-05', '2012-08-23', 'Strada Principala 3', 'Timisoara', 'georgescu@ab.ro', 'angajat'),
('Vasilescu', 'Elena', '1992-04-10', '2018-12-05', 'Aleea Trandafirilor 7', 'Brasov', 'vasilescu@ab.ro', 'angajat'),
('Dumitru', 'Mihai', '1980-09-25', '2010-11-30', 'Calea Mosilor 110', 'Constanta', 'dumitru@ab.ro', 'angajat'),
('Matei', 'Andreea', '1995-01-14', '2020-03-01', 'Strada 1 Mai 25', 'Iasi', 'matei@ab.ro', 'angajat'),
('Petrescu', 'Victor', '1988-06-30', '2014-07-17', 'Strada Presei Libere 22', 'Oradea', 'petrescu@ab.ro', 'angajat'),
('Popa', 'Anca', '1993-12-01', '2016-05-10', 'Strada Linistii 8', 'Sibiu', 'popa@ab.ro', 'angajat'),
('Stan', 'Alin', '1987-02-15', '2011-09-09', 'Strada Calarasi 56', 'Ploiesti', 'stan@ab.ro', 'angajat'),
('Marin', 'Laura', '1991-10-05', '2017-04-02', 'Strada Tineretului 10', 'Targu Mures', 'marin@ab.ro', 'angajat'),
('Sima', 'Adrian', '1984-08-18', '2013-02-20', 'Strada Martisor 3', 'Braila', 'sima@ab.ro', 'angajat'),
('Radu', 'Simona', '1994-11-10', '2019-09-14', 'Calea Dorobantilor 88', 'Bucuresti', 'radu@ab.ro', 'angajat'),
('Calinescu', 'Doru', '1980-04-30', '2010-01-15', 'Strada Mihai Eminescu 12', 'Bucuresti', 'calinescu@ab.ro', 'angajat'),
('Luca', 'Oana', '1996-05-22', '2021-06-05', 'Bulevardul Unirii 99', 'Cluj-Napoca', 'luca@ab.ro', 'angajat'),
('Enache', 'Florin', '1989-09-08', '2016-11-12', 'Strada Sfantul Gheorghe 24', 'Bacau', 'enache@ab.ro', 'angajat'),
('Gheorghe', 'Raluca', '1983-07-15', '2011-02-02', 'Strada Capcaunului 17', 'Pitesti', 'gheorghe@ab.ro', 'angajat'),
('Tudor', 'Alexandru', '1981-03-28', '2008-10-12', 'Calea Grivitei 63', 'Craiova', 'tudor@ab.ro', 'angajat'),
('Ilie', 'Stefania', '1992-12-18', '2017-01-10', 'Strada Gloriei 55', 'Satu Mare', 'ilie@ab.ro', 'angajat'),
('Roman', 'Florentina', '1994-01-11', '2020-08-15', 'Strada Despartirii 2', 'Slatina', 'roman@ab.ro', 'angajat'),
('Voinescu', 'Bogdan', '1986-03-02', '2014-05-23', 'Strada Pacii 16', 'Arad', 'voinescu@ab.ro', 'angajat');

INSERT INTO Departamente(Denumire) VALUES
('Departamentul de Coafor'),
('Departamentul de Manichiura'),
('Departamentul de Cosmetologie'),
('Departamentul de Masaj'),
('Departamentul Administrativ')

INSERT INTO Functii(Denumire) VALUES
('Coafor'),--1
('Frizer'),
('Specialist colorist'),
('Tehnician manichiura'),--2
('Tehnician pedichiura'),
('Cosmetician'),--3
('Specialist in tratamente faciale'),
('Maseur terapeutic'),--4
('Specialist masaj de relaxare'),
('Consultant produse cosmetice'),--5
('Manager administrativ'),
('Contabil'),
('Specialist resurse umane');

INSERT INTO Angajati_Functii_Departamente(ID_Angajat,ID_Functie,ID_Departament) Values
(1,11,5),
(2,13,5),
(3,1,1),
(4,1,1),
(5,2,1),
(6,2,1),
(7,3,1),
(8,1,1),
(9,4,2),
(10,4,2),
(11,4,2),
(12,5,2),
(13,5,2),
(14,6,3),
(15,7,3),
(16,8,4),
(17,8,4),
(18,9,4),
(19,10,5),
(20,3,1),
(21,6,3),
(22,12,5);

INSERT INTO Angajati_Servicii(ID_Angajat,ID_Serviciu) VALUES 
(5,1),
(6,1),
(5,2),
(6,2),
(5,3),
(6,3),
(5,4),
(6,4),
(5,5),
(6,5),
(5,6),
(6,6),
(5,7),
(6,7),
(5,8),
(6,8),
(5,9),
(6,9),
(5,10),
(6,10),
(5,11),
(6,11),
(3,12),
(4,12),
(8,12),
(3,13),
(4,13),
(8,13),
(3,14),
(4,14),
(8,14),
(3,15),
(4,15),
(8,15),
(7,16),
(7,17),
(7,18),
(7,19),
(9,20),
(10,20),
(11,20),
(9,21),
(10,21),
(11,21),
(9,22),
(10,22),
(11,22),
(9,23),
(10,23),
(11,23),
(9,24),
(10,24),
(11,24),
(9,25),
(10,25),
(11,25),
(9,26),
(10,26),
(11,26),
(9,27),
(10,27),
(11,27),
(9,28),
(10,28),
(11,28),
(9,29),
(10,29),
(11,29),
(12,30),
(13,30),
(12,31),
(13,31),
(12,32),
(13,32),
(12,33),
(13,33),
(12,34),
(13,34),
(12,35),
(13,35),
(14,36),
(14,37),
(14,38),
(14,39),
(14,40),
(15,41),
(15,42),
(15,43),
(16,44),
(17,44),
(16,45),
(17,45),
(18,46),
(18,47),
(16,48),
(17,48),
(18,49),
(18,50),
(18,51),
(21,36),
(21,37),
(21,38),
(21,39),
(21,40),
(20,16),
(20,17),
(20,18),
(20,19)


INSERT INTO Produse_Cosmetice (Denumire, CantitaeInStoc, PretBucata, Furnizor) VALUES
('Moisturizing Shampoo', 75, 14.99, 'HairCare Essentials'), 
('Color Protection Conditioner', 50, 20.00, 'ColorLock'), 
('Hair Styling Mousse', 60, 18.50, 'StyleMaster'), 
('Hydrating Facial Mask', 40, 28.00, 'SkinGlow'), 
('Acne Treatment Cream', 30, 35.00, 'ClearSkin'), 
('Collagen Serum', 45, 50.00, 'AgePerfect Skincare'),
('Nail Strengthening Base Coat', 80, 7.50, 'NailTech'),
('Luxury Pedicure Scrub', 70, 12.00, 'FootCare Pro'), 
('Pedicure Foot Soak', 90, 6.99, 'FootCare Essentials'), 
('Cuticle Oil', 100, 9.00, 'NailCare Plus'),
('Smoothing Hair Serum', 40, 22.00, 'ShineOn Haircare'),
('Volumizing Hairspray', 50, 15.00, 'MaxVolume'), 
('Gentle Facial Cleanser', 60, 18.00, 'SkinEase'), 
('Nail Art Gel Kit', 30, 25.00, 'ArtNails'),
('Pedicure Callus Remover', 80, 11.50, 'SmoothFeet'), 
('Anti-Frizz Leave-in Conditioner', 45, 17.00, 'FrizzFree Haircare'), 
('Whitening Facial Cream', 40, 40.00, 'BrightSkin'), 
('Luxurious Foot Cream', 70, 14.50, 'FootCare Pro'), 
('Nail Polish (Red Glam)', 100, 8.00, 'NailMaster'), 
('Professional Hair Dryer', 30, 120.00, 'ProTools'), 
('Hair Repair Treatment', 50, 45.00, 'HairRenew'), 
('Facial Toner', 60, 20.00, 'PureSkin'), 
('Foot Exfoliating Mask', 75, 13.00, 'FootBeauty'), 
('Herbal Foot Scrub', 90, 10.00, 'FootEssence'),
('Permanent Hair Dye - Black', 50, 25.00, 'ColorPerfect'), 
('Permanent Hair Dye - Dark Brown', 60, 26.00, 'ColorPerfect'),
('Permanent Hair Dye - Light Brown', 40, 27.00, 'HairColor Co.'), 
('Semi-Permanent Hair Dye - Red', 30, 20.00, 'BrightLocks'), 
('Semi-Permanent Hair Dye - Purple', 40, 22.00, 'VividColor'),
('Permanent Hair Dye - Blonde', 50, 28.00, 'LuxeColor');
