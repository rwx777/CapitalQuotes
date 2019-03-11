#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see http://aka.ms/containercompat 

FROM microsoft/dotnet-framework:4.7.2-runtime-windowsservercore-1803
ARG source
WORKDIR /app
COPY ${source:-obj/Docker/publish} .
ADD https://download.microsoft.com/download/3/2/2/3224B87F-CFA0-4E70-BDA3-3DE650EFEBA5/vcredist_x64.exe vc10_x64.exe
RUN vc10_x64.exe /quiet /install /norestart
SHELL ["powershell", "-Command"]
#RUN cp C:\app\SKAPI\x64\oledlg.dll C:\Windows\System32\;
RUN regsvr32 /s C:\app\SKAPI\x64\SKCOM.dll;
ENTRYPOINT ["C:\\app\\CapitalQuotes.exe"]
