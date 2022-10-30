# IFramework

IFramework OOP, AOP tasarım desenleri kullanılarak geliştirilen ve bir alt yapı sağlayan framework'tür.

Projedeki docker compose ile birlikte Mssql server, Web application server, redis, elasticsearch, kibana, logstash ve sonarqube sunucuları çalıştırılmaktadır.

## Kullanılan teknolojiler ve mimariler

- Clean Architecture
- Onion Architecture
- OOP
- AOP
- EntityFramework Core
- Castle Windsor (Dynamic Proxy, Container)
- NLOG
- FluentValidation
- AutoMapper
- Swagger
- Redis
- ELK
- Docker
- Sonarqube

## Proje Adresleri

Proje clone alınır. Sonrasında komut satırı açılır. Komut satırında projenin ana dizinine gelinir ve aşağıdaki komut çalıştırılarak proje ayağa kaldırılır. Uygulamayı Visual studio içerisinde çalıştırmak için startup project docker compose seçilir ve F5 ile proje ayağa kaldırılabilir.
- docker-compose up

Proje çalıştırıldıktan sonra aşağıdaki adreslerden ilgili ortamlara ulaşılabilir.
 - WebApi Projesi : https://localhost:8443/swagger/index.html
 - Kibana : http://localhost:5601/
 - Sonarqube : http://localhost:9000/

Mssql server, Redis vb. alt ortam adresleri docker compose dosyasından kontrol edilebilir.

## Sonarqube kod taraması başlatma

Proje bir defa çalıştırıldıktan sonra (Sonarqube server çalıştıktan sonra) proje ana dizininde komut satırı açılır ve aşağıdaki komut çalıştırılır.

```bash
docker build --network=dockercompose4150345526347878863_iframework-network --build-arg PROJECT_KEY=iframework --build-arg PROJECT_NAME=iframework --build-arg SONAR_LOGIN_TOKEN="efe1beb9e6fdff1084a3473a6a32c45948c7664c" --build-arg SONAR_HOST=http://sonarqube.iframework.com:9000 .
```
