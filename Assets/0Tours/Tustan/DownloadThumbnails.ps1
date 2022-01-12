function GetImage(){
$session = New-Object Microsoft.PowerShell.Commands.WebRequestSession
$session.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36 Edg/97.0.1072.55"
$imgName = "/tustan/images/preview_nodeimage_node18.jpg"
Invoke-WebRequest -UseBasicParsing -Uri $("https://vr.ekarpaty.com" + $imgName) -WebSession $session -Headers @{
	"method"="GET"
	"authority"="vr.ekarpaty.com"
	"scheme"="https"
	"path"= $($imgNode)
	"pragma"="no-cache"
	"cache-control"="no-cache"
	"sec-ch-ua"="`" Not;A Brand`";v=`"99`", `"Microsoft Edge`";v=`"97`", `"Chromium`";v=`"97`""
	"dnt"="1"
	"sec-ch-ua-mobile"="?0"
	"sec-ch-ua-platform"="`"Windows`""
	"accept"="image/webp,image/apng,image/svg+xml,image/*,*/*;q=0.8"
	"sec-fetch-site"="same-origin"
	"sec-fetch-mode"="no-cors"
	"sec-fetch-dest"="image"
	"referer"="https://vr.ekarpaty.com/tustan/"
	"accept-encoding"="gzip, deflate, br"
	"accept-language"="en-US,en;q=0.9,ru;q=0.8,uk;q=0.7"
} -OutFile $imgName
}
