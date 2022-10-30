FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS sonarqube-scan
# Install OpenJDK-11
RUN apt-get update && \
    apt-get install --yes default-jdk;

# Setup JAVA_HOME
RUN java -version

# Install global tools
RUN dotnet tool install --global dotnet-sonarscanner

# Add global tools folder to PATH
ENV PATH="${PATH}:/root/.dotnet/tools"
ENV DOTNET_ROLL_FORWARD=Major

ARG SONAR_HOST
ARG SONAR_LOGIN_TOKEN
ARG PROJECT_KEY
ARG PROJECT_NAME
ARG CACHE_DATE=2016-01-04

WORKDIR /app
COPY . .
#RUN dotnet restore /ignoreprojectextensions:.dcproj

WORKDIR /app/Docker
RUN ls -list

WORKDIR /app
RUN dotnet sonarscanner begin \
/k:$PROJECT_KEY \
/n:$PROJECT_NAME \
/v:"1" \
/d:sonar.login=$SONAR_LOGIN_TOKEN \
/d:sonar.exclusions="**/wwwroot/**, **/obj/**, **/bin/**" \
/d:sonar.links.homepage=$SONAR_HOST \
/d:sonar.host.url=$SONAR_HOST
RUN dotnet build --no-incremental /p:RestoreUseSkipNonexistentTargets=true
RUN dotnet sonarscanner end /d:sonar.login="$SONAR_LOGIN_TOKEN"