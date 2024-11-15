sp_help 'Configuracion.Colonia'


select * from Configuracion.CodigoPostal 
inner join Configuracion.Municipio on Configuracion.CodigoPostal.IdMunicipio = Configuracion.Municipio.IdMunicipio
order by Colonia


select * from Configuracion.Colonia
inner join Configuracion.Municipio on Configuracion.Colonia.IdMunicipio = Configuracion.Municipio.IdMunicipio


select * from Configuracion.Municipio
where clave = '017'

select  * from Configuracion.Municipio
where Clave like '044'


delete c
from Configuracion.Colonia c
left join Configuracion.Usuario u on c.IdColonia = u.IdColonia
where c.IdColonia  not in (select IdColonia from Configuracion.Usuario)



delete m
from Configuracion.Municipio m
left join Configuracion.CodigoPostal cp on m.IdMunicipio = cp.IdMunicipio
left join Configuracion.Hospital h on m.IdMunicipio = h.IdMunicipio
left join Configuracion.Usuario u on m.IdMunicipio = u.IdMunicipio
where cp.IdMunicipio is null and h.IdMunicipio is null and u.IdMunicipio is null

delete from 
Configuracion.CodigoPostal

select * from Configuracion.Estado


--agregando iddmunicipio
ALTER TABLE Configuracion.Colonia  add 
IdMunicipio INT FOREIGN KEY REFERENCES Configuracion.Municipio(IdMunicipio)




update Configuracion.Colonia set IdMunicipio = 18

SELECT * FROM configuracion.CodigoPostal where codigopostal = 64890

select * from configuracion.municipio m
inner join configuracion.estado e on e.idestado = m.idestado
where m.idmunicipio = 145

