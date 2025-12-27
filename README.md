## Proyecto Final Tracker
Curso: Desarrollo de Servicio Web 1

Backend con estructura hexagonal hecho .Net.

#### 1)  Seeding with Entity Framework:

```
dotnet tool restore
dotnet restore
dotnet ef database update \
	--project src/Tracker.Infrastructure \
	--startup-project src/Tracker.Api
```


#### ~~1) Seeding with Mysql:~~ (old)
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


#### 3) Run:
```
export JWT_SECRET="this-is-a-very-secure-secret-key-with-at-least-32-characters"
export JWT_ISSUER="TrackerApi"
export JWT_AUDIENCE="TrackerClient"
```
Then:
```
dotnet run --project src/Tracker.Api
```

#### 4) Success message:
```
.env loaded!
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5016

```
