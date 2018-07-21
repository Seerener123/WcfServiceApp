To allow enitity framework to support mysql database, please install the following dependencies via nuget manager.
	- Entity Framework.
	- MySql.Data by Oracle
	- MySql.Data.EntityFramwork by Oracle.
	- Google.Protobuf By Google.

After installing the dependencies, please add them to reference of each project. Inside your reference, you should have
something like this:
	- Google.Protobuf
	- MySql.Data
	- MySql.Data.EntityFramwork
	- MySql.Data.EntityFramworkCore