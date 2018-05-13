USE MASTER
GO

CREATE DATABASE CBRTelemoveis
GO

USE CBRTelemoveis
GO

CREATE TABLE Resolu��oEcra(
idResolu��oEcra INTEGER NOT NULL IDENTITY(1,1),
designa��o VARCHAR(20) NOT NULL, 
PRIMARY KEY(idResolu��oEcra)
)

CREATE TABLE Ecra(
idEcra INTEGER NOT NULL IDENTITY(1,1),
tamanho FLOAT NOT NULL,     --polegadas
idResolu��oEcra INTEGER NOT NULL,
CHECK(tamanho>0),
PRIMARY KEY(idEcra),
FOREIGN KEY(idResolu��oEcra) REFERENCES Resolu��oEcra
)

CREATE TABLE Processador(
idProcessador INTEGER NOT NULL IDENTITY(1,1),
velocidadeProcessador FLOAT NOT NULL, --GHz
nucleosProcessador INTEGER NOT NULL,
CHECK(velocidadeProcessador>0),
CHECK(nucleosProcessador>0),
PRIMARY KEY(idProcessador)
)

CREATE TABLE Cameras(
idCameras INTEGER NOT NULL IDENTITY(1,1),
resolucaoFrontal FLOAT NOT NULL,
resolucaoTraseira FLOAT NOT NULL,
CHECK(resolucaoFrontal>0),
CHECK(resolucaoTraseira>0),
PRIMARY KEY(idCameras)
)

CREATE TABLE Estado(
idEstado INTEGER NOT NULL IDENTITY(1,1),
designa��o VARCHAR(30) NOT NULL,
PRIMARY KEY(idEstado)
)
CREATE TABLE marca(
idMarca INTEGER NOT NULL IDENTITY(1,1),
nome VARCHAR(20) NOT NULL, 
PRIMARY KEY(idMarca)
)

CREATE TABLE Telemovel(
idTelemovel INTEGER NOT NULL IDENTITY(1,1), 
ram FLOAT NOT NULL,
memoriaInterna INTEGER NOT NULL, 
mAhBateria INTEGER NOT NULL, --ex: 2000 mAh
idade INTEGER NOT NULL,
valorFinal INTEGER,
idEcra INTEGER NOT NULL,
idProcessador INTEGER NOT NULL,
idCameras INTEGER NOT NULL,
idEstado INTEGER NOT NULL,
idMarca INTEGER NOT NULL,
CHECK(ram>0),
CHECK(memoriaInterna>0),
CHECK(mAhBateria>0),
PRIMARY KEY(idTelemovel),
FOREIGN KEY(idEcra) REFERENCES Ecra,
FOREIGN KEY(idProcessador) REFERENCES Processador,
FOREIGN KEY(idCameras) REFERENCES Cameras,
FOREIGN KEY(idEstado) REFERENCES Estado,
FOREIGN KEY(idMarca) REFERENCES Marca
)