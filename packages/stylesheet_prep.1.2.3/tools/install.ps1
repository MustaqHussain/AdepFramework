param($installPath, $toolsPath, $package, $project)


$folderList = 
                $project.ProjectItems.Item("Scripts"),
                $project.ProjectItems.Item("Content").ProjectItems.Item("themes")			
	
$ignoreList = "custom"
	

function removeFolder($folder){
   
    if($folder){
        $folder.ProjectItems | ForEach-Object { 
        
           if($ignoreList -contains $_.Name){
           
				Write-host "Ignoring file: "$_.Name
				
            }else{ 
			     removeFile $_
            }
        } 
    }
    
    Write-host $folder.Name " : "  $folder.ProjectItems.Count
    
    removeFolderIfEmpty $folder
    
}

function removeFile($file){
	# If kind is file
	if($file.Kind -eq "{6BB5F8EE-4483-11D3-8BCF-00C04F8EC28C}"){
                 $file.Delete()
	}else{
		removeFolder $file
	}

}

function removeFolderIfEmpty($folder){
    if($folder.ProjectItems.Count -eq 0){
        Write-host "Folder - " $folder.Name " is empty deleting."
        $folder.Delete()  
    }
}

function GetProjectItem($project, $name){
    if($name.Contains("\")){
        $item = $name.substring(0, $name.IndexOf('\'))
        $name = $name.substring($name.IndexOf("\")+1)
        $proj = $project.ProjectItems.Item($item)
        
        GetProjectItem -project $proj -name $name
    }else{
        return $project.ProjectItems.Item($name)
    }
}


GetProjectItem  $project "Content\IE6.css" | %{$_.Delete() }
GetProjectItem  $project "Content\Legacy.css" | %{$_.Delete() }
GetProjectItem  $project "Content\Site_dev.css" | %{$_.Delete() }
GetProjectItem  $project "Content\Site.css" | %{$_.Delete() }
GetProjectItem  $project "Content\JQueryOverwrite.css" | %{$_.Delete() }
GetProjectItem  $project "Content\images\dwp_logo.jpg" | %{$_.Delete() }
GetProjectItem  $project "Content\images\dwp_logo.png" | %{$_.Delete() }
GetProjectItem  $project "Content\images\menu.png" | %{$_.Delete() }
GetProjectItem  $project "Content\images\menu.jpg" | %{$_.Delete() }
GetProjectItem  $project "Scripts\custom\general.js" | %{$_.Delete() }
GetProjectItem  $project "Scripts\custom\IE6.js" | %{$_.Delete() }		
GetProjectItem  $project "_._" | %{$_.Delete() }	



foreach($item in $folderList){
	removeFolder $item
}

foreach($item in $ListToRemove){
	removeFile $item
}