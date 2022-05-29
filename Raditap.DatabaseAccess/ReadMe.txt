For this GAC project, we are using MySql database.
This is how we implement and use database in here.

============================================================================================================
1. Models - we use auto generate model class which will generate class from table in database
For example if you add more column/field in table then here you just use command
dotnet ef dbcontext Scaffold "Server=DESKTOP-5USALAD\MSSQLSERVERDEV;User=sa;Password=adsarc;Database=Raditap_SIT" Microsoft.EntityFrameworkCore.SqlServer -c RaditapContext -o Entities -f

**** MUST DO ****
Everytime after scaffolding pls go to GacContext.cs
and remove 2 things
	1. No params constructor
	2. Code under OnConfiguring method

2. Store procedure - We will not use SP for this project but I init it for very rare case if really needed to use it.
