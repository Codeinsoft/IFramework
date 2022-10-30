# This is docker file for our sonarqube-scanner. You don't need to read it since
# the goal of this tutorial isn't about teaching docker or about presenting the best 
# way for creating Sonarqube scanner image. Of course feel free to check on it if you like.
# Get dotnetcore SDK
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS sonarqube

# Install OpenJDK-11
RUN apt-get update && \
    apt-get install -y openjdk-11-jdk && \
    apt-get install -y ant && \
    apt-get clean;

# Fix certificate issues
RUN apt-get update && \
    apt-get install ca-certificates-java && \
    apt-get clean && \
    update-ca-certificates -f;

# Setup JAVA_HOME
ENV JAVA_HOME /usr/lib/jvm/java-11-openjdk-amd64/
RUN export JAVA_HOM

# Install global tools
RUN dotnet tool install -g dotnetsay
RUN dotnet tool install --global dotnet-sonarscanner --version 4.7.1

# Add global tools folder to PATH
ENV PATH="${PATH}:/root/.dotnet/tools"

# Get required packages for sonar scanner
RUN apt-get update && apt-get -y install curl bash unzip yarn bzip2

WORKDIR /root

ENV LATEST='sonar-scanner-cli-3.3.0.1492-linux.zip'

# Get & install sonar scanner
RUN env && \
curl -OL 'https://binaries.sonarsource.com/Distribution/sonar-scanner-cli/'$LATEST && \
mkdir sonar_scanner && unzip -d sonar_scanner $LATEST && mv sonar_scanner/* sonar_home && \
rm -rf sonar_scanner $LATEST

# Add sonar scanner to PATH
ENV SONAR_RUNNER_HOME=/root/sonar_home
ENV PATH ${SONAR_RUNNER_HOME}/bin:$PATH

WORKDIR /app
COPY . .
# We are restoring package here in order to make logs clearer for us to read in sonar-scanner
# analysis execution section
RUN dotnet restore
RUN ls -list
# First difference between this scanner and the previous one is a way that we execute it.
# We need to execute 3 commands in order to trigger an analysis. Firstly we need to begin Sonarqube
# analysis and pass analysis parameters. Then we need to build an application and then finally we need to
# to explicitly end analysis.
# What is more syntax of passing parameters varies from the previous scanner. We have predefined special
# prefixes for project key, project name and project versions instead of passing them as casual
# parameter. Also instead of adding prefix "/D" for this scanner, we need to ad "/d:" instead.
# The last, but not least difference is that we need to authorize on both begin and end step
# if Sonarqube project is private.
# Except for the above differences other parameters is mostly working the same.
RUN dotnet sonarscanner begin \
/k:"test" \
/n:"test" \
/v:"1" \
/d:sonar.login="cae44b40cacdd5658079940c3708aacd32a4f746" \
/d:sonar.exclusions="**/wwwroot/**, **/obj/**, **/bin/**" \
/d:sonar.links.homepage="http://localhost:9000/"
RUN dotnet build --no-incremental
RUN dotnet sonarscanner end /d:sonar.login="cae44b40cacdd5658079940c3708aacd32a4f746"