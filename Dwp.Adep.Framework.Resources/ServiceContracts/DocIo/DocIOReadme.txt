Purpose of DocIO service is to make the calling application independent of DocIo (Syncfusion dlls)component and use the DocIo functionality as well.
Currently the Service provides two output types 'PDF' and 'DOC'

Please follow instructions to use DocIo
	1. Make sure to change the below parameters to maximum size in the app.config/web.config of the calling application
		
		<binding maxBufferSize="4194304"
		maxBufferPoolSize="4194304"
		maxReceivedMessageSize="4194304" >

		<readerQuotas maxArrayLength="4194304" />

	2. Soft copy of a letter document is required and needs to be passed as byte array to the DocIo service
	3. Prepare the necessary list of type DocContent which contains placeholder name and their correcponding values to be replaced using DocIo service.
	4. Specify the output document type for example 'PDF' or 'DOC'
	5. Add below entry in your Host file on 'c:\windows\system32\drivers\etc' before you add docio service reference to your application.
		10.21.34.170	adv-itsst-lnk-web.adv.itsst.com
	
Test project 'TestEmailService.csproj' is available at "DWP_ADEP_Framework\Development\V2\Source\TestEmailService\TestEmailService\"
Complete solution implementing DocIo framework service in real time is LetterGen Application in TFS