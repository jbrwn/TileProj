#!/usr/bin/env bash

if test `uname` = Darwin; then
    cachedir=~/Library/Caches/KBuild
else
    if [ -z $XDG_DATA_HOME ]; then
        cachedir=$HOME/.local/share
    else
        cachedir=$XDG_DATA_HOME;
    fi
fi
mkdir -p $cachedir
nugetVersion=latest
cachePath=$cachedir/nuget.$nugetVersion.exe
url=https://dist.nuget.org/win-x86-commandline/$nugetVersion/nuget.exe
BUILDCMD_DNX_FEED=https://www.nuget.org/api/v2
BUILDCMD_DNX_VERSION=1.0.0-rc1-final 

if test ! -f $cachePath; then
    wget -O $cachePath $url 2>/dev/null || curl -o $cachePath --location $url /dev/null
fi

if test ! -e .nuget; then
    mkdir .nuget
    cp $cachePath .nuget/nuget.exe
fi

if test ! -d packages/Sake; then  
    mono .nuget/nuget.exe install Sake -ExcludeVersion -Source https://www.nuget.org/api/v2/ -o packages
fi

if test ! -d packages/KoreBuild; then
    mono .nuget/nuget.exe install KoreBuild -ExcludeVersion -Source https://www.myget.org/F/aspnetvnext/api/v2 -o packages -nocache -pre
fi    

if ! type dnvm > /dev/null 2>&1; then
    source packages/KoreBuild/build/dnvm.sh
fi

DNX_FEED=$BUILDCMD_DNX_FEED
dnvm install $BUILDCMD_DNX_VERSION -runtime coreclr 
dnvm install $BUILDCMD_DNX_VERSION -runtime mono -alias default

mono packages/Sake/tools/Sake.exe -I packages/KoreBuild/build -f makefile.shade "$@"