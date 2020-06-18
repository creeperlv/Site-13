$Folder=$args[0]
$ACL = Get-ACL $Folder
$Group = New-Object System.Security.Principal.NTAccount("Everyone")
$person = [System.Security.Principal.NTAccount]"Everyone"
$access = [System.Security.AccessControl.FileSystemRights]"FullControl"
$inheritance = [System.Security.AccessControl.InheritanceFlags] "ObjectInherit,ContainerInherit"
$propagation = [System.Security.AccessControl.PropagationFlags]"None"
$type = [System.Security.AccessControl.AccessControlType]"Allow"
$rule = New-Object System.Security.AccessControl.FileSystemAccessRule( $person, $access, $inheritance, $propagation, $type)
$ACL.AddAccessRule($rule)
$ACL.SetOwner($Group)
Set-Acl -Path $Folder -AclObject $ACL
$collection = Get-ChildItem -Path $Folder -Recurse -Force
foreach ($item in $collection) {
    Write-Host "Set Access Control: " -NoNewline
    Write-Host "$item" -ForegroundColor "Green"
    $ACL0 = Get-ACL $item.FullName
    $Group = New-Object System.Security.Principal.NTAccount("Everyone")
    $person = [System.Security.Principal.NTAccount]"Everyone"
    $access = [System.Security.AccessControl.FileSystemRights]"FullControl"
    #$inheritance = [System.Security.AccessControl.InheritanceFlags] "ObjectInherit,ContainerInherit"
    $inheritance = [System.Security.AccessControl.InheritanceFlags] "None"
    $propagation = [System.Security.AccessControl.PropagationFlags]"None"
    $type = [System.Security.AccessControl.AccessControlType]"Allow"
    $rule = New-Object System.Security.AccessControl.FileSystemAccessRule( $Group, $access, $inheritance, $propagation, $type)
    #$rule = New-Object System.Security.AccessControl.FileSystemAccessRule( $person, $access, $propagation, $type)
    $ACL0.AddAccessRule($rule)
    $ACL0.SetOwner($person)
    Set-Acl -Path $item.FullName -AclObject $ACL0
}