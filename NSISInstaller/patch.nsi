; !include "MUI2.nsh"
; Installer Content
Name "PolygonMC"
!define MUI_ICON "..\PolygonMC\wwwroot\images\icon.ico"
OutFile "patch.exe"
; Unicode True

; Registry data
InstallDir "$PROGRAMFILES\LFInteractive\PolygonMC"
InstallDirRegKey HKCU "Software\LFInteractive\PolygonMC" ""
RequestExecutionLevel user

!define MUI_STARTMENUPAGE_REGISTRY_ROOT "HKCU" 
!define MUI_STARTMENUPAGE_REGISTRY_KEY "Software\LFInteractive\PolygonMC" 

Section "PolygonMC" SecPolygonMC
  SetOutPath "$INSTDIR\PolygonMC"
  Delete "..\PolygonMC\bin\Release\net7.0-windows10.0.19041.0\win10-x64\settings.json"

  File "..\PolygonMC\bin\Release\net7.0-windows10.0.19041.0\win10-x64\PolygonMC.exe"
  File /r "..\PolygonMC\bin\Release\net7.0-windows10.0.19041.0\win10-x64\wwwroot"
  File "..\PolygonMC\bin\Release\net7.0-windows10.0.19041.0\win10-x64\PolygonMC.dll"
  File /r "..\Minecraft.NET\Modpack.NET\bin\Release\net7.0\*.dll"

  ExecWait '"$INSTDIR\PolygonMC.zip" -o "$INSTDIR"' $0
  AccessControl::GrantOnFile "$INSTDIR" "(BU)" "FullAccess"
  ExecShell "" "$INSTDIR\PolygonMC\PolygonMC.exe"
  SetAutoClose true
SectionEnd
