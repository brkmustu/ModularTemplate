# lokaldeki modül şablonu lokasyonu
$modulesPath="./src/BaseModule"

# project name placeholder
$oldProjectName="BaseModule"
# your project name
$newProjectName="NewProjectName"

# file type
$fileType="FileInfo"

# directory type
$dirType="DirectoryInfo"

Write-Host 'Start copy folders...'
$newRoot=$newProjectName
mkdir $newRoot
Copy-Item -Recurse .\src\CoreModule\ .\$newRoot\
Copy-Item -Recurse .\src\BaseModule\ .\$newRoot\
Copy-Item .\src\.gitignore .\$newRoot\
Copy-Item .\src\BaseModule.sln .\$newRoot\
Copy-Item .\src\bin_obj_remove.ps1 .\$newRoot\

# folder to deal with
$slnFolder = (Get-Item -Path "./$newRoot/" -Verbose).FullName

function Rename {
	param (
		$TargetFolder,
		$PlaceHolderProjectName,
		$NewProjectName
	)
	# file extensions to deal with
	$include=@("*.cs","*.cshtml","*.asax","*.ps1","*.ts","*.csproj","*.sln","*.xaml","*.json","*.js","*.xml","*.config","Dockerfile")

	$elapsed = [System.Diagnostics.Stopwatch]::StartNew()

	Write-Host "[$TargetFolder]Start rename folder..."
	# rename folder
	Ls $TargetFolder -Recurse | Where { $_.GetType().Name -eq $dirType -and $_.Name.Contains($PlaceHolderProjectName) } | ForEach-Object{
		Write-Host 'directory ' $_.FullName
		$newDirectoryName=$_.Name.Replace($PlaceHolderProjectName,$NewProjectName)
		Rename-Item $_.FullName $newDirectoryName
	}
	Write-Host "[$TargetFolder]End rename folder."
	Write-Host '-------------------------------------------------------------'

	# replace file content and rename file name
	Write-Host "[$TargetFolder]Start replace file content and rename file name..."
	Ls $TargetFolder -Include $include -Recurse | Where { $_.GetType().Name -eq $fileType} | ForEach-Object{
		$fileText = Get-Content $_ -Raw -Encoding UTF8
		if($fileText.Length -gt 0 -and $fileText.contains($PlaceHolderProjectName)){
			$fileText.Replace($PlaceHolderProjectName,$NewProjectName) | Set-Content $_ -Encoding UTF8
			Write-Host 'file(change text) ' $_.FullName
		}
		If($_.Name.contains($PlaceHolderProjectName)){
			$newFileName=$_.Name.Replace($PlaceHolderProjectName,$NewProjectName)
			Rename-Item $_.FullName $newFileName
			Write-Host 'file(change name) ' $_.FullName
		}
	}
	Write-Host "[$TargetFolder]End replace file content and rename file name."
	Write-Host '-------------------------------------------------------------'

	$elapsed.stop()
	write-host "[$TargetFolder]Total Time Cost: $($elapsed.Elapsed.ToString())"
}

Rename -TargetFolder $slnFolder -PlaceHolderProjectName $oldProjectName -NewProjectName $newProjectName

Start-Sleep -s 10