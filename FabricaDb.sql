create database FabricaDB

use FabricaDB

create table PRODUCTO
(

	ProID Int primary Key identity(1,1),
	ProDesc Varchar (50)  not null,
	ProValor Money not null,
	

)

create table USUARIO
(

	UsuID Int primary Key identity(1,1),
	UsuNombre Varchar (50)  not null,
	UsuPass Varchar (20) not null,

)

create table PEDIDOS
(

	PedID Int primary Key identity(1,1),
	PedUsu Int   not null,
	PedPro Int  not null,
	PedVrUnit Money  not null,
	PedCant float  not null,
	PedSubtot  Money  not null,
    PedIVA float   not null,
	PedTotal Money  not null,
	CONSTRAINT FK_Producto FOREIGN KEY (PedPro) REFERENCES PRODUCTO(ProID),
	CONSTRAINT FK_Usuario FOREIGN KEY (PedUsu) REFERENCES USUARIO(UsuID),

)

insert into PRODUCTO values ( 'PRODUCTO 1', 2500)
insert into PRODUCTO values ( 'PRODUCTO 2', 2500)
insert into PRODUCTO values ( 'PRODUCTO 3', 2500)
insert into PRODUCTO values ( 'PRODUCTO 4', 2500)
insert into PRODUCTO values ( 'PRODUCTO 5', 2500)
insert into USUARIO values ( 'USUARIO1', 'PASS1')
insert into USUARIO values ( 'USUARIO2', 'PASS2')
insert into USUARIO values ( 'USUARIO3', 'PASS3')
insert into USUARIO values ( 'USUARIO4', 'PASS4')
insert into USUARIO values ( 'USUARIO5', 'PASS5')

insert into PEDIDOS([PedUsu]
           ,[PedPro]
           ,[PedVrUnit]
           ,[PedCant]
           ,[PedSubtot]
           ,[PedIVA]
           ,[PedTotal]) 
	values ( 1,
	1,
	2500,
	2,
	5000,
	0.19,
	5950)

	insert into PEDIDOS([PedUsu]
           ,[PedPro]
           ,[PedVrUnit]
           ,[PedCant]
           ,[PedSubtot]
           ,[PedIVA]
           ,[PedTotal]) 
	values ( 1,
	2,
	2500,
	2,
	5000,
	0.19,
	5950)

	insert into PEDIDOS([PedUsu]
           ,[PedPro]
           ,[PedVrUnit]
           ,[PedCant]
           ,[PedSubtot]
           ,[PedIVA]
           ,[PedTotal]) 
	values ( 3,
	2,
	2500,
	2,
	5000,
	0.19,
	5950)