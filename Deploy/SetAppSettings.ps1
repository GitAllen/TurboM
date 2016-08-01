$resourceGroupName = $Env:ResourceGroupName
$webAppName = $Env:WebAppName
$slotName = $Env:SlotName

$webApp = Get-AzureRMWebAppSlot -ResourceGroupName $resourceGroupName -Name $webAppName -Slot $slotName
$appSettingList = $webApp.SiteConfig.AppSettings
$hash = @{}
ForEach ($kvp in $appSettingList) {
    $hash[$kvp.Name] = $kvp.Value
}
$hash["Version"] = "$Env:BUILD_BUILDNUMBER"
Set-AzureRMWebAppSlot -ResourceGroupName $resourceGroupName -Name $webAppName -AppSettings $hash -Slot $slotName
Write-Host "Done!"