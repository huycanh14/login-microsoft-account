# Tạo migrate

Vào thư mục src chạy lệnh sau:

> dotnet ef migrations add "InitIdentityServer" --project AlecEdu-api-ISV4.Infrastructure --context AlecEduContext --startup-project AlecEdu-api-ISV4.API --output-dir Persistence/Migrations --verbose

> dotnet ef database update --project AlecEdu-api-ISV4.Infrastructure --context AlecEduContext --startup-project AlecEdu-api-ISV4.API --verbose
