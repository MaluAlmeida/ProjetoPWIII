create database dbProjetoPW; 
use dbProjetoPW;

create table Usuarios (
IdUser int primary key auto_increment, 
Nome varchar(50) not null,
Email varchar(50) not null,
Senha varchar(50) not null
);

create table Produtos (
IdProd int primary key auto_increment, 
Nome varchar(50) not null,
Descricao varchar(50) not null,
Preco decimal(5,2) not null, 
Quantidade int not null
);