/*

    Base de datos para Sistema de Gestión de Recursos Humanos

    Modulo de mantenimientos:
        - Empleados:
            - Id 
            - Código Empleado
            - Nombre
            - Apellido
            - Teléfono
            - Departamento
            - Cargo
            - Fecha ingreso
            - Salario
            - Estatus (Activo/Inactivo))

        - Departamentos
            - Id
            - Código Departamento
            - Nombre
        
        - Cargos
            - Id
            - Cargo



    - MÓDULO DE PROCESOS: En este módulo se van a configurar algunos procesos o acciones. Vale decir que estos procesos 
            o acciones son registrados en respectivas tablas.
            
            - Cálculo de nómina: Calcular el monto total de la nómina, sumando el salario de los empleados activos y presentando 
            el total, para que sea visto y validado por la persona encargada.

            Almacenar en una tabla nominas la siguiente información:
            (Id, Año, Mes, Monto Total)

            Salida de empleados: Inactivar a un determinado empleado, y almacenar la información en una tabla salidas 
            (Empleado, Tipo salida (Renuncia, Despido, Desahucio), Motivo, Fecha Salida) 
            
            Vacaciones: Registrar las vacaciones que tome un empleado.
            (Empleado, Desde, Hasta, Correspondiente a: (año), Comentarios)

            Permisos: Registrar los permisos que tome un empleado. (Empleado, Desde, Hasta, Comentarios) 
            
            Licencias: Registrar las licencias que tome un empleado. (Empleado, Desde, Hasta, Motivo, Comentarios)

    - MÓDULO DE INFORMES: En este módulo se elegirá el tipo de informe a generar para presentar listas de datos en vistas
        
        - Nóminas: Con este informe se busca visualizar todas las nóminas de un año especificado, o bien visualizar la 
        correspondiente a un mes en específico

        - Empleados Activos: Aquí buscamos visualizar a los empleados activos de la empresa, con opción de verlos todos, o de filtrar por nombre o por departamento. (Incluir ambos filtros)

        - Empleados inactivos: Visualizar todos los empleados que han salido de la empresa

        - Departamentos: Listar los departamentos creados
        
        - Cargos: Listar los cargos creados

        - Entradas de empleados por mes: Visualizar las entradas (empleados creados, ingresados) en un mes determinado por el usuario

        - Salida de empleados por mes: Visualizar las entradas (empleados creados, ingresados) en un mes determinado por el usuario
        
        - Permisos: Visualizar los permisos tomados por determinado empleado (a elegir por parte del usuario)

*/


-- drop database GestionRRHH; 

-- create database GestionRRHH;

use GestionRRHH;


create table Cargos(
    Id int identity(1,1),
    Cargo varchar(50) not null,
    constraint pk_cargos primary key(Id),
    constraint uk_cargos unique(Cargo)
);

create table Departamentos(
    Id int identity (1,1) not null,
    Codigo varchar(100) not null,
    Nombre varchar(100) not null,
	constraint pk_iddto primary key(Id), 
	constraint uk_codigodto unique(Codigo)
);


create table Empleados(
    Id  int identity(1,1),
	IdCargo int not null,
    Codigo varchar(100) not null,
    Nombre varchar(100) not null,
    Apellido varchar(100),
    Teléfono varchar(20),
    Departamento int,
    FechaIngreso datetime,
    Salario int,
    Estatus varchar(10),
    constraint pk_empleados primary key(Id),
    constraint uk_codempleado unique(Codigo),
    --constraint chk_salario check(Salario > 0),
    constraint fk_Departamento_empleado foreign key (Departamento) references Departamentos(Id),
    constraint fk_codcargo_empleado foreign key (Id) references Cargos(Id)
);


-- no va relacionada con nadie, aunque relacionalmente este mal
create table Nominas(
    Id int not null identity(1,1),
    _Year int not null,
    Mes int not null,
    MontoTotal int not null,
    constraint pk_idnomina primary key(Id),
    --constraint chk_monto_nomina check(MontoTotal >= 0)
);

create table Salidas(
    Id int not null identity(1,1),
    IdEmpleado int not null,
    TipoSalida varchar(10) not null,
    Motivo varchar(255) null,
    FechaSalida datetime not null,
    constraint pk_idsalidas primary key(Id),
    constraint fk_codsalida_empleado foreign key(IdEmpleado) references Empleados(Id) 
);


create table Vacaciones(
    Id int not null identity(1,1),
    IdEmpleado int not null,
    FechaInicio datetime not null,
    FechaFin datetime not null,
    Correspondiente datetime not null,
    Comentario varchar(255) null,
    constraint pk_idvacaciones primary key(Id),
    constraint fk_codvacaciones_empleado foreign key(IdEmpleado) references Empleados(Id),
    --constraint chk_fechas check(FechaInicio != FechaFin)
);


create table Permisos(
    Id int not null identity(1,1),
    IdEmpleado int not null,
    FechaInicio datetime not null,
    FechaFin datetime not null,
    Comentarios varchar(255) null,
    constraint pk_idpermisos primary key(Id),
    constraint fk_codpermisos_empleado foreign key(IdEmpleado) references Empleados(Id),
    --constraint chk_fechaPermiso check(FechaInicio != FechaFin)
);

create table Licencias(
	Id int not null identity(1,1),
	IdEmpleado int not null,
	FechaInicio datetime not null,
	FechaFin datetime not null,
	Motivo varchar(100) not null,
	Comentarios varchar(300) null,
	constraint pk_idlicencias primary key (Id),
	constraint fk_idempleado_licencias foreign key(IdEmpleado) references Empleados(Id),
    --constraint chk_fechaPermiso check(FechaInicio != FechaFin)
);

--nixe

