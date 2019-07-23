Import-Module -Name WebAdministration

$domains = "fr-kinderjoy-1012226147.us-west-2.elb.amazonaws.com","kinderjoy.co.kr","www.kinderjoy.co.kr"
$appName = "FerreroRocher.KinderJoy"
$port = 8003
$webDir = "D:\websites\FerreroRocher.KinderJoy\www"

Write-Host "Creating $webDir"
if (!(Test-Path -Path $webDir)) { 
	New-Item -ItemType directory -Path $webDir
}
Write-Host "Creating the WebAppPool $appName"
if (!(Test-Path IIS:\AppPools\$appName)) {
	New-WebAppPool $appName
}
Write-Host "Creating the Website $appName"
if (!(Test-Path IIS:\Sites\$appName)) { 
	New-WebSite -Name $appName -Id $port -Port $port -PhysicalPath $webDir -ApplicationPool $appName
}
Write-Host "Creating the Bindings"
foreach ($domain in $domains) {
	New-WebBinding -Name $appName -IPAddress "*" -Port 80 -HostHeader $domain
	Write-Host "Created $domain Binding"
}
