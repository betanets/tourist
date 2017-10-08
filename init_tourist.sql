create table sight (
	id serial,
	sight_name varchar(255),
	sight_descr varchar(255),
	
	constraint sight_id primary key(id)
) with (OIDS = false);

comment on table sight IS 'Таблица достопримечательностей';

create table tourtype (
	id serial,
	tour_type_name varchar(255),
	
	constraint tourtype_id primary key(id)
) with (OIDS = false);

comment on table tourtype IS 'Таблица типов экскурсий';

create table schedule (
	id serial,
	tour_date timestamp,
	
	constraint schedule_id primary key(id)
) with (OIDS = false);

comment on table schedule IS 'Таблица расписания экскурсий';

create table instructor (
	id serial,
	surname varchar(255),
	forename varchar(255),
	patronymic varchar(255),
	id_schedule int,
	id_tour_type int,
	
	constraint instructor_id primary key(id),
	
	constraint instructor_schedule_fk FOREIGN KEY(id_schedule)
    references schedule(id),
    
    constraint instructor_tour_type_fk FOREIGN KEY(id_tour_type)
    references tourtype(id)
) with (OIDS = false);

comment on table instructor IS 'Таблица инструкторов';

create table tour (
	id serial,
	tour_name varchar(255),
	tour_descr varchar(255),
	id_sight int,
	id_schedule int,
	id_tour_type int,
	
	constraint tour_id primary key(id),
	
	constraint tour_sight_fk FOREIGN KEY(id_sight)
    references sight(id),
    
    constraint tour_schedule_fk FOREIGN KEY(id_schedule)
    references schedule(id),
    
    constraint tour_tour_type_fk FOREIGN KEY(id_tour_type)
    references tourtype(id)
) with (OIDS = false);

comment on table tour IS 'Таблица экскурсий';