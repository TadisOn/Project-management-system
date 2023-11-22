FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /

COPY PMS/PMS/*.csproj .
RUN dotnet restore -r linux-musl-x64 /p:PublishReadyToRun=true

COPY PMS/PMS/. .
RUN dotnet publish -c Release -o /app -r linux-musl-x64 --self-contained true --no-restore /p:PublishReadyToRun=true /p:PublishSingleFile=true

FROM mcr.microsoft.com/dotnet/runtime-deps:7.0-alpine-amd64
WORKDIR /app
COPY --frombuild /app .
ENTRYPOINT ["./PMS"]


ENV \
    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false \
    LC_ALL=en_US.UTF-8 \
    LANG=en_US-UTF8
RUN apk add --no-cache \
    icu-data-full \
    icu-libs