!include "MUI2.nsh"
Name "PolygonMC"
!define MUI_ICON "..\PolygonMC\wwwroot\images\icon.ico"

OutFile "PolygonMC Installer.exe"
Unicode True


Var StartMenuFolder

InstallDir "$PROGRAMFILES\LFInteractive\PolygonMC"
InstallDirRegKey HKCU "Software\LFInteractive\PolygonMC" ""
RequestExecutionLevel admin

!define MUI_ABORTWARNING
!insertmacro MUI_PAGE_LICENSE "tos.txt"


!insertmacro MUI_PAGE_DIRECTORY


!define MUI_STARTMENUPAGE_REGISTRY_ROOT "HKCU" 
!define MUI_STARTMENUPAGE_REGISTRY_KEY "Software\LFInteractive\PolygonMC" 
!define MUI_STARTMENUPAGE_REGISTRY_VALUENAME "Start Menu Folder"
!insertmacro MUI_PAGE_STARTMENU Application $StartMenuFolder

!insertmacro MUI_PAGE_INSTFILES

!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES
!insertmacro MUI_LANGUAGE "English"

Section "PolygonMC" SecPolygonMC

  SetOutPath "$INSTDIR\PolygonMC"
  File /r "..\PolygonMC\bin\Release\net7.0-windows10.0.19041.0\win10-x64\"

  ExecWait '"$INSTDIR\PolygonMC.zip" -o "$INSTDIR"' $0

  WriteRegStr HKCU "Software\LFInteractive\PolygonMC" "" $INSTDIR
  WriteUninstaller "$INSTDIR\Uninstall.exe"

  !insertmacro MUI_STARTMENU_WRITE_BEGIN Application
    
    ;Create shortcuts
    CreateDirectory "$SMPROGRAMS\$StartMenuFolder"
    CreateShortcut "$SMPROGRAMS\$StartMenuFolder\PolygonMC.lnk" "$INSTDIR\PolygonMC\PolygonMC.exe"
    CreateShortcut "$INSTDIR\PolygonMC.lnk" "$INSTDIR\PolygonMC\PolygonMC.exe"
  
  !insertmacro MUI_STARTMENU_WRITE_END

SectionEnd


Section "Uninstall"

  Delete "$INSTDIR\Uninstall.exe"

  RMDir "$INSTDIR"

  DeleteRegKey /ifempty HKCU "Software\LFInteractive\PolygonMC"

SectionEnd