FROM microsoft/dotnet:2.1-sdk

COPY . /app

WORKDIR /app

RUN ["dotnet", "build"]

EXPOSE 6502/tcp

CMD ["dotnet", "run", "--server.urls", "http://*:6502", "--no-launch-profile"]
