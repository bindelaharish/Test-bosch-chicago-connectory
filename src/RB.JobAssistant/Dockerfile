FROM microsoft/dotnet:2.0.7-sdk-2.1.200-stretch

COPY . /app

WORKDIR /app

RUN ["dotnet", "build"]

EXPOSE 6502/tcp

CMD ["dotnet", "run", "--server.urls", "http://*:6502", "--no-launch-profile"]
