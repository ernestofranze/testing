docker-compose down

docker-compose up -d

cd .\N5.Entities.Migrations\

dotnet ef database update

cd ..\N5.Confluent.Kafka.Tests\

dotnet xunit

cd ..\N5.Entities.Customer.Tests\

dotnet xunit

cd ..\N5.Entities.Product.Tests\

dotnet xunit

cd ..