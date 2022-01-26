# Pono VR clone

## Feture request

 - 1. Language switch ~ 15 minutes
 - 2. City switch ~ 15 minutes
 - 3. Virtual Walk ~ 30 minutes

## Video Compression

Just run `.\ConvertVideos.ps1` in `Videos` directory

### FFmpeg command
Finally command

`ffmpeg -i .\mistok-src.mp4 -c:v libx264 -b:v 23M -pix_fmt yuv420p -c:a aac -b:a 192K -ss 03 -fs 90M mistok-5.mp4`

### Mistok Kol4ava

The biggest file is pasika. And it's important to set file read as it is.

#### Input 
> 396 MB
> 50 seconds
> Bitrate 66 MB
> libx264
Just file witout 

#### Default
> 95 MB
> Bitrate 15 MB
After uplying default `ffmpeg` compression wit

#### Recommended for 360 x264
> 177 MB
> Bitrate 30 mb

`ffmpeg -i .\mistok-src.mp4 -c:v libx264 -b:v 30M -pix_fmt yuv420p -c:a aac -b:a 192K mistok-2.mp4`

Bitrate 30 mb does make much more sense

So I will use That one with file size limitation 80 MB just in case

####  Test 1
> 23 seconds
> 83 MB
> Bitrate 30 mb

####  Test 2 
> 91 MB
> 30 seconds
> Bitrate 25 mb

#### Test 3
> 91 MB
> 33 seconds
> Bitrate 23 Mb

## Upload to cloud

To upload run command:

`.\UploadScript.ps1 "{VERSION}"`

> `{VERSION}` - current name of apk for example {1.14}.apk



