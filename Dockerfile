FROM  mcr.microsoft.com/dotnet/sdk:5.0

WORKDIR /rabbit/

COPY . /rabbit/

ENTRYPOINT [ "/bin/bash" ]