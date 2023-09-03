# README #

To install the **Athletic Shop** project, clone this repository.
Open the **Ats.Web** project's **web.config** file and change the existing connection string as follows:

### Change the connection string ###

	<add name="AtsContext" 
	connectionString="Data Source=[YOUR-SQL-SERVER];Initial Catalog=[DB-NAME];user id=[DB-USER];password=[DB-PASSWORD];
	Integrated Security=False;Persist Security Info=False;Connection Timeout=5"
	providerName="System.Data.SqlClient" />
<ul>
	<li><strong>[YOUR-SQL-SERVER]</strong>, The full name of the MS SQL Server you are using, MS SQL Server 2019 is recommended;</li>
	<li><strong>[DB-NAME]</strong>, the name of the database, whatever you want.</li>
	<li><strong>[DB-USER]</strong>, the username that has rights to create and manage the database, for local use for development, usually sa.</li>
	<li><strong>[DB-PASSWORD]</strong>, the user's password.</li>
</ul>

### Build and Run ###
<ul>
	<li>Create the database with the name you gave in the connection string.</li>
	<li>Make sure the <strong>Ats.Web</strong> project is the startup project, otherwise select it, right-click and give the command <strong>Set as Startup Project.</strong></li>
	<li>Build the solution, it build the tables and inserts sample data.</li>
	<li>Run the <strong>Ats.Web</strong> project with <strong>F5</strong>.</li>
	<li>To login as Administrator choose Login from Header menu and in Login page use the following credentials:</li>
	<li>Username: <strong>admin</strong></li>
	<li>Password: <strong>123</strong></li>
</ul>
