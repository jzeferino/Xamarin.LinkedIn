#!/bin/bash
cd native_libraries/li-android-sdk-1.1.4-release/
# build release bypassing lint
./gradlew linkedin-sdk:clean linkedin-sdk:assembleRelease -x lint
cp linkedin-sdk/build/outputs/aar/linkedin-sdk-release.aar ../../src/Xamarin.Android.LinkedIn/Jars/
cd ../../
./build.sh -t Build-Android-Library -v diagnostic 