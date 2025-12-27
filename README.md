## Proyecto Final Tracker
Curso: Desarrollo de Servicio Web 1

Backend con estructura hexagonal hecho .Net.

<img width="1846" height="1026" alt="Image" src="https://github.com/user-attachments/assets/1cc161c8-28a3-48b0-b385-6373c76759f9" />
<img width="1846" height="1026" alt="Image" src="https://github.com/user-attachments/assets/85211fb8-de92-4ba1-867d-dc4ad2887123" />
<img width="1846" height="1026" alt="Image" src="https://github.com/user-attachments/assets/5737bd6e-0a41-4c5f-8895-794d7ef6e88a" />

## After cloning:

#### 1)  Load inserts with Entity Framework:

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

5 - 3 required keys

```
DB_SERVER=localhost
DB_PORT=3306
DB_NAME=tracker
DB_USER={}
DB_PASSWORD={}

JWT_SECRET="this-is-a-very-secure-secret-key-with-at-least-32-characters"
JWT_ISSUER="TrackerApi"
JWT_AUDIENCE="TrackerClient"

```


#### 3) Run:

```
dotnet run --project src/Tracker.Api
```

#### 4) Success message:
```
.env loaded!
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5016

```

#### Extra )

You can also confirm data exists from your mysql:
```
USE tracker_db;
SHOW TABLES;
SELECT * FROM shipment_statuses;
SELECT * FROM customer_types;
SELECT * FROM customers;
SELECT * FROM shipments;
SELECT * FROM users;
```
Para logearse:
- Email: 
- Password: password
```
1	Admin User	admin@tracker.com	$2a$10$U1Et/gitorJ8ogzMkWQl6OoMrqrjUR.iAPG5eRBtZFNBDRSA1XJYq	3	1
2	Juan Pérez	juan.perez@tracker.com	$2a$10$U1Et/gitorJ8ogzMkWQl6OoMrqrjUR.iAPG5eRBtZFNBDRSA1XJYq	2	1
3	María González	maria.gonzalez@tracker.com	$2a$10$U1Et/gitorJ8ogzMkWQl6OoMrqrjUR.iAPG5eRBtZFNBDRSA1XJYq	1	1
4	Carlos Ramírez	carlos.ramirez@tracker.com	$2a$10$U1Et/gitorJ8ogzMkWQl6OoMrqrjUR.iAPG5eRBtZFNBDRSA1XJYq	1	1
```
