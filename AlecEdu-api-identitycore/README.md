# Tạo migrate

Vào thư mục src chạy lệnh sau:

> dotnet ef migrations add "InitIntittyServer" --project AlecEdu-api-identitycore.Infrastructure --startup-project AlecEdu-api-identitycore.API --output-dir Persistence/Migrations --verbose

> dotnet ef database update --project AlecEdu-api-identitycore.Infrastructure --startup-project AlecEdu-api-identitycore.API
