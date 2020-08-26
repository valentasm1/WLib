Wlib Readme


****************************** IMPORTANT ****************************************

Personal plugin. Contact gudrutis16@gmail.com for more info

****************************** IMPORTANT ****************************************
<uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
<uses-permission android:name="android.permission.INTERNET" />
<uses-permission android:name="android.permission.CAMERA" />
<uses-permission android:name="android.permission.RECORD_AUDIO" />
<uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />
<uses-permission android:name="android.permission.FLASHLIGHT" />


private void CheckAppPermissions()
{
	if ((int) Build.VERSION.SdkInt < 23)
	{
		return;
	}
	else
	{
		if (PackageManager.CheckPermission(Manifest.Permission.Camera, PackageName) != Permission.Granted
			&& PackageManager.CheckPermission(Manifest.Permission.WriteExternalStorage, PackageName) != Permission.Granted
			&& PackageManager.CheckPermission(Manifest.Permission.RecordAudio, PackageName) != Permission.Granted
		)
		{
			var permissions = new string[] {Manifest.Permission.Camera, Manifest.Permission.WriteExternalStorage, Manifest.Permission.RecordAudio};
			RequestPermissions(permissions, 1);
		}
	}
}