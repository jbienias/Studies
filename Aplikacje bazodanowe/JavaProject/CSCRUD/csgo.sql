--DROP TABLE druzyna
CREATE IF NOT EXISTS TABLE druzyna (
id INTEGER PRIMARY KEY,
nazwa VARCHAR(25) NOT NULL UNIQUE,
data_utworzenia DATE NOT NULL,
liczba_czlonkow INTEGER NOT NULL CHECK(liczba_czlonkow > 0),
sponsor VARCHAR(25) NOT NULL
);
GO

--DROP TABLE mapa
CREATE IF NOT EXISTS TABLE mapa (
id INTEGER PRIMARY KEY,
nazwa VARCHAR(25) NOT NULL UNIQUE,
data_stworzenia DATE NOT NULL,
rozmiar INTEGER NOT NULL CHECK(rozmiar >= 0),
ocena INTEGER NOT NULL CHECK(ocena >= 0 AND ocena <= 10)
);
GO


--DROP TABLE kraj
CREATE TABLE IF NOT EXISTS kraj (
id INTEGER PRIMARY KEY,
nazwa VARCHAR (25) NOT NULL UNIQUE,
iso VARCHAR (3) NOT NULL,
populacja INTEGER NOT NULL CHECK(populacja > 0),
data_zalozenia DATE NOT NULL
);
GO

--DROP TABLE zawodnik
CREATE TABLE IF NOT EXISTS zawodnik (
id INTEGER PRIMARY KEY,
imie VARCHAR(25) NOT NULL,
nazwisko VARCHAR(25) NOT NULL,
pseudonim VARCHAR(25) NOT NULL UNIQUE,
stawka DECIMAL(8,2) NOT NULL,
druzyna_id INTEGER NOT NULL REFERENCES druzyna(id) ON DELETE CASCADE,
kraj_id INTEGER NOT NULL REFERENCES kraj(id) ON DELETE CASCADE
);
GO

--DROP TABLE preferowana_mapa
CREATE TABLE IF NOT EXISTS preferowana_mapa (
mapa_id INTEGER NOT NULL REFERENCES mapa(id) ON DELETE CASCADE,
zawodnik_id INTEGER NOT NULL REFERENCES zawodnik(id) ON DELETE CASCADE
);
GO


INSERT INTO kraj(nazwa, iso, populacja, data_zalozenia) VALUES 
('Polska', 'POL', 40000000, '966-01-01'),
('Niemcy', 'DE', 80000000, '1488-12-12');
SELECT * FROM kraj;

INSERT INTO druzyna(nazwa, data_utworzenia, liczba_czlonkow, sponsor)VALUES
('Virtus.pro', '2003-04-12', 6, 'Steelseries'),
('Ninjas in Pyjamas', '2012-03-14', 8, 'Heineken'),
('Fnatic', '2012-02-12', 6, 'Intel'),
('Natus Vincere', '2009-05-02', 7, 'Amarena'),
('Team EnVyUs', '2015-02-03', 11, 'Rayman');
SELECT * FROM druzyna

INSERT INTO mapa(nazwa, data_stworzenia, rozmiar, ocena) VALUES
('de_dust2', '1999-05-20', '12', '10'),
('de_train', '2001-07-12', '6', '7'),
('de_mirage', '2000-04-10', '8', '5'),
('de_inferno', '1996-02-13', '15', '8'),
('de_cobblestone', '2016-05-15', '11', '9'),
('de_overpass', '1998-07-23', '14', '3'),
('de_cache', '2013-02-11', '17', '4'),
('de_nuke', '1994-03-22', '6', '6');
SELECT * FROM mapa

INSERT INTO zawodnik(imie, nazwisko, pseudonim, stawka, druzyna_id, kraj_id) VALUES
('Filip', 'Kubski', 'Neo',40000, 1, 1),
('Jarosław', 'Jarząbkowski', 'pashaBiceps',34000, 1,1),
('Wiktor', 'Wojtas', 'TaZ',38000, 1,1),
('Janusz', 'Pogorzelski', 'Snax', 32000, 2,1),
('Paweł', 'Bieliński', 'byali', 29500, 2,1),
('Christopher', 'Alesund', 'GeT-RiGhT', 26444, 3,1),
('Patrik', 'Lindberg', 'f0rest',51000, 3,2),
('Adam', 'Friberg', 'Friberg', 45200, 4,1),
('Richard', 'Landström', 'Xizt', 48000, 5,2),
('Jacob', 'Mourujärvi' , 'Pyth', 16000, 5,2);
SELECT * FROM zawodnik;


INSERT INTO preferowana_mapa(mapa_id, zawodnik_id)VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5);
SELECT * FROM preferowana_mapa;