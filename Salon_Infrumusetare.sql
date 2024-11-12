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

ALTER TABLE Istoric_Clienti
ADD Recenzie varchar(200) 

ALTER TABLE Istoric_Clienti
ADD Nota int 

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

INSERT INTO Angajati(Nume, Prenume, Data_Nasterii, Data_Angajarii, Adresa, Oras) VALUES 
('Steopoae', 'Anamaria', '2003-02-15','2010-01-13', 'Strada Lalelelor 20','Bistrita'),
('Stan', 'Sabin','2003-02-16', '2014-05-23', 'Strada Pinului 7','Borlesti'),
('Popescu', 'Ion', '1985-03-15', '2010-06-01', 'Strada Lalelelor 12', 'Bucuresti'),
('Ionescu', 'Maria', '1990-07-20', '2015-02-18', 'Bulevardul Victoriei 44', 'Cluj-Napoca'),
('Georgescu', 'Andrei', '1982-11-05', '2012-08-23', 'Strada Principala 3', 'Timisoara'),
('Vasilescu', 'Elena', '1992-04-10', '2018-12-05', 'Aleea Trandafirilor 7', 'Brasov'),
('Dumitru', 'Mihai', '1980-09-25', '2010-11-30', 'Calea Mosilor 110', 'Constanta'),
('Matei', 'Andreea', '1995-01-14', '2020-03-01', 'Strada 1 Mai 25', 'Iasi'),
('Petrescu', 'Victor', '1988-06-30', '2014-07-17', 'Strada Presei Libere 22', 'Oradea'),
('Popa', 'Anca', '1993-12-01', '2016-05-10', 'Strada Liniștii 8', 'Sibiu'),
('Stan', 'Alin', '1987-02-15', '2011-09-09', 'Strada Călărași 56', 'Ploiesti'),
('Marin', 'Laura', '1991-10-05', '2017-04-02', 'Strada Tineretului 10', 'Targu Mures'),
('Sima', 'Adrian', '1984-08-18', '2013-02-20', 'Strada Mărțișor 3', 'Braila'),
('Radu', 'Simona', '1994-11-10', '2019-09-14', 'Calea Dorobanților 88', 'Bucuresti'),
('Călinescu', 'Doru', '1980-04-30', '2010-01-15', 'Strada Mihai Eminescu 12', 'Bucuresti'),
('Luca', 'Oana', '1996-05-22', '2021-06-05', 'Bulevardul Unirii 99', 'Cluj-Napoca'),
('Enache', 'Florin', '1989-09-08', '2016-11-12', 'Strada Sfântul Gheorghe 24', 'Bacau'),
('Gheorghe', 'Raluca', '1983-07-15', '2011-02-02', 'Strada Căpcăunului 17', 'Pitesti'),
('Tudor', 'Alexandru', '1981-03-28', '2008-10-12', 'Calea Griviței 63', 'Craiova'),
('Ilie', 'Stefania', '1992-12-18', '2017-01-10', 'Strada Gloriei 55', 'Satu Mare'),
('Roman', 'Florentina', '1994-01-11', '2020-08-15', 'Strada Despărțirii 2', 'Slatina'),
('Voinescu', 'Bogdan', '1986-03-02', '2014-05-23', 'Strada Păcii 16', 'Arad');

INSERT INTO Departamente(Denumire) VALUES
('Departamentul de Coafor'),
('Departamentul de Manichiura'),
('Departamentul de Cosmetologie'),
('Departamentul de Masaj'),
('Departamentul de Vanzari'),
('Departamentul administrativ')

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
('Reprezentant vanzari'),--5
('Consultant produse cosmetice'),
('Manager de vanzari'),
('Manager administrativ'),--6
('Contabil'),
('Specialist resurse umane');

INSERT INTO Angajati_Functii_Departamente(ID_Angajat,ID_Functie,ID_Departament) Values
(1,13,6),
(2,15,6),
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
(20,11,5),
(21,12,5),
(22,14,6);

INSERT INTO Produse_Cosmetice (Denumire, CantitaeInStoc, PretBucata, Furnizor)
VALUES 
('Crema Hidratanta', 100, 35.50, 'Cosmetics Co.'),
('Sampon Revitalizant', 50, 22.00, 'Beauty Supplies Inc.'),
('Ruj Mat', 200, 45.75, 'Makeup Essentials'),
('Fond de ten', 80, 60.99, 'Luxury Beauty'),
('Parfum Floral', 30, 120.00, 'Fragrance World'),
('Lotiune Corp', 60, 15.50, 'SkinCare Pro'),
('Balsam de buze', 150, 10.00, 'LipCare Solutions'),
('Crema Anti-imbatranire', 40, 85.20, 'Advanced Skincare'),
('Ulei pentru Par', 70, 28.00, 'HairCare Co.'),
('Masca de fata', 90, 25.30, 'Beauty Treatments'),
('Demachiant', 120, 18.50, 'Cosmetics Co.'),
('Crema de zi', 75, 45.00, 'Advanced Skincare'),
('Sapun Antibacterian', 300, 5.00, 'Clean & Pure'),
('Lac de unghii', 220, 9.80, 'NailArt Pro'),
('Crema de maini', 90, 15.00, 'SoftTouch'),
('Exfoliant Facial', 65, 27.50, 'FaceCare Plus'),
('Spray Fixativ', 50, 18.25, 'HairCare Co.'),
('Ser Anti-acnee', 45, 55.90, 'ClearSkin Solutions'),
('Ulei Esential de Lavanda', 100, 30.00, 'Essential Oils Co.'),
('Crema contur ochi', 80, 50.99, 'EyeBeauty'),
('Gloss de buze', 180, 20.75, 'Makeup Essentials'),
('Sampon uscat', 85, 19.00, 'HairCare Co.'),
('Crema solara SPF 50', 60, 40.00, 'SunCare Pro'),
('Corector', 90, 22.45, 'Makeup Essentials'),
('Crema de picioare', 75, 16.75, 'SoftTouch'),
('Exfoliant pentru corp', 55, 32.00, 'BodyCare Pro'),
('Creion de ochi', 190, 14.20, 'Makeup Essentials'),
('Fard de obraz', 100, 35.00, 'Cosmetics Co.'),
('Gel pentru sprancene', 110, 25.80, 'Beauty Essentials'),
('Crema pentru cuticule', 60, 12.50, 'NailArt Pro'),
('Crema de noapte', 70, 65.90, 'Advanced Skincare'),
('Ulei de cocos', 140, 25.00, 'Natural Oils'),
('Spuma de curatare', 120, 17.00, 'FaceCare Plus'),
('Mascara Waterproof', 160, 27.75, 'Makeup Essentials'),
('Fiole cu acid hialuronic', 35, 90.00, 'SkinLab Solutions'),
('Ser cu Vitamina C', 50, 75.50, 'Advanced Skincare'),
('Parfum de lux', 25, 250.00, 'Fragrance World');


INSERT INTO Programari(ID_Client,ID_Angajat,ID_Serviciu,Data_programare, Durata) VALUES
(1,4,19,'2024-11-11', 30);

INSERT INTO Istoric_Clienti(ID_Client,ID_Programare,Recenzie, Nota) VALUES 
(1,2,'Servicii foarte bune', 5);

ALTER TABLE Clienti 
ADD Gen varchar(1)
