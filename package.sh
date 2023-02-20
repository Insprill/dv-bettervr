INFO_FILE="info.json"
BETTERVR_DLL="bin/Release/netframework4.8/BetterVR.dll"
DISPLAY_NAME=$(jq -r '.Id' $INFO_FILE)
VERSION=$(jq -r '.Version' $INFO_FILE)

if ! [ -e $BETTERVR_DLL ]; then
    echo "Failed to find $BETTERVR_DLL! Have you built it in release mode yet?"
    exit 1
fi

zip -1 -T -j -u "${DISPLAY_NAME}_v$VERSION.zip" $BETTERVR_DLL info.json
