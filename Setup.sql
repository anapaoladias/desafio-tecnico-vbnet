USE master;
GO

CREATE DATABASE DesafioTecnicoVbNet;
GO

USE DesafioTecnicoVbNet;
GO

CREATE TABLE Empresa (
    Id INT PRIMARY KEY IDENTITY,
    Nome VARCHAR(200) NOT NULL,
    CNPJ VARCHAR(14) NOT NULL UNIQUE
);

CREATE TABLE Associado (
    Id INT PRIMARY KEY IDENTITY,
    Nome VARCHAR(200) NOT NULL,
    Cpf VARCHAR(11) NOT NULL UNIQUE,
    DataNascimento DATETIME NULL
);


CREATE TABLE Empresa_Associado (
    EmpresaId INT NOT NULL,
    AssociadoId INT NOT NULL,
    PRIMARY KEY (EmpresaId, AssociadoId),
    FOREIGN KEY (EmpresaId) REFERENCES Empresa(Id),
    FOREIGN KEY (AssociadoId) REFERENCES Associado(Id)
);

-- Carga inicial para exemplo
declare @empresaId int,
		@associadoId int

insert into Empresa (Nome, CNPJ) values ('Empresa 1', '12345678900001')
select @empresaId = SCOPE_IDENTITY()

insert into Associado (Nome, Cpf, DataNascimento) values ('Associado 1','12345678901',getdate())
select @associadoId = SCOPE_IDENTITY()

insert into Empresa_Associado values (@empresaId, @associadoId)