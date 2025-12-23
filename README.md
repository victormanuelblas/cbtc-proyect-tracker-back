## Proyecto Final Tracker
Curso: Desarrollo de Servicio Web 1

Backend con estructura hexagonal hecho .Net.

#### 1) Seeding with Mysql:
```sql
-- DROP DATABASE IF EXISTS tracker_db;
-- CREATE DATABASE tracker_db;
USE tracker_db;

INSERT INTO tracker_db.customer_types VALUES (1,'regular');
INSERT INTO tracker_db.customer_types VALUES (2,'vip');
INSERT INTO tracker_db.customer_types VALUES (3,'gold');
INSERT INTO tracker_db.customer_types VALUES (4,'platinium');


INSERT INTO tracker_db.shipment_statuses VALUES (1,'Habilitado');
INSERT INTO tracker_db.shipment_statuses VALUES (2,'Empaquetado');
INSERT INTO tracker_db.shipment_statuses VALUES (3,'En Tránsito');
INSERT INTO tracker_db.shipment_statuses VALUES (4,'LlegadaDestino');
INSERT INTO tracker_db.shipment_statuses VALUES (5,'Entregado');
INSERT INTO tracker_db.shipment_statuses VALUES (6,'Devuelto');
```
#### 2) Place .env inside Tracker.Api

5 required keys. Placeholder values

```
DB_SERVER=localhost
DB_PORT=3306
DB_NAME=tracker
DB_USER=
DB_PASSWORD=

```


#### 3)Run:
```
JWT_SECRET="a" JWT_ISSUER="b" JWT_AUDIENCE="c" dotnet run --project src/Tracker.Api
```

#### 4) Success message:
```
.env loaded!
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5016

```
