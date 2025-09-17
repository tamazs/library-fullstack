﻿set -a
source .env
set +a

dotnet tool install -g dotnet-ef && dotnet ef dbcontext scaffold "$CONN_STR" Npgsql.EntityFrameworkCore.PostgreSQL   --context LibraryDbContext     --no-onconfiguring        --schema library   --force