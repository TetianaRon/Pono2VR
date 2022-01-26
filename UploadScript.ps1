	$build=$args[0]
	echo $("Sart upload for  build:"+$build)

	$file = "B:\Git\Pono2VR\Builds\Oculus\"+$build;
	$apk = $($file+".apk") 
	B:\Res\AppData\Roaming\odh\ovr-platform-util.exe upload-quest-build --app-id 4778895315525656 --app-secret 4c97715bde301d62ad12fb61b7b90c2c --apk $apk --channel LIVE --obb $($file+".main.obb")
	echo $("Uploaded file: "+$file)

