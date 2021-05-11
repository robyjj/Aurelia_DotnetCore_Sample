FROM microsoft/dotnet:2.2-sdk as build

ARG BUILDCONFIG=RELEASE
ARG VERSION=1.0.0

COPY Hahn.ApplicatonProcess.February2021.API.csproj /build/

RUN dotnet restore ./build/Hahn.ApplicatonProcess.February2021.API.csproj

COPY . ./build/
WORKDIR /build/
RUN dotnet publish ./Hahn.ApplicatonProcess.February2021.API.csproj -c $BUILDCONFIG -0 out /p:Version=$VERSION

FROM microsoft/dotner:2.2-aspnetcore-runtime
WORKDIR /app

COPY --from=build /build/out .

ENTRYPOINT ["dotnet",Hahn.ApplicatonProcess.February2021.API.dll]
