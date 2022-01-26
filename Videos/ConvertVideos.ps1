$root = $(gl).Path
$srcP = $($root+"\src")
$outP = $($root+"\out")
$assets= 

echo "Removing all from out"
rm $($outP+"\*") -recurse -force

foreach($dir in $(ls $srcP)){
	
	$dirN = $dir.Name
	echo $("   " +$dirN )
	$outDirP = $outP + "\" +$dirN
	mkdir $outDirP > ""
	foreach ($vid in $(ls $dir)){
		$vidN = $vid.Name
		echo $("      " +$vidN )

		$inF = $vid.FullName
		$outF = $outDirP +"\" +$vidN
		echo $("         Start " +$inF)

		ffmpeg -i $inF -c:v libx264 -b:v 23M -pix_fmt yuv420p -c:a aac -b:a 192K -ss 03 -fs 90M -loglevel quiet -threads 12 $outF

		echo $("         Done" +$outF)
	}

	echo "Moving "+$dirN + " into Resources"
	mv $outDirP "B:\Git\Pono2VR\Assets\0Tours\Videos\Resources\" -force
	
}

